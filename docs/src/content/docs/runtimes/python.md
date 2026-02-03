---
title: Python Runtime
description: Official Python runtime library for AgentSchema - load, create, and manipulate agent definitions.
---

The Python runtime provides a Pythonic interface for working with AgentSchema definitions, with full type hints and async support.

## Installation

```bash
pip install agentschema
```

Or using `uv`:

```bash
uv add agentschema
```

## Requirements

- Python >= 3.11

## Quick Start

### Loading Agent Definitions

Load agent definitions from YAML or JSON:

```python
from agentschema import AgentDefinition

# Load from YAML
agent = AgentDefinition.from_yaml("""
name: my-agent
model: gpt-4o
instructions: You are a helpful assistant.
""")

# Load from JSON
json_agent = AgentDefinition.from_json("""{
  "name": "my-agent",
  "model": "gpt-4o",
  "instructions": "You are a helpful assistant."
}""")

print(agent.name)  # "my-agent"
print(agent.model)  # "gpt-4o"
```

### Loading from Files

Load directly from YAML or JSON files:

```python
from agentschema import AgentDefinition

# Load from a YAML file
agent = AgentDefinition.from_yaml_file("agent.yaml")

# Load from a JSON file
agent = AgentDefinition.from_json_file("agent.json")
```

### Creating Agents Programmatically

Build agent definitions using Python classes:

```python
from agentschema import AgentDefinition, FunctionTool, Property

agent = AgentDefinition(
    name="weather-agent",
    model="gpt-4o",
    instructions="You help users check the weather.",
    tools=[
        FunctionTool(
            name="get_weather",
            description="Get current weather for a location",
            parameters={
                "location": Property(
                    kind="string",
                    description="City name",
                    required=True,
                ),
            },
        ),
    ],
)
```

### Saving Agent Definitions

Export to YAML or JSON:

```python
# Convert to YAML
yaml_str = agent.to_yaml()
print(yaml_str)

# Convert to JSON (with indentation)
json_str = agent.to_json(indent=2)
print(json_str)

# Convert to dictionary
data = agent.save()

# Save to file
agent.to_yaml_file("output.yaml")
agent.to_json_file("output.json")
```

## Working with Context

### Load Context

Customize how data is loaded:

```python
from agentschema import AgentDefinition, LoadContext

def pre_process(data: dict) -> dict:
    print(f"Loading: {data.get('name')}")
    return data

def post_process(result):
    print("Load complete")
    return result

context = LoadContext(
    pre_process=pre_process,
    post_process=post_process,
)

agent = AgentDefinition.from_yaml(yaml_content, context)
```

### Save Context

Control output formatting:

```python
from agentschema import AgentDefinition, SaveContext

context = SaveContext(
    collection_format="object",  # Use object format for named collections
    use_shorthand=True,          # Use shorthand notation when possible
)

yaml_str = agent.to_yaml(context)
```

## Available Classes

The SDK exports all AgentSchema types as Python classes:

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

## Type Hints

The SDK provides full type hint support:

```python
from agentschema import AgentDefinition, Tool

def process_agent(agent: AgentDefinition) -> list[Tool]:
    """Process an agent and return its tools."""
    return agent.tools or []

# IDE provides full autocomplete and type checking
agent = AgentDefinition(
    name="my-agent",      # str
    model="gpt-4o",       # str
    instructions="...",   # str
    tools=[],             # list[Tool]
)
```

## Async Support

The SDK supports async file operations:

```python
import asyncio
from agentschema import AgentDefinition

async def load_agent():
    agent = await AgentDefinition.from_yaml_file_async("agent.yaml")
    return agent

async def save_agent(agent: AgentDefinition):
    await agent.to_yaml_file_async("output.yaml")

# Run async operations
agent = asyncio.run(load_agent())
```

## Error Handling

Handle parsing errors gracefully:

```python
from agentschema import AgentDefinition
import yaml

try:
    agent = AgentDefinition.from_yaml(invalid_yaml)
except yaml.YAMLError as e:
    print(f"Invalid YAML: {e}")
except ValueError as e:
    print(f"Invalid agent definition: {e}")
```

## Integration Examples

### With LangChain

```python
from agentschema import AgentDefinition

# Load your agent definition
agent = AgentDefinition.from_yaml_file("agent.yaml")

# Use with LangChain
from langchain.chat_models import ChatOpenAI

llm = ChatOpenAI(
    model=agent.model,
    temperature=agent.model_options.temperature if agent.model_options else 0.7,
)
```

### With OpenAI SDK

```python
from agentschema import AgentDefinition
from openai import OpenAI

agent = AgentDefinition.from_yaml_file("agent.yaml")
client = OpenAI()

response = client.chat.completions.create(
    model=agent.model,
    messages=[
        {"role": "system", "content": agent.instructions},
        {"role": "user", "content": "Hello!"},
    ],
)
```

## Next Steps

- [TypeScript SDK](../typescript/) - Node.js/browser version
- [C# SDK](../csharp/) - .NET version
- [AgentDefinition Reference](../../reference/AgentDefinition/) - Full API reference
