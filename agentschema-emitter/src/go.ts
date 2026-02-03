import { EmitContext, emitFile, resolvePath } from "@typespec/compiler";
import { execSync } from "child_process";
import { existsSync } from "fs";
import { dirname, resolve } from "path";
import { EmitTarget, AgentSchemaEmitterOptions } from "./lib.js";
import {
  enumerateTypes,
  PropertyNode,
  TypeNode,
} from "./ast.js";
import { getCombinations, scalarValue } from "./utilities.js";
import { createTemplateEngine } from "./template-engine.js";
import * as YAML from "yaml";

/**
 * Type mapping from TypeSpec scalar types to Go types.
 */
export const goTypeMapper: Record<string, string> = {
  "string": "string",
  "number": "float64",
  "array": "[]",
  "object": "map[string]interface{}",
  "boolean": "bool",
  "int64": "int64",
  "int32": "int32",
  "float64": "float64",
  "float32": "float32",
  "integer": "int",
  "float": "float64",
  "numeric": "float64",
  "any": "interface{}",
  "dictionary": "map[string]interface{}",
};

/**
 * Go context interfaces for template rendering
 */
interface GoClassContext {
  node: TypeNode;
  typeMapper: Record<string, string>;
  alternates: Array<{ scalar: string; alternate: string }>;
  polymorphicTypes: any;
  imports: string[];
  collectionTypes: Array<{ prop: PropertyNode; type: string[] }>;
  shorthandProperty: string | null;
}

interface GoFileContext {
  containsAbstract: boolean;
  imports: string[];
  classes: GoClassContext[];
  typeMapper: Record<string, string>;
  packageName: string;
}

interface GoTestContext {
  node: TypeNode;
  examples: Array<{
    json: string[];
    yaml: string[];
    validation: Array<{ key: string; value: any; delimeter: string }>;
  }>;
  alternates: Array<{
    title: string;
    scalar: string;
    value: string;
    validation: Array<{ key: string; value: any; delimeter: string }>;
  }>;
  packageName: string;
}

interface GoContextContext {
  header: string;
  packageName: string;
}

/**
 * Escape a string for use in Go string literals.
 */
