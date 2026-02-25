import { EmitContext, emitFile, resolvePath } from "@typespec/compiler";
import { execSync } from "child_process";
import { resolve } from "path";
import { AgentSchemaEmitterOptions, EmitTarget } from "./lib.js";
import {
  BaseTestContext,
  enumerateTypes,
  PropertyNode,
  TypeNode,
} from "./ast.js";
import { GeneratorOptions, filterNodes } from "./emitter.js";
import { createTemplateEngine } from "./template-engine.js";
import { buildBaseTestContext, rustTestOptions } from "./test-context.js";
import { toSnakeCase } from "./utilities.js";

/**
 * Type mapping from TypeSpec scalar types to Rust types.
 */
export const rustTypeMapper: Record<string, string> = {
  "string": "String",
  "number": "f64",
  "array": "Vec<serde_json::Value>",
  "object": "serde_json::Value",
  "boolean": "bool",
  "int64": "i64",
  "int32": "i32",
  "float64": "f64",
  "float32": "f32",
  "integer": "i64",
  "float": "f64",
  "numeric": "f64",
  "any": "serde_json::Value",
  "dictionary": "std::collections::HashMap<String, serde_json::Value>",
};

/**
 * Rust context interfaces for template rendering.
 */
interface RustAlternate {
  /** Rust type name for the scalar (e.g. "String") */
  rustScalar: string;
  /** Variant name to use in the untagged helper enum */
  variantName: string;
  /** The snake_case field name that receives the scalar value */
  valueField: string | null;
  /** Fields to set when this alternate form is used */
  fields: Array<{
    /** snake_case Rust field name */
    rustKey: string;
    /** Original JSON property name */
    jsonKey: string;
    /** True if this field receives the scalar value */
    isValue: boolean;
    /** Literal constant value if not a placeholder */
    literalValue?: string;
  }>;
}

interface RustClassContext {
  node: TypeNode;
  typeMapper: Record<string, string>;
  alternates: RustAlternate[];
  polymorphicTypes: any;
  polymorphicTypeNames: string[];
  shorthandProperty: string | null;
  hasAlternates: boolean;
}

interface RustFileContext {
  containsAbstract: boolean;
  classes: RustClassContext[];
  typeMapper: Record<string, string>;
  packageName: string;
  polymorphicTypeNames: string[];
}

interface RustLibContext {
  packageName: string;
  moduleNames: string[];
}

/**
 * Main entry point for Rust code generation.
 */
export const generateRust = async (
  context: EmitContext<AgentSchemaEmitterOptions>,
  templateDir: string,
  node: TypeNode,
  emitTarget: EmitTarget,
  options?: GeneratorOptions
): Promise<void> => {
  const engine = createTemplateEngine(templateDir, 'rust');

  const nodes = filterNodes(Array.from(enumerateTypes(node)), options);

  const packageName = node.typeName.namespace.toLowerCase().replace(/\./g, '');

  // Collect all polymorphic type names
  const polymorphicTypeNames = new Set<string>();
  for (const n of nodes) {
    const polyTypes = n.retrievePolymorphicTypes();
    if (polyTypes) {
      polymorphicTypeNames.add(n.typeName.name);
    }
  }

  // Collect top-level module names for lib.rs
  const moduleNames: string[] = [];
  for (const n of nodes) {
    if (!n.base) {
      moduleNames.push(toSnakeCase(n.typeName.name));
    }
  }

  // Render lib.rs
  const libContext: RustLibContext = { packageName, moduleNames };
  const libContent = engine.render('lib.rs.njk', libContext);
  await emitRustFile(context, 'lib.rs', libContent, emitTarget["output-dir"]);

  // Render each base type and its children as a single file
  for (const n of nodes) {
    if (!n.base) {
      const fileContext = buildFileContext(n, packageName, polymorphicTypeNames);
      const fileContent = engine.render('file.rs.njk', fileContext);
      const fileName = toSnakeCase(n.typeName.name) + '.rs';
      await emitRustFile(context, fileName, fileContent, emitTarget["output-dir"]);
    }

    // Render test file for each type
    if (emitTarget["test-dir"]) {
      const testContext = buildTestContext(n, packageName);
      const testContent = engine.render('test.rs.njk', testContext);
      const testFileName = toSnakeCase(n.typeName.name) + '_test.rs';
      await emitRustFile(context, testFileName, testContent, emitTarget["test-dir"]);
    }
  }

  // Format emitted files if format option is enabled (default: true)
  if (emitTarget.format !== false) {
    formatRustFiles(emitTarget["output-dir"], emitTarget["test-dir"], context);
  }
};

