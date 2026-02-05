---
applyTo: "agentschema-emitter/src/templates/**/*.njk"
description: "Instructions for editing Nunjucks code generation templates"
---

# Nunjucks Template Guidelines

## Template Structure

Templates receive context objects from the language-specific generators. Common context includes:

- `node` - TypeNode with model information
- `isAbstract` - Whether this is a polymorphic base type (skip direct instantiation)
- `package` - Target namespace/package name
- `typeMapper` - Scalar type conversions

### Test Template Context (Standardized)

All test templates receive a `BaseTestContext` with standardized field names:

```typescript
interface BaseTestContext {
  node: TypeNode;
  isAbstract: boolean;
  package?: string;
  examples: TestExample[]; // From @sample decorators
  alternates: AlternateTest[]; // From @shorthand decorators
}

interface TestExample {
  json: string[]; // JSON lines
  yaml: string[]; // YAML lines
  validations: PropertyValidation[];
}

interface PropertyValidation {
  key: string; // Property name (in target casing)
  value: any; // Expected value
  delimiter: string; // Quote character(s) for strings
  isOptional: boolean; // Whether property is optional
}

interface AlternateTest {
  title: string; // Test name suffix
  scalarType: string; // The scalar type name
  value: string; // Scalar value literal
  validations: PropertyValidation[];
}
```

:::caution[Standardized Field Names]
Always use `validations` (plural), `delimiter` (correct spelling), and `scalarType`. These are consistent across all language templates.
:::

## Nunjucks Syntax

### Variables

```nunjucks
{{ node.typeName.name }}
{{ prop.name | lower }}
```

### Conditionals

```nunjucks
{% if prop.isOptional %}nullable{% endif %}
{% if not loop.first %}_{{ loop.index }}{% endif %}
```

### Loops

```nunjucks
{% for prop in node.properties %}
  {{ prop.name }}: {{ prop.typeName.name }}
{% endfor %}
```

### Whitespace Control

```nunjucks
{%- ... -%}   {# Trim both sides #}
{{- ... -}}   {# Trim both sides #}
{%- ... %}    {# Trim left only #}
```

### Filters

- `| lower` - Lowercase
- `| safe` - No escaping (use for code output)
- `| title` - Title case
- `| join(",")` - Join array

## Language-Specific Conventions

### C# Templates (`csharp/`)

- PascalCase for property names: `{{ renderName(prop.name) }}`
- Use `@"..."` for multiline strings
- Nullable types: `{{ type }}?`

### Python Templates (`python/`)

- snake_case property names (handled in Python code, not template)
- Use `"""..."""` for multiline strings
- Type hints: `{{ prop.name }}: {{ pythonType }}`

### TypeScript Templates (`typescript/`)

- camelCase property names
- Use backticks for template literals
- Optional properties: `{{ prop.name }}?: {{ type }}`

### Go Templates (`go/`)

- PascalCase for exported names: `{{ renderName(prop.name) }}`
- Struct tags for JSON/YAML: `` `json:"name" yaml:"name"` ``
- Pointer types for optional: `*{{ type }}`

## File Header Pattern

```nunjucks
{#
  Template Name
  =============
  Brief description of what this template generates.

  Expected context:
    - node: TypeNode
    - examples: Array<...>
#}
```

## After Editing Templates

Templates must be copied to `dist/` after changes:

```bash
cd agentschema-emitter
npx tsc
cp -r src/templates dist/src/
cd ../agentschema && npm run generate
```
