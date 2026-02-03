---
description: "Add a new property to an existing TypeSpec model"
---

# Add New Property Workflow

When adding a new property to a model:

## 1. Edit the TypeSpec File

Find the model in `agentschema/model/*.tsp` and add:

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
cd agentschema && npm run generate
cd ../runtime/csharp && dotnet test
cd ../runtime/python/agentschema && uv run pytest tests/
cd ../runtime/typescript/agentschema && npm test
```

## Property Type Patterns

| TypeSpec          | C#                            | Python           | TypeScript                |
| ----------------- | ----------------------------- | ---------------- | ------------------------- |
| `string`          | `string`                      | `str`            | `string`                  |
| `int32`           | `int`                         | `int`            | `number`                  |
| `float32`         | `float`                       | `float`          | `number`                  |
| `boolean`         | `bool`                        | `bool`           | `boolean`                 |
| `string[]`        | `IList<string>`               | `list[str]`      | `string[]`                |
| `Record<unknown>` | `IDictionary<string, object>` | `dict[str, Any]` | `Record<string, unknown>` |

