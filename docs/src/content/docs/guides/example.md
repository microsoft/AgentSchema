---
title: AgentManifest vs AgentDefinition
description: Understanding the key differences between AgentManifest and AgentDefinition in AgentSchema.
---

AgentSchema provides two primary ways to define agents: **AgentDefinition** and **AgentManifest**. Understanding the difference between these is crucial for effectively using the specification.

## What You'll Learn

- The core differences between AgentManifest and AgentDefinition
- When to use each approach
- How they work together for dynamic agent creation
- Practical examples of both formats

## AgentDefinition: Ready-to-Execute Agents

An **AgentDefinition** is a complete, concrete specification of an agent that can be executed directly. It contains all the necessary information to run the agent immediately.

### Key Characteristics

- **Complete Configuration**: All values are concrete and specified
- **Immediate Execution**: Can be run without any additional parameters
- **Static**: Values are fixed at definition time
- **Direct Usage**: Suitable for single-purpose agents

### AgentDefinition Example

```yaml
kind: prompt
name: customer-support
displayName: "Customer Support Agent"
description: "Handles customer inquiries and support requests"

model: gpt-4o

instructions: |
  You are a helpful customer support agent. Provide clear, 
  professional responses to customer inquiries.

tools:
  knowledge_base:
    kind: function
    description: "Search company knowledge base"
    parameters:
      query:
        kind: string
        description: "Search query"
        required: true
```

## AgentManifest: Parameterized Agent Templates

An **AgentManifest** is a template for creating agents dynamically. It includes parameters that can be substituted to create different variations of the same agent pattern.

### Key Features

- **Parameterized**: Uses `{{parameter}}` syntax for dynamic values
- **Template-Based**: Contains an AgentDefinition as a template
- **Configurable**: Parameters can be set at runtime
- **Reusable**: One manifest can create multiple agent instances
- **Resource Management**: Defines required resources separately

### AgentManifest Example

```yaml
name: customer-support-template
displayName: "Customer Support Template"
description: "Configurable customer support agent template"

template:
  kind: prompt
  name: "{{agent_name}}"
  displayName: "{{display_name}}"
  description: "{{agent_description}}"
  
  model: "{{model_name}}"
  
  instructions: |
    You are a {{agent_role}}. {{custom_instructions}}
    
  tools:
    knowledge_base:
      kind: function
      description: "Search {{company_name}} knowledge base"
      connection: "{{kb_connection}}"

parameters:
  properties:
    - name: agent_name
      kind: string
      description: "Internal agent identifier"
      required: true
    - name: display_name
      kind: string
      description: "Human-readable agent name"
      required: true
    - name: agent_description
      kind: string
      description: "Agent description"
      required: true
    - name: model_name
      kind: string
      value: "gpt-4o"
      description: "AI model to use"
    - name: agent_role
      kind: string
      value: "customer support agent"
      description: "Agent's role description"
    - name: custom_instructions
      kind: string
      description: "Additional instructions"
    - name: company_name
      kind: string
      required: true
      description: "Company name for personalization"
    - name: kb_connection
      kind: string
      required: true
      description: "Knowledge base connection identifier"

resources:
  - name: gpt-model
    kind: model
    id: "{{model_name}}"
  - name: knowledge-base-tool
    kind: tool
    id: function-tool
```

## Key Differences at a Glance

| Aspect | AgentDefinition | AgentManifest |
|--------|----------------|---------------|
| **Purpose** | Direct execution | Template for creation |
| **Values** | Concrete, fixed | Parameterized with `{{}}` |
| **Usage** | Single-use agent | Multiple agent variations |
| **Configuration** | Static | Dynamic at runtime |
| **Structure** | Self-contained | Template + Parameters + Resources |
| **Flexibility** | Limited | High |

## When to Use Each

### Use AgentDefinition When

- Creating a specific, single-purpose agent
- All configuration values are known and fixed
- No need for parameterization or reusability
- Simple, straightforward agent requirements

### Use AgentManifest When

- Creating reusable agent templates
- Need to deploy similar agents with different configurations
- Want to parameterize connections, models, or instructions
- Building a catalog of configurable agents
- Need to manage resources separately from agent logic

## Parameter Substitution

AgentManifest uses `{{parameter_name}}` syntax for substitution. When parameters are provided, the manifest is "projected" into a concrete AgentDefinition:

```yaml
# In manifest template
model: "{{model_name}}"
instructions: "You are a {{agent_role}} for {{company_name}}"

# With parameters
parameters:
  model_name: "gpt-4o"
  agent_role: "sales assistant"
  company_name: "Acme Corp"

# Becomes in projected AgentDefinition
model: "gpt-4o"
instructions: "You are a sales assistant for Acme Corp"
```

## Next Steps

- Explore [AgentDefinition Reference](/reference/agentdefinition) for complete specification details
- Review [AgentManifest Reference](/reference/agentmanifest) for template syntax
- Check out the [Examples Repository](https://github.com/microsoft/AgentSchema/tree/main/examples) for real-world usage patterns
- Learn about [PromptAgent](/reference/promptagent), [ContainerAgent](/reference/containeragent), and [Workflow](/reference/workflow) types
