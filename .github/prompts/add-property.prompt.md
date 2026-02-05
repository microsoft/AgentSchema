---
description: "Add a new property to an existing TypeSpec model"
---

# Add New Property Workflow

When adding a new property to a model:

## 1. Edit the TypeSpec File

Find the model in `agentschema-emitter/lib/model/*.tsp` and add:

```typespec
@doc("Description of the new property")
@sample(#{ newProperty: "example value" })
newProperty?: string;  // Use ? for optional
```

## 2. Consider These Questions

- Is it required or optional? (use `?` for optional)
- What's a good example value for tests?
- Does it need special serialization handling?
- Is it a scalar, object, or collection?

## 3. Regenerate and Test

```bash
cd agentschema-emitter && npm run generate
cd ../runtime/csharp && dotnet test
cd ../runtime/python/agentschema && uv run pytest tests/
cd ../runtime/typescript/agentschema && npm test
cd ../runtime/go/agentschema && go test ./...
```

## Property Type Patterns

| TypeSpec          | C#                            | Python           | TypeScript                | Go                    |
| ----------------- | ----------------------------- | ---------------- | ------------------------- | --------------------- |
| `string`          | `string`                      | `str`            | `string`                  | `string`              |
| `int32`           | `int`                         | `int`            | `number`                  | `int32`               |
| `float32`         | `float`                       | `float`          | `number`                  | `float32`             |
| `boolean`         | `bool`                        | `bool`           | `boolean`                 | `bool`                |
| `string[]`        | `IList<string>`               | `list[str]`      | `string[]`                | `[]string`            |
| `Record<unknown>` | `IDictionary<string, object>` | `dict[str, Any]` | `Record<string, unknown>` | `map[string]any`      |

