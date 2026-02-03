---
applyTo: "schemas/**/*"
description: "CRITICAL: Schema files are auto-generated - do not edit directly"
---

# ⚠️ AUTO-GENERATED FILES - DO NOT EDIT

Files in `schemas/` are **automatically generated** by the `@typespec/json-schema` emitter.

## If You Need to Change Schema Definitions

1. Edit the TypeSpec source files in `agentschema/model/*.tsp`
2. Regenerate: `cd agentschema && npm run generate`
3. Schema files will be updated in `schemas/v1.0/`

## Schema Generation Configuration

Configured in `agentschema/tspconfig.yaml`:

```yaml
"@typespec/json-schema":
  emitter-output-dir: "{cwd}/../schemas/v1.0"
  emitAllModels: true
  emitAllRefs: true
  file-type: yaml
```

