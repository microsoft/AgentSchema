---
applyTo: "agentschema-emitter/**/*.ts"
description: "Instructions for editing the TypeSpec emitter code"
---

# Emitter Development Guidelines

## Architecture

The emitter transforms TypeSpec AST into runtime code for C#, Python, TypeScript, and Go using:

- `ast.ts` - TypeSpec model traversal and TypeNode/PropertyNode structures
- `{language}.ts` - Language-specific code generators
- `templates/{language}/*.njk` - Nunjucks templates for code output
- `template-engine.ts` - Template rendering utilities
- `test-context.ts` - **Shared test context builder for standardized test generation**

## Key Files

- `emitter.ts` - Entry point, dispatches to language generators
- `ast.ts` - `TypeNode`, `PropertyNode`, `enumerateTypes()`, `resolveModel()`, `BaseTestContext`
- `test-context.ts` - `buildBaseTestContext()`, language-specific options (`pythonTestOptions`, etc.)
- `decorators.ts` - Custom decorator handling (`@sample`, `@shorthand`, `@abstract`)
- `utilities.ts` - Helper functions like `getCombinations()`, `scalarValue`

## Test Generation

All language generators use the shared `buildBaseTestContext()` function from `test-context.ts` to ensure consistent test generation:

```typescript
import { buildBaseTestContext, pythonTestOptions } from "./test-context.js";

function buildTestContext(
  node: TypeNode,
  packageName: string,
): BaseTestContext {
  return buildBaseTestContext(node, packageName, pythonTestOptions);
}
```

### Standardized Test Context Interface

All test templates receive a `BaseTestContext` with these fields:

- `node` - The TypeNode being tested
- `isAbstract` - Whether to skip direct instantiation tests
- `package` - Package/namespace name for imports
- `examples` - Array of `TestExample` from `@sample` decorators
- `alternates` - Array of `AlternateTest` for shorthand representations

### Language-Specific Options

Each language defines a `TestContextOptions` preset:

- `pythonTestOptions` - snake_case keys, `True`/`False` booleans, triple-quote delimiters
- `goTestOptions` - PascalCase keys, Go string escaping
- `typescriptTestOptions` - camelCase keys, template string escaping
- C# uses inline context but follows the same `validations`/`delimiter` naming

## Template System

Templates use Nunjucks syntax. Key patterns:

- `{{ variable }}` - Output variable
- `{% for item in items %}` - Loops
- `{{ value | filter }}` - Filters (e.g., `| lower`, `| safe`)
- `{%- ... -%}` - Trim whitespace

## Type Mappers

Each language has a type mapper for scalar conversions:

```typescript
const typeMapper: Record<string, string> = {
  string: "str", // Python
  string: "string", // C#/TypeScript
  boolean: "bool", // Python/C#
  int32: "int",
  // etc.
};
```

## YAML Generation

Use proper options for clean YAML output:

```typescript
const doc = new YAML.Document(sample);
YAML.visit(doc, {
  Scalar(key, node) {
    if (typeof node.value === "string") {
      const str = node.value as string;
      if (str.includes("\n") || str.includes("#")) {
        node.type = "QUOTE_DOUBLE";
      }
    }
  },
});
doc.toString({ indent: 2, lineWidth: 0 });
```

## Test File Naming Conventions

Generated test files follow idiomatic naming for each language:

| Language   | Pattern                            | Example                              |
|------------|-------------------------------------|--------------------------------------|
| C#         | `{TypeName}ConversionTests.cs`      | `AgentDefinitionConversionTests.cs`  |
| Python     | `test_{type_name}.py`               | `test_agent_definition.py`           |
| TypeScript | `{type-name}.test.ts`               | `agent-definition.test.ts`           |
| Go         | `{type_name}_test.go`               | `agent_definition_test.go`           |

These conventions are implemented using shared utilities:

- `toSnakeCase()` - PascalCase → snake_case (Python, Go)
- `toKebabCase()` - PascalCase → kebab-case (TypeScript)

## Build Process

After editing ANY file in `agentschema-emitter/src/`:

```bash
cd agentschema-emitter
npx tsc
cp -r src/templates dist/src/   # CRITICAL: templates must be copied!
cd ../agentschema
npm run generate
```

## Testing Changes

Always test all four runtimes after emitter changes:

```bash
cd runtime/csharp && dotnet test
cd runtime/python/agentschema && uv run pytest tests/
cd runtime/typescript/agentschema && npm test
cd runtime/go/agentschema && go test ./...
```
