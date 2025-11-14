---
title: "FunctionTool"
description: "Documentation for the FunctionTool type."
slug: "reference/functiontool"
---

Represents a local function tool.

## Class Diagram

```mermaid
---
title: FunctionTool
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class Tool {
        +string name
        +string kind
        +string description
        +Binding[] bindings
    }
    Tool <|-- FunctionTool
    class FunctionTool {
      
        +string kind
        +PropertySchema parameters
        +boolean strict
    }
    class PropertySchema {
        +dictionary[] examples
        +boolean strict
        +Property[] properties
    }
    FunctionTool *-- PropertySchema
```



## Yaml Example

```yaml
kind: function
parameters:
  properties:
    firstName:
      kind: string
      value: Jane
    lastName:
      kind: string
      value: Doe
    question:
      kind: string
      value: What is the meaning of life?
strict: true

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | The kind identifier for function tools  |
| parameters | [PropertySchema](PropertySchema.md) | Parameters accepted by the function tool  |
| strict | boolean | Indicates whether the function tool enforces strict validation on its parameters  |







## Composed Types
The following types are composed within `FunctionTool`:

- [PropertySchema](/reference/propertyschema)


