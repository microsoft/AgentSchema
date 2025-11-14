---
title: "ModelResource"
description: "Documentation for the ModelResource type."
slug: "reference/modelresource"
---

Represents a model resource required by the agent

## Class Diagram

```mermaid
---
title: ModelResource
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
    Resource <|-- ModelResource
    class ModelResource {
      
        +string kind
        +string id
    }
```



## Yaml Example

```yaml
kind: model
id: gpt-4o

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | The kind identifier for model resources  |
| id | string | The unique identifier of the model resource  |








