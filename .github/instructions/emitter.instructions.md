---
applyTo: "agentschema-emitter/**/*.ts"
description: "Instructions for editing the TypeSpec emitter code"
---

# Emitter Development Guidelines

## Architecture

The emitter transforms TypeSpec AST into runtime code for C#, Python, and TypeScript using:

- `ast.ts` - TypeSpec model traversal and TypeNode/PropertyNode structures
- `{language}.ts` - Language-specific code generators
- `templates/{language}/*.njk` - Nunjucks templates for code output
- `template-engine.ts` - Template rendering utilities

## Key Files

- `emitter.ts` - Entry point, dispatches to language generators
- `ast.ts` - `TypeNode`, `PropertyNode`, `enumerateTypes()`, `resolveModel()`
- `decorators.ts` - Custom decorator handling (`@sample`, `@shorthand`, `@abstract`)
- `utilities.ts` - Helper functions like `getCombinations()`, `scalarValue`

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

Always test all three runtimes after emitter changes:

```bash
cd runtime/csharp && dotnet test
cd runtime/python/agentschema && uv run pytest tests/
cd runtime/typescript/agentschema && npm test
```

