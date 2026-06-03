---
title: "AgentDefinition"
description: "Documentation for the AgentDefinition type."
slug: "reference/agentdefinition"
---

The following is a specification for defining AI agents with structured metadata, inputs, outputs, tools, and templates.
It provides a way to create reusable and composable AI agents that can be executed with specific configurations.
The specification includes metadata about the agent, model configuration, input parameters, expected outputs,
available tools, and template configurations for prompt rendering.

## Class Diagram

```mermaid
---
title: AgentDefinition
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class AgentDefinition {
      <<abstract>>
        +string kind
        +string name
        +string displayName
        +string description
        +dictionary metadata
        +PropertySchema inputSchema
        +PropertySchema outputSchema
        +Policy[] policies
    }
    class PromptAgent {
        +string kind
        +Model model
        +Tool[] tools
        +Template template
        +string instructions
        +string additionalInstructions
    }
    AgentDefinition <|-- PromptAgent
    class Workflow {
        +string kind
        +dictionary trigger
    }
    AgentDefinition <|-- Workflow
    class ContainerAgent {
        +string kind
        +ProtocolVersionRecord[] protocols
        +string image
        +string dockerfilePath
        +ContainerResources resources
        +EnvironmentVariable[] environmentVariables
        +CodeConfiguration codeConfiguration
    }
    AgentDefinition <|-- ContainerAgent
    class Policy {
        +string type
        +string raiPolicyName
    }
    AgentDefinition *-- Policy
```

## Yaml Example

```yaml
kind: prompt
name: basic-prompt
displayName: Basic Prompt Agent
description: A basic prompt that uses the GPT-3 chat API to answer questions
metadata:
  authors:
    - sethjuarez
    - jietong
  tags:
    - example
    - prompt
inputSchema:
  properties:
    firstName:
      kind: string
      value: Jane
    lastName:
      kind: string
      value: Doe
    question:
      kind: string
      value: What is the meaning of life?
outputSchema:
  properties:
    answer:
      kind: string
      description: The answer to the user's question.
policies:
  - type: rai_policy
    raiPolicyName: /subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2
```

## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| kind | string | Kind represented by the document |
| name | string | Human-readable name of the agent |
| displayName | string | Display name of the agent for UI purposes |
| description | string | Description of the agent&#39;s capabilities and purpose |
| metadata | dictionary | Additional metadata including authors, tags, and other arbitrary properties |
| inputSchema | [PropertySchema](../propertyschema/) | Input parameters that participate in template rendering |
| outputSchema | [PropertySchema](../propertyschema/) | Expected output format and structure from the agent |
| policies | [Policy[]](../policy/) | Safety and governance policies for the agent, such as Responsible AI content-safety configuration. |

## Child Types

The following types extend `AgentDefinition`:

- [PromptAgent](../promptagent/)
- [Workflow](../workflow/)
- [ContainerAgent](../containeragent/)

## Composed Types

The following types are composed within `AgentDefinition`:

- [Policy](../policy/)
