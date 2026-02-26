---
title: TypeScript Runtime
description: Official TypeScript/JavaScript runtime library for AgentSchema - load, create, and manipulate agent definitions.
---

The TypeScript runtime provides a strongly-typed interface for working with AgentSchema definitions in Node.js and browser environments.

## Installation

```bash
npm install agentschema
# or
yarn add agentschema
# or
pnpm add agentschema
```

## Requirements

- Node.js >= 18.0.0
- TypeScript >= 5.0 (for TypeScript users)

## Quick Start

### Loading Agent Definitions

Load agent definitions from YAML or JSON:

```typescript
import { AgentDefinition } from "agentschema";

// Load from YAML
const agent = AgentDefinition.fromYaml(`
name: my-agent
model: gpt-4o
instructions: You are a helpful assistant.
`);

// Load from JSON
const jsonAgent = AgentDefinition.fromJson(`{
  "name": "my-agent",
  "model": "gpt-4o",
  "instructions": "You are a helpful assistant."
}`);

console.log(agent.name); // "my-agent"
console.log(agent.model); // "gpt-4o"
```

### Creating Agents Programmatically

Build agent definitions using TypeScript classes:

```typescript
import { AgentDefinition, FunctionTool, Property } from "agentschema";

const agent = new AgentDefinition({
  name: "weather-agent",
  model: "gpt-4o",
  instructions: "You help users check the weather.",
  tools: [
    new FunctionTool({
      name: "get_weather",
      description: "Get current weather for a location",
      parameters: {
        location: new Property({
          kind: "string",
          description: "City name",
          required: true,
        }),
      },
    }),
  ],
});
```

### Saving Agent Definitions

Export to YAML or JSON:

```typescript
// Convert to YAML
const yaml = agent.toYaml();
console.log(yaml);

// Convert to JSON (with indentation)
const json = agent.toJson(undefined, 2);
console.log(json);

// Convert to dictionary
const data = agent.save();
```

## Working with Context

### Load Context

Customize how data is loaded:

```typescript
import { AgentDefinition, LoadContext } from "agentschema";

const context = new LoadContext({
  preProcess: (data) => {
    // Transform input before loading
    console.log("Loading:", data["name"]);
    return data;
  },
  postProcess: (result) => {
    // Transform result after loading
    return result;
  },
});

const agent = AgentDefinition.fromYaml(yamlContent, context);
```

### Save Context

Control output formatting:

```typescript
import { AgentDefinition, SaveContext } from "agentschema";

const context = new SaveContext({
  collectionFormat: "object", // Use object format for named collections
  useShorthand: true,         // Use shorthand notation when possible
});

const yaml = agent.toYaml(context);
```

## Available Classes

The SDK exports all AgentSchema types as TypeScript classes:

### Core Types

| Class | Description |
|-------|-------------|
| `AgentDefinition` | Complete agent specification |
| `AgentManifest` | Parameterized agent template |
| `Model` | AI model configuration |
| `ModelOptions` | Model parameters (temperature, tokens, etc.) |

### Tools

| Class | Description |
|-------|-------------|
| `FunctionTool` | Custom function definitions |
| `OpenApiTool` | OpenAPI/Swagger integrations |
| `McpTool` | Model Context Protocol tools |
| `CodeInterpreterTool` | Code execution capability |
| `FileSearchTool` | File search capability |
| `WebSearchTool` | Web search capability |
| `CustomTool` | Custom tool implementations |

### Connections

| Class | Description |
|-------|-------------|
| `ApiKeyConnection` | API key authentication |
| `AnonymousConnection` | No authentication |
| `ReferenceConnection` | Reference to external connection |
| `RemoteConnection` | Remote service connection |

### Properties

| Class | Description |
|-------|-------------|
| `Property` | Base property type |
| `ObjectProperty` | Object/nested property |
| `ArrayProperty` | Array/list property |
| `PropertySchema` | Schema with multiple properties |

## Type Safety

The SDK provides full TypeScript support with:

- Complete type definitions for all classes
- IntelliSense/autocomplete in VS Code
- Compile-time type checking
- JSDoc documentation

```typescript
import { AgentDefinition } from "agentschema";

// TypeScript knows all available properties
const agent = new AgentDefinition({
  name: "my-agent",      // string
  model: "gpt-4o",       // string
  instructions: "...",   // string
  tools: [],             // Tool[]
  // ... full autocomplete available
});
```

## ESM and CommonJS

The SDK supports both module systems:

```typescript
// ESM (recommended)
import { AgentDefinition } from "agentschema";

// CommonJS
const { AgentDefinition } = require("agentschema");
```

## Error Handling

Handle parsing errors gracefully:

```typescript
import { AgentDefinition } from "agentschema";

try {
  const agent = AgentDefinition.fromYaml(invalidYaml);
} catch (error) {
  console.error("Failed to parse agent definition:", error.message);
}
```

## Next Steps

- [Python SDK](../python/) - Python version of the SDK
- [C# SDK](../csharp/) - .NET version of the SDK
- [Go SDK](../go/) - Go version of the SDK
- [Rust SDK](../rust/) - Rust version of the SDK
- [AgentDefinition Reference](../../reference/AgentDefinition/) - Full API reference
