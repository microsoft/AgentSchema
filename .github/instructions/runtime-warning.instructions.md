---
applyTo: "runtime/**/*"
description: "CRITICAL: Runtime files are auto-generated - do not edit directly"
---

# ⚠️ AUTO-GENERATED FILES - DO NOT EDIT

Files in `runtime/` are **automatically generated** by the TypeSpec emitter.

## If You Need to Change Runtime Code

1. **Find the source**: Changes must be made in `agentschema-emitter/src/templates/`
2. **Edit the template**: Modify the appropriate `.njk` template file
3. **Rebuild**:
   ```bash
   cd agentschema-emitter && npx tsc && cp -r src/templates dist/src/
   cd ../agentschema && npm run generate
   ```
4. **Verify**: Run tests in the affected runtime

## Template Locations

| Runtime    | Template Directory                              |
| ---------- | ----------------------------------------------- |
| C#         | `agentschema-emitter/src/templates/csharp/`     |
| Python     | `agentschema-emitter/src/templates/python/`     |
| TypeScript | `agentschema-emitter/src/templates/typescript/` |

## Common Template Files

- `file.cs.njk` / `file.py.njk` / `file.ts.njk` / `file.go.njk` - Main class definitions
- `test.njk` / `test.py.njk` / `test.ts.njk` - Test file generation
- `_macros.njk` - Shared template macros

## Exception: Non-Generated Files

These files in `runtime/` are NOT generated and CAN be edited:

- `runtime/csharp/AgentSchema.sln`
- `runtime/csharp/AgentSchema/AgentSchema.csproj`
- `runtime/python/agentschema/pyproject.toml`
- `runtime/typescript/agentschema/package.json`
- `runtime/typescript/agentschema/tsconfig.json`

