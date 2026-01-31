# AgentSchema TypeScript SDK

TypeScript SDK for declarative agent schema definitions.

## Installation

```bash
npm install agentschema
# or
yarn add agentschema
# or
pnpm add agentschema
```

## Quick Start

### Loading Agent Definitions

You can load agent definitions from JSON or YAML:

```typescript
import { AgentDefinition } from "agentschema";

// Load from JSON
const jsonAgent = AgentDefinition.fromJson(`{
  "name": "my-agent",
  "model": "gpt-4o",
  "instructions": "You are a helpful assistant."
}`);

// Load from YAML
const yamlAgent = AgentDefinition.fromYaml(`
name: my-agent
model: gpt-4o
instructions: You are a helpful assistant.
`);
```

### Creating Agent Definitions Programmatically

```typescript
import { AgentDefinition, FunctionTool } from "agentschema";

const agent = new AgentDefinition({
  name: "my-agent",
  model: "gpt-4o",
  instructions: "You are a helpful assistant.",
  tools: [
    new FunctionTool({
      name: "get_weather",
      description: "Get the current weather for a location",
    }),
  ],
});

// Convert to YAML
const yaml = agent.toYaml();
console.log(yaml);

// Convert to JSON
const json = agent.toJson();
console.log(json);
```

### Using Load/Save Context

Customize the loading and saving process with context objects:

```typescript
import { AgentDefinition, LoadContext, SaveContext } from "agentschema";

// Custom load context with preprocessing
const loadContext = new LoadContext({
  preProcess: (data) => {
    // Transform input data before loading
    console.log("Loading agent:", data["name"]);
    return data;
  },
  postProcess: (result) => {
    // Transform result after loading
    console.log("Loaded successfully");
    return result;
  },
});

const agent = AgentDefinition.fromYaml(yamlContent, loadContext);

// Custom save context
const saveContext = new SaveContext({
  collectionFormat: "object", // Use object format for collections (name as key)
  useShorthand: true, // Use shorthand notation when possible
});

const output = agent.toYaml(saveContext);
```

### Working with Dictionaries

You can also work directly with plain JavaScript objects:

```typescript
import { AgentDefinition } from "agentschema";

// Load from a dictionary
const data: Record<string, unknown> = {
  name: "my-agent",
  model: "gpt-4o",
  instructions: "You are a helpful assistant.",
};

const agent = AgentDefinition.load(data);

// Save to a dictionary
const savedData = agent.save();
console.log(savedData);
```

## API Reference

### Core Classes

#### `LoadContext`

Context for customizing the loading process.

```typescript
interface LoadContext {
  preProcess?: (data: Record<string, unknown>) => Record<string, unknown>;
  postProcess?: (result: unknown) => unknown;
}
```

#### `SaveContext`

Context for customizing the serialization process.

```typescript
interface SaveContext {
  preSave?: (obj: unknown) => unknown;
  postSave?: (data: Record<string, unknown>) => Record<string, unknown>;
  collectionFormat: "object" | "array";
  useShorthand: boolean;
}
```

### Model Classes

Each model class provides the following methods:

- **`static load(data, context?)`** - Load from a dictionary
- **`static fromJson(json, context?)`** - Load from a JSON string
- **`static fromYaml(yaml, context?)`** - Load from a YAML string
- **`save(context?)`** - Save to a dictionary
- **`toJson(context?, indent?)`** - Convert to a JSON string
- **`toYaml(context?)`** - Convert to a YAML string

## Development

### Prerequisites

- Node.js >= 18.0.0
- npm, yarn, or pnpm

### Setup

```bash
npm install
```

### Build

```bash
npm run build
```

### Test

```bash
npm test
```

### Format

```bash
npm run format
```

### Lint

```bash
npm run lint
```

## License

MIT
