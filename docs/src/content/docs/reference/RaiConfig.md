---
title: "RaiConfig"
description: "Documentation for the RaiConfig type."
slug: "reference/raiconfig"
---

Responsible AI (content safety) configuration applied to an agent.
Associates the agent with a Responsible AI policy that governs content
filtering and safety guardrails for prompts and completions.

## Class Diagram

```mermaid
---
title: RaiConfig
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class RaiConfig {
      
        +string raiPolicyName
    }
```

## Yaml Example

```yaml
raiPolicyName: /subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2
```

## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| raiPolicyName | string | Full ARM resource ID of the Responsible AI policy on the Cognitive Services account (e.g., &#39;/subscriptions/&lt;sub&gt;/resourceGroups/&lt;rg&gt;/providers/Microsoft.CognitiveServices/accounts/&lt;account&gt;/raiPolicies/&lt;policyName&gt;&#39;). |
