---
applyTo: "agentschema-emitter/src/templates/**/*.njk"
description: "Instructions for editing Nunjucks code generation templates"
---

# Nunjucks Template Guidelines

## Template Structure

Templates receive context objects from the language-specific generators. Common context includes:

- `node` - TypeNode with model information
- `examples` - Array of test examples with `json`, `yaml`, `validation` arrays
- `alternates` - Shorthand representation test cases
- `namespace` - Target namespace/package name
- `typeMapper` - Scalar type conversions

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

