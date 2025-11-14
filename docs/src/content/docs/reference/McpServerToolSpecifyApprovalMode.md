---
title: "McpServerToolSpecifyApprovalMode"
description: "Documentation for the McpServerToolSpecifyApprovalMode type."
slug: "reference/mcpservertoolspecifyapprovalmode"
---



## Class Diagram

```mermaid
---
title: McpServerToolSpecifyApprovalMode
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
    McpServerApprovalMode <|-- McpServerToolSpecifyApprovalMode
    class McpServerToolSpecifyApprovalMode {
      
        +string kind
        +string[] alwaysRequireApprovalTools
        +string[] neverRequireApprovalTools
    }
```



## Yaml Example

```yaml
kind: specify
alwaysRequireApprovalTools:
  - operation1
neverRequireApprovalTools:
  - operation2

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | The kind identifier for specify approval mode  |
| alwaysRequireApprovalTools | string[] | List of tools that always require approval  |
| neverRequireApprovalTools | string[] | List of tools that never require approval  |








