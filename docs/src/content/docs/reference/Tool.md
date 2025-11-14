---
title: "Tool"
description: "Documentation for the Tool type."
slug: "reference/tool"
---

Represents a tool that can be used in prompts.

## Class Diagram

```mermaid
---
title: Tool
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class Tool {
      <<abstract>>
        +string name
        +string kind
        +string description
        +Binding[] bindings
    }
    class FunctionTool {
        +string kind
        +PropertySchema parameters
        +boolean strict
    }
    Tool <|-- FunctionTool
    class CustomTool {
        +string kind
        +Connection connection
        +dictionary options
    }
    Tool <|-- CustomTool
    class WebSearchTool {
        +string kind
        +Connection connection
        +dictionary options
    }
    Tool <|-- WebSearchTool
    class FileSearchTool {
        +string kind
        +Connection connection
        +string[] vectorStoreIds
        +int32 maximumResultCount
        +string ranker
        +float32 scoreThreshold
        +dictionary filters
    }
    Tool <|-- FileSearchTool
    class McpTool {
        +string kind
        +Connection connection
        +string serverName
        +string serverDescription
        +McpServerApprovalMode approvalMode
        +string[] allowedTools
    }
    Tool <|-- McpTool
    class OpenApiTool {
        +string kind
        +Connection connection
        +string specification
    }
    Tool <|-- OpenApiTool
    class CodeInterpreterTool {
        +string kind
        +string[] fileIds
    }
    Tool <|-- CodeInterpreterTool
    class Binding {
        +string name
        +string input
    }
    Tool *-- Binding
```



## Yaml Example

```yaml
name: my-tool
kind: function
description: A description of the tool
bindings:
  input: value

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | string | Name of the tool. If a function tool, this is the function name, otherwise it is the type  |
| kind | string | The kind identifier for the tool  |
| description | string | A short description of the tool for metadata purposes  |
| bindings | [Binding[]](Binding.md) | Tool argument bindings to input properties  |





## Child Types

The following types extend `Tool`:

- [FunctionTool](/reference/functiontool)
- [CustomTool](/reference/customtool)
- [WebSearchTool](/reference/websearchtool)
- [FileSearchTool](/reference/filesearchtool)
- [McpTool](/reference/mcptool)
- [OpenApiTool](/reference/openapitool)
- [CodeInterpreterTool](/reference/codeinterpretertool)



## Composed Types
The following types are composed within `Tool`:

- [Binding](/reference/binding)