/**
 * Format Rust files using rustfmt.
 */
function formatRustFiles(
  outputDir?: string,
  testDir?: string,
  context?: EmitContext<AgentSchemaEmitterOptions>
): void {
  const resolvedOutput = outputDir
    ? resolve(process.cwd(), outputDir)
    : context?.emitterOutputDir;
  const resolvedTest = testDir
    ? resolve(process.cwd(), testDir)
    : undefined;

  for (const dir of [resolvedOutput, resolvedTest].filter(Boolean)) {
    try {
      execSync(`rustfmt ${dir}/*.rs`, {
        stdio: 'pipe',
        encoding: 'utf-8'
      });
    } catch {
      // rustfmt is optional
    }
  }
}

/**
 * Build context for rendering a single Rust struct.
 */
function buildClassContext(
  node: TypeNode,
  polymorphicTypeNames: Set<string>
): RustClassContext {
  const alternates = prepareAlternates(node);
  return {
    node,
    typeMapper: rustTypeMapper,
    alternates,
    polymorphicTypes: node.retrievePolymorphicTypes(),
    polymorphicTypeNames: Array.from(polymorphicTypeNames),
    shorthandProperty: getShorthandProperty(node),
    hasAlternates: alternates.length > 0,
  };
}

/**
 * Build context for rendering a Rust file with a base type and its children.
 */
function buildFileContext(
  node: TypeNode,
  packageName: string,
  polymorphicTypeNames: Set<string>
): RustFileContext {
  const classes: RustClassContext[] = [
    buildClassContext(node, polymorphicTypeNames),
    ...node.childTypes.map(ct => buildClassContext(ct, polymorphicTypeNames))
  ];

  return {
    containsAbstract: node.isAbstract || node.childTypes.some(c => c.isAbstract),
    classes,
    typeMapper: rustTypeMapper,
    packageName,
    polymorphicTypeNames: Array.from(polymorphicTypeNames),
  };
}

/**
 * Build context for rendering a test file.
 */
function buildTestContext(node: TypeNode, packageName: string): BaseTestContext {
  return buildBaseTestContext(node, packageName, rustTestOptions);
}

/**
 * Get the shorthand property name from alternates (snake_case).
 */
function getShorthandProperty(node: TypeNode): string | null {
  if (!node.alternates || node.alternates.length === 0) return null;

  for (const alt of node.alternates) {
    for (const [key, value] of Object.entries(alt.expansion)) {
      if (value === "{value}") {
        return toSnakeCase(key);
      }
    }
  }
  return null;
}

/**
 * Prepare alternate representations for Rust template rendering.
 */
function prepareAlternates(node: TypeNode): RustAlternate[] {
  if (!node.alternates || node.alternates.length === 0) return [];

  return node.alternates.map((alt, idx) => {
    const rustScalar = rustTypeMapper[alt.scalar] || "serde_json::Value";

    // Derive a variant name: Short for first, ShortN for duplicates
    const variantName = idx === 0 ? 'Short' : `Short${idx + 1}`;

    // Build field list from expansion
    const fields = Object.entries(alt.expansion).map(([key, val]) => ({
      rustKey: toSnakeCase(key),
      jsonKey: key,
      isValue: val === "{value}",
      literalValue: val !== "{value}" ? String(val) : undefined,
    }));

    const valueField = fields.find(f => f.isValue)?.rustKey ?? null;

    return { rustScalar, variantName, fields, valueField };
  });
}

/**
 * Write generated Rust content to file.
 */
async function emitRustFile(
  context: EmitContext<AgentSchemaEmitterOptions>,
  filename: string,
  content: string,
  outputDir?: string
): Promise<void> {
  outputDir = outputDir || `${context.emitterOutputDir}/rust`;
  const filePath = resolvePath(outputDir, filename);

  await emitFile(context.program, {
    path: filePath,
    content,
  });
}
