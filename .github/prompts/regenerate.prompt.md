---
description: "Regenerate all runtime code after TypeSpec or emitter changes"
---

# Regenerate Runtime Code

Run the full build and regeneration workflow:

1. Build the emitter (if emitter source was changed):

```bash
cd agentschema-emitter
npx tsc
cp -r src/templates dist/src/
```

2. Generate all runtime code:

```bash
cd agentschema
npm run generate
```

3. Run all tests to verify:

```bash
# C#
cd runtime/csharp && dotnet test

# Python
cd runtime/python/agentschema && uv run pytest tests/

# TypeScript
cd runtime/typescript/agentschema && npm test
```

