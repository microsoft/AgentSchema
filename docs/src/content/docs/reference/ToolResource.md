---
title: "ToolResource"
description: "Documentation for the ToolResource type."
slug: "reference/toolresource"
---

Represents a tool resource required by the agent

## Class Diagram

```mermaid
---
title: ToolResource
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class Resource {
        +string name
        +string kind
    }
    Resource <|-- ToolResource
    class ToolResource {
      
        +string kind
        +string id
        +dictionary options
    }
```



## Yaml Example

```yaml
kind: tool
id: web-search
options:
  myToolResourceProperty: myValue

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | The kind identifier for tool resources  |
| id | string | The unique identifier of the tool resource  |
| options | dictionary | Configuration options for the tool resource  |








