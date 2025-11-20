---
title: "WebSearchTool"
description: "Documentation for the WebSearchTool type."
slug: "reference/websearchtool"
---

The Bing search tool.

## Class Diagram

```mermaid
---
title: WebSearchTool
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
    Tool <|-- WebSearchTool
    class WebSearchTool {
      
        +string kind
        +Connection connection
        +dictionary options
    }
```



## Yaml Example

```yaml
kind: bing_search
connection:
  kind: reference
options:
  instanceName: MyBingInstance
  market: en-US
  setLang: en
  count: 10
  freshness: Day

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | The kind identifier for Bing search tools  |
| connection | [Connection](../connection/) | The connection configuration for the Bing search tool  |
| options | dictionary | The configuration options for the Bing search tool  |








