---
title: "McpServerApprovalMode"
description: "Documentation for the McpServerApprovalMode type."
slug: "reference/mcpserverapprovalmode"
---

The approval mode for MCP server tools.

## Class Diagram

```mermaid
---
title: McpServerApprovalMode
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class McpServerApprovalMode {
      <<abstract>>
        +string kind
    }
    class McpServerToolAlwaysRequireApprovalMode {
        +string kind
    }
    McpServerApprovalMode <|-- McpServerToolAlwaysRequireApprovalMode
    class McpServerToolNeverRequireApprovalMode {
        +string kind
    }
    McpServerApprovalMode <|-- McpServerToolNeverRequireApprovalMode
    class McpServerToolSpecifyApprovalMode {
        +string kind
        +string[] alwaysRequireApprovalTools
        +string[] neverRequireApprovalTools
    }
    McpServerApprovalMode <|-- McpServerToolSpecifyApprovalMode
```



## Yaml Example

```yaml
kind: never

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | The kind identifier for string approval modes  |





## Child Types

The following types extend `McpServerApprovalMode`:

- [McpServerToolAlwaysRequireApprovalMode](/reference/mcpservertoolalwaysrequireapprovalmode)
- [McpServerToolNeverRequireApprovalMode](/reference/mcpservertoolneverrequireapprovalmode)
- [McpServerToolSpecifyApprovalMode](/reference/mcpservertoolspecifyapprovalmode)





## Alternate Constructions

The following alternate constructions are available for `McpServerApprovalMode`. 
These allow for simplified creation of instances using a single property.

### string kind
Mcp Approval Mode

The following simplified representation can be used:
```yaml
kind: "example"

```
This is equivalent to the full representation:
```yaml
kind:
  kind: "example"

```