function escapeGoString(str: string): string {
  return str
    .replace(/\\/g, '\\\\')
    .replace(/"/g, '\\"')
    .replace(/\n/g, '\\n')
    .replace(/\r/g, '\\r')
    .replace(/\t/g, '\\t');
}

/**
 * Main entry point for Go code generation.
 */
export const generateGo = async (
  context: EmitContext<AgentSchemaEmitterOptions>,
  templateDir: string,
  node: TypeNode,
  emitTarget: EmitTarget
): Promise<void> => {
  // Create template engine with Go templates + shared macros
  const engine = createTemplateEngine(templateDir, 'go');

  const nodes = Array.from(enumerateTypes(node));

  // Determine package name from root node namespace (e.g., "AgentSchema" -> "agentschema")
  const packageName = node.typeName.namespace.toLowerCase().replace(/\./g, '');

  // Render context file (LoadContext/SaveContext utilities)
  const contextContext = buildContextContext(packageName);
  const contextContent = engine.render('context.go.njk', contextContext);
  await emitGoFile(context, 'context.go', contextContent, emitTarget["output-dir"]);

  // Render each base type and its children as a single file
  for (const n of nodes) {
    // Skip child types - they're rendered with their parent
    if (!n.base) {
      const fileContext = buildFileContext(n, packageName);
      const fileContent = engine.render('file.go.njk', fileContext);
      const fileName = toSnakeCase(n.typeName.name) + '.go';
      await emitGoFile(context, fileName, fileContent, emitTarget["output-dir"]);
    }

    // Render test file for each type
    if (emitTarget["test-dir"]) {
      const testContext = buildTestContext(n, packageName);
      const testContent = engine.render('test.go.njk', testContext);
      const testFileName = toSnakeCase(n.typeName.name) + '_test.go';
      await emitGoFile(context, testFileName, testContent, emitTarget["test-dir"]);
    }
  }

  // Format emitted files if format option is enabled (default: true)
  if (emitTarget.format !== false) {
    const outputDir = emitTarget["output-dir"]
      ? resolve(process.cwd(), emitTarget["output-dir"])
      : context.emitterOutputDir;
    const testDir = emitTarget["test-dir"]
      ? resolve(process.cwd(), emitTarget["test-dir"])
      : undefined;

    formatGoFiles(outputDir, testDir);
  }
};

/**
 * Format Go files using gofmt and goimports.
 */
function formatGoFiles(outputDir: string, testDir?: string): void {
  const dirs = [outputDir, ...(testDir ? [testDir] : [])];

  for (const dir of dirs) {
    // Run gofmt
    try {
      execSync(`gofmt -w "${dir}"`, {
        stdio: 'pipe',
        encoding: 'utf-8'
      });
    } catch (error) {
      console.warn(`Warning: gofmt formatting failed for ${dir}. You may need to install Go.`);
    }

    // Run goimports if available
    try {
      execSync(`goimports -w "${dir}"`, {
        stdio: 'pipe',
        encoding: 'utf-8'
      });
    } catch (error) {
      // goimports is optional, don't warn if not available
    }
  }
}

/**
 * Build context for rendering a single Go struct.
 */
function buildClassContext(node: TypeNode): GoClassContext {
  return {
    node,
    typeMapper: goTypeMapper,
    alternates: prepareAlternates(node),
    polymorphicTypes: node.retrievePolymorphicTypes(),
    imports: getUniqueImportTypes(node),
    collectionTypes: getCollectionTypes(node),
    shorthandProperty: getShorthandProperty(node),
  };
}

/**
 * Build context for rendering a Go file with a base type and its children.
 */
function buildFileContext(node: TypeNode, packageName: string): GoFileContext {
  const classes: GoClassContext[] = [
    buildClassContext(node),
    ...node.childTypes.map(ct => buildClassContext(ct))
  ];

  return {
    containsAbstract: node.isAbstract || node.childTypes.some(c => c.isAbstract),
    imports: getUniqueImportTypes(node),
    classes,
    typeMapper: goTypeMapper,
    packageName,
  };
}

/**
 * Build context for rendering a test file.
 */
function buildTestContext(node: TypeNode, packageName: string): GoTestContext {
  // Get sample properties and generate combinations
  const samples = node.properties
    .filter(p => p.samples && p.samples.length > 0)
    .map(p => p.samples?.map(s => ({ ...s.sample })));

  const combinations = samples.length > 0 ? getCombinations(samples) : [];

  // Flatten combinations into test examples
  const examples = combinations.map(c => {
    const sample = Object.assign({}, ...c);
    return {
      json: JSON.stringify(sample, null, 2).split('\n'),
      yaml: YAML.stringify(sample, { indent: 2 }).split('\n'),
      validation: Object.keys(sample)
        .filter(key => typeof sample[key] !== 'object')
        .map(key => ({
          key: toPascalCase(key),
          value: typeof sample[key] === 'boolean'
            ? (sample[key] ? "true" : "false")
            : (typeof sample[key] === 'string' ? escapeGoString(sample[key]) : sample[key]),
          delimeter: typeof sample[key] === 'string' ? '"' : '',
        })),
    };
  });

  // Prepare alternate test cases
  const alternates = node.alternates.map(alt => {
    const example = alt.example
      ? (typeof alt.example === "string" ? '"' + alt.example + '"' : alt.example.toString())
      : scalarValue[alt.scalar] || "nil";

    return {
      title: alt.title || alt.scalar,
      scalar: alt.scalar,
      value: example,
      validation: Object.keys(alt.expansion)
        .filter(key => typeof alt.expansion[key] !== 'object')
        .map(key => {
          const value = alt.expansion[key] === "{value}" ? example : alt.expansion[key];
          const needsQuotes = typeof value === 'string' && !value.includes('"') && alt.expansion[key] !== "{value}";
          return {
            key: toPascalCase(key),
            value: needsQuotes ? escapeGoString(value) : value,
            delimeter: needsQuotes ? '"' : '',
          };
        }),
    };
  });

  return {
    node,
    examples,
    alternates,
    packageName,
  };
}

/**
 * Build context for rendering the context.go file.
 */
function buildContextContext(packageName: string): GoContextContext {
  return {
    header: "AgentSchema Context",
    packageName,
  };
}

/**
 * Prepare alternate representations for template rendering.
 */
function prepareAlternates(node: TypeNode): Array<{ scalar: string; alternate: string }> {
  if (!node.alternates || node.alternates.length === 0) {
    return [];
  }

  return node.alternates.map(alt => ({
    scalar: goTypeMapper[alt.scalar],
    alternate: JSON.stringify(alt.expansion, null, '')
      .replaceAll('\n', '')
      .replaceAll('"{value}"', 'data'),
  }));
}

/**
 * Get the shorthand property name from alternates.
 */
function getShorthandProperty(node: TypeNode): string | null {
  if (!node.alternates || node.alternates.length === 0) {
    return null;
  }

  for (const alt of node.alternates) {
    for (const [key, value] of Object.entries(alt.expansion)) {
      if (value === "{value}") {
        return key;
      }
    }
  }
  return null;
}

/**
 * Get collection properties with their nested type info.
 */
function getCollectionTypes(node: TypeNode): Array<{ prop: PropertyNode; type: string[] }> {
  return node.properties
    .filter(p => p.isCollection && !p.isScalar && !p.isDict)
    .map(p => ({
      prop: p,
      type: p.type?.properties.filter(t => t.name !== "name").map(t => t.name) || [],
    }));
}

/**
 * Get unique import types needed from other modules.
 */
function getUniqueImportTypes(node: TypeNode): string[] {
  const imports = [
    node.properties.filter(p => !p.isScalar && !p.isDict).map(p => p.typeName.name),
    ...node.childTypes.flatMap(c =>
      c.properties.filter(p => !p.isScalar && !p.isDict).map(p => p.typeName.name)
    )
  ].flat().filter(n => n !== node.typeName.name && node.base?.name !== n);

  return Array.from(new Set(imports)).sort();
}

/**
 * Write generated Go content to file.
 */
async function emitGoFile(
  context: EmitContext<AgentSchemaEmitterOptions>,
  filename: string,
  content: string,
  outputDir?: string
): Promise<void> {
  outputDir = outputDir || `${context.emitterOutputDir}/go`;
  const filePath = resolvePath(outputDir, filename);

  await emitFile(context.program, {
    path: filePath,
    content,
  });
}

/**
 * Convert PascalCase to snake_case.
 */
function toSnakeCase(str: string): string {
  return str
    .replace(/([a-z])([A-Z])/g, "$1_$2")
    .replace(/([A-Z]+)([A-Z][a-z])/g, "$1_$2")
    .toLowerCase();
}

/**
 * Convert snake_case or camelCase to PascalCase.
 */
function toPascalCase(str: string): string {
  return str
    .split(/[_\-]/)
    .map(part => part.charAt(0).toUpperCase() + part.slice(1))
    .join('');
}
