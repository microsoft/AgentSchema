# AgentSchema Python SDK

[![PyPI version](https://badge.fury.io/py/agentschema.svg)](https://pypi.org/project/agentschema/)
[![Python 3.11+](https://img.shields.io/badge/python-3.11+-blue.svg)](https://www.python.org/downloads/)

A Python SDK for working with [AgentSchema](https://microsoft.github.io/AgentSchema/) - a declarative specification for defining AI agents in a code-first YAML format. This SDK provides strongly-typed Python classes for loading, manipulating, and saving agent definitions.

## Installation

```bash
pip install agentschema
```

## Quick Start

### Loading an Agent Definition

```python
import yaml
from agentschema.core import AgentDefinition, LoadContext

# Load from a YAML file
with open("my_agent.yaml", "r") as f:
    data = yaml.safe_load(f)

agent = AgentDefinition.load(data)

print(f"Agent: {agent.name}")
print(f"Description: {agent.description}")
print(f"Kind: {agent.kind}")
```

### Creating an Agent Programmatically

```python
from agentschema.core import (
    PromptAgent,
    Model,
    FunctionTool,
    PropertySchema,
    Property,
    SaveContext,
)

# Create a simple prompt-based agent
agent = PromptAgent(
    name="my-assistant",
    description="A helpful assistant that can answer questions",
    model=Model(
        name="gpt-4o",
    ),
    instructions="You are a helpful assistant. Answer questions clearly and concisely.",
    tools=[
        FunctionTool(
            name="get_weather",
            description="Get the current weather for a location",
        ),
    ],
    inputSchema=PropertySchema(
        properties={
            "question": Property(
                type="string",
                description="The user's question",
            ),
        },
    ),
)

# Save to YAML
yaml_output = agent.to_yaml()
print(yaml_output)

# Save to JSON
json_output = agent.to_json()
print(json_output)
```

## Agent Types

AgentSchema supports multiple agent types:

### PromptAgent

A straightforward agent that uses a language model with optional tools:

```python
from agentschema.core import PromptAgent, Model

agent = PromptAgent(
    name="chat-agent",
    kind="prompt",
    model=Model(name="gpt-4o"),
    instructions="You are a helpful assistant.",
)
```

### ContainerAgent (Hosted)

An agent that runs in a container with custom logic:

```python
from agentschema.core import ContainerAgent

agent = ContainerAgent(
    name="custom-agent",
    kind="hosted",
    endpoint="https://my-agent.azurewebsites.net",
)
```

## Tools

Agents can use various tool types:

### Function Tools

```python
from agentschema.core import FunctionTool

tool = FunctionTool(
    name="search",
    description="Search for information",
)
```

### OpenAPI Tools

```python
from agentschema.core import OpenApiTool, RemoteConnection

tool = OpenApiTool(
    name="weather_api",
    description="Get weather information",
    specification="./weather.openapi.json",
    connection=RemoteConnection(
        name="weather_connection",
        endpoint="https://api.weather.com",
    ),
)
```

### MCP Tools (Model Context Protocol)

```python
from agentschema.core import McpTool

tool = McpTool(
    name="filesystem",
    description="Access filesystem operations",
    command="npx",
    args=["-y", "@modelcontextprotocol/server-filesystem", "/path/to/dir"],
)
```

### Code Interpreter

```python
from agentschema.core import CodeInterpreterTool

tool = CodeInterpreterTool(
    name="code_interpreter",
    description="Execute Python code",
)
```

### File Search

```python
from agentschema.core import FileSearchTool

tool = FileSearchTool(
    name="file_search",
    description="Search through documents",
)
```

## Connections

Define how tools connect to external services:

```python
from agentschema.core import (
    ReferenceConnection,
    RemoteConnection,
    ApiKeyConnection,
    AnonymousConnection,
)

# Reference an existing connection by name
ref_conn = ReferenceConnection(name="my-existing-connection")

# Remote endpoint connection
remote_conn = RemoteConnection(
    name="api-connection",
    endpoint="https://api.example.com",
)

# API key authentication
api_key_conn = ApiKeyConnection(
    name="secure-connection",
    endpoint="https://api.example.com",
    key="${API_KEY}",  # Environment variable reference
)

# Anonymous (no auth) connection
anon_conn = AnonymousConnection(
    name="public-connection",
    endpoint="https://public-api.example.com",
)
```

## Context Customization

### LoadContext

Customize how data is loaded with pre/post processing hooks:

```python
from agentschema.core import AgentDefinition, LoadContext

def preprocess(data):
    # Transform data before parsing
    data["name"] = data.get("name", "").lower()
    return data

def postprocess(agent):
    # Transform agent after instantiation
    print(f"Loaded agent: {agent.name}")
    return agent

context = LoadContext(
    pre_process=preprocess,
    post_process=postprocess,
)

agent = AgentDefinition.load(data, context)
```

### SaveContext

Control serialization format and behavior:

```python
from agentschema.core import SaveContext

# Default: collections as objects with name as key
context = SaveContext(
    collection_format="object",  # or "array" for list format
    use_shorthand=True,  # Use compact representations when possible
)

yaml_output = agent.to_yaml(context)
```

**Collection formats:**

- `"object"` (default): Collections use the item's name as the key
  ```yaml
  tools:
    my_tool:
      kind: function
      description: A tool
  ```

- `"array"`: Collections are lists of objects
  ```yaml
  tools:
    - name: my_tool
      kind: function
      description: A tool
  ```

## Working with YAML Files

### Load from File

```python
import yaml
from agentschema.core import AgentDefinition

with open("agent.yaml", "r") as f:
    data = yaml.safe_load(f)

agent = AgentDefinition.load(data)
```

### Save to File

```python
yaml_content = agent.to_yaml()

with open("agent_output.yaml", "w") as f:
    f.write(yaml_content)
```

## Example: Complete Agent Definition

Here's a complete example of a travel assistant agent:

```python
from agentschema.core import (
    PromptAgent,
    Model,
    FunctionTool,
    OpenApiTool,
    RemoteConnection,
    PropertySchema,
    Property,
)

agent = PromptAgent(
    name="travel-assistant",
    description="A travel assistant that helps plan trips",
    metadata={
        "tags": ["travel", "assistant"],
        "authors": ["your-name"],
    },
    model=Model(name="gpt-4o"),
    tools=[
        FunctionTool(
            name="get_travel_info",
            description="Get basic travel information",
        ),
        OpenApiTool(
            name="tripadvisor",
            description="Get travel recommendations from TripAdvisor",
            specification="./tripadvisor.openapi.json",
            connection=RemoteConnection(
                name="tripadvisor_connection",
                endpoint="https://api.tripadvisor.com",
            ),
        ),
    ],
    inputSchema=PropertySchema(
        properties={
            "destination": Property(
                type="string",
                description="The travel destination",
            ),
            "duration": Property(
                type="integer",
                description="Trip duration in days",
            ),
        },
    ),
    instructions="""You are a knowledgeable travel assistant.
Help users plan their trips by providing recommendations for:
- Attractions and activities
- Restaurants and dining
- Hotels and accommodations
- Local tips and customs

Always provide specific, actionable recommendations.""",
)

# Output as YAML
print(agent.to_yaml())
```

## Type Support

This package includes type stubs (`py.typed`) for full IDE support with type checking. All classes are fully typed using Python's type hints.

## Documentation

For more information about the AgentSchema specification, visit:
- [AgentSchema Documentation](https://microsoft.github.io/AgentSchema/)
- [Object Model Reference](https://microsoft.github.io/AgentSchema/reference/)
- [GitHub Repository](https://github.com/microsoft/AgentSchema)

## Contributing

We welcome contributions! Please see the [main repository](https://github.com/microsoft/AgentSchema) for contribution guidelines.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/microsoft/AgentSchema/blob/main/LICENSE) file for details.
