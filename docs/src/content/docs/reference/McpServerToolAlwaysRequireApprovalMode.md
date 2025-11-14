---
title: "McpServerToolAlwaysRequireApprovalMode"
description: "Documentation for the McpServerToolAlwaysRequireApprovalMode type."
slug: "reference/mcpservertoolalwaysrequireapprovalmode"
---



## Class Diagram

```mermaid
---
title: McpServerToolAlwaysRequireApprovalMode
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
    McpServerApprovalMode <|-- McpServerToolAlwaysRequireApprovalMode
    class McpServerToolAlwaysRequireApprovalMode {
      
        +string kind
    }
```



## Yaml Example

```yaml
kind: always

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | The kind identifier for always approval mode  |








