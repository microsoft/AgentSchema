---
description: "Add a new model type to the schema"
---

# Add New Model Workflow

## 1. Create or Edit TypeSpec File

In `agentschema-emitter/lib/model/`, create a new file or add to existing:

```typespec
import "agentschema-emitter";
import "./core.tsp";  // if using Named<> or Id<> templates

namespace AgentSchema;

/**
 * Documentation for the model.
 */
model MyNewModel {
  @doc("Property description")
  @sample(#{ myProp: "example" })
  myProp: string;

  @doc("Optional property")
  @sample(#{ optionalProp: 42 })
  optionalProp?: int32;
}
```

## 2. Import in main.tsp (if new file)

Edit `agentschema-emitter/lib/model/main.tsp`:

```typespec
import "./manifest.tsp";
import "./agent.tsp";
import "./container.tsp";
import "./mynewfile.tsp";  // Add your import
```

## 3. For Polymorphic Types

```typespec
@abstract
@discriminator("kind")
model BaseType {
  @doc("Type discriminator")
  @sample(#{ kind: "child1" })
  kind: string;
}

model ChildType1 extends BaseType {
  @doc("Type discriminator")
  @sample(#{ kind: "child1" })
  kind: "child1";

  // Child-specific properties
}
```

## 4. For Shorthand Support

Allow scalar initialization:

```typespec
@shorthand(string, #{ value: "{value}" })
model MyModel {
  @doc("The value")
  @sample(#{ value: "example" })
  value: string;
}
```

## 5. Regenerate and Test

```bash
cd agentschema && npm run generate
# Run all tests...
```
