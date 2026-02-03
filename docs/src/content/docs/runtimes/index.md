---
title: Runtimes
description: Official runtime libraries for working with AgentSchema in your preferred programming language.
---

AgentSchema provides official runtime libraries for multiple programming languages, making it easy to load, create, and manipulate agent definitions programmatically.

## Available Runtimes

| Language | Package | Status |
|----------|---------|--------|
| [TypeScript/JavaScript](typescript/) | [`agentschema`](https://www.npmjs.com/package/agentschema) | Beta |
| [Python](python/) | [`agentschema`](https://pypi.org/project/agentschema/) | Beta |
| [C# / .NET](csharp/) | [`AgentSchema`](https://www.nuget.org/packages/AgentSchema/) | Beta |

## Common Features

All runtimes provide a consistent set of features:

### Loading Agent Definitions

Load agent definitions from YAML or JSON files:

- **`fromYaml()`** - Parse a YAML string into an agent definition
- **`fromJson()`** - Parse a JSON string into an agent definition
- **`load()`** - Load from a dictionary/object

### Saving Agent Definitions

Export agent definitions to various formats:

- **`toYaml()`** - Convert to a YAML string
- **`toJson()`** - Convert to a JSON string
- **`save()`** - Convert to a dictionary/object

### Programmatic Construction

Create agent definitions using strongly-typed classes:

```python
# Python example
agent = AgentDefinition(
    name="my-agent",
    model="gpt-4o",
    instructions="You are a helpful assistant."
)
```

### Context Objects

Customize loading and saving behavior:

- **`LoadContext`** - Pre/post processing during load operations
- **`SaveContext`** - Control output format (shorthand, collection format)

## Choosing a Runtime

Choose the runtime that matches your development environment:

- **TypeScript/JavaScript** - For Node.js applications, serverless functions, or web applications
- **Python** - For data science workflows, AI/ML pipelines, or Python-based backends
- **C# / .NET** - For enterprise applications, Azure Functions, or .NET-based services

## Getting Started

Select your language to get started:

- [TypeScript Runtime →](typescript/)
- [Python Runtime →](python/)
- [C# Runtime →](csharp/)
