---
title: "ProtocolVersionRecord"
description: "Documentation for the ProtocolVersionRecord type."
slug: "reference/protocolversionrecord"
---



## Class Diagram

```mermaid
---
title: ProtocolVersionRecord
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class ProtocolVersionRecord {
      
        +string protocol
        +string version
    }
```



## Yaml Example

```yaml
protocol: responses
version: v0.1.1

```




## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| protocol | string | The protocol type.  |
| version | string | The version string for the protocol, e.g. &#39;v0.1.1&#39;.  |








