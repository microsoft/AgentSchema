---
applyTo: "agentschema-emitter/lib/model/**/*.tsp"
description: "Instructions for editing TypeSpec schema definitions"
---

# TypeSpec Schema Guidelines

## Namespace

Always use `namespace AgentSchema;` - never use `AgentSchema.Core` or other sub-namespaces.

## Decorators

- `@doc("description")` - Document all properties and models
- `@sample(#{ propertyName: value })` - Required for test generation, provide realistic example values
- `@shorthand(scalarType, #{ expansion }, "property", "title", "example")` - For alternate scalar representations
- `@abstract` - Mark base types that shouldn't be instantiated directly
- `@discriminator("propertyName")` - For polymorphic type discrimination

## Model Patterns

### Basic Model

```typespec
model MyModel {
  @doc("Description of property")
  @sample(#{ myProp: "example" })
  myProp: string;
}
```

### Polymorphic Model

```typespec
@abstract
@discriminator("kind")
model BaseModel {
  @doc("Type discriminator")
  @sample(#{ kind: "child" })
  kind: string;
}

model ChildModel extends BaseModel {
  @doc("Type discriminator")
  @sample(#{ kind: "child" })
  kind: "child";
}
```

### Optional Properties

```typespec
@doc("Optional property")
@sample(#{ optionalProp: "value" })
optionalProp?: string;
```

## Imports

- Always import `@agentschema/emitter` for decorators
- Import related `.tsp` files using relative paths: `import "./core.tsp";`

## After Editing

1. Run `cd agentschema && npm run generate` to regenerate all code
2. Run tests in all three runtimes to verify changes

