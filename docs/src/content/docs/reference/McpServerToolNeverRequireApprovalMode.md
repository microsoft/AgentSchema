---
title: "McpServerToolNeverRequireApprovalMode"
description: "Documentation for the McpServerToolNeverRequireApprovalMode type."
slug: "reference/mcpservertoolneverrequireapprovalmode"
---



## Class Diagram

```mermaid
---
title: McpServerToolNeverRequireApprovalMode
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class McpServerApprovalMode {
        +string kind
    }
    McpServerApprovalMode <|-- McpServerToolNeverRequireApprovalMode
    class McpServerToolNeverRequireApprovalMode {
      
        +string kind
    }
```



## Yaml Example

```yaml
kind: never

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | The kind identifier for never approval mode  |








