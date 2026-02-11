---
title: "ContainerScale"
description: "Documentation for the ContainerScale type."
slug: "reference/containerscale"
---

Scaling configuration for a containerized agent.

## Class Diagram

```mermaid
---
title: ContainerScale
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class ContainerScale {
      
        +int32 minReplicas
        +int32 maxReplicas
    }
```

## Yaml Example

```yaml
minReplicas: 1
maxReplicas: 3
```

## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| minReplicas | int32 | Minimum number of replicas |
| maxReplicas | int32 | Maximum number of replicas |
