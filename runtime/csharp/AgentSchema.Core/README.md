# AgentSchema C# SDK

[![NuGet version](https://badge.fury.io/nu/AgentSchema.svg)](https://www.nuget.org/packages/AgentSchema/)
[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download)

A C# SDK for working with [AgentSchema](https://microsoft.github.io/AgentSchema/) - a declarative specification for defining AI agents in a code-first YAML format. This SDK provides strongly-typed C# classes for loading, manipulating, and saving agent definitions.

## Installation

```bash
dotnet add package AgentSchema
```

## Quick Start

### Loading an Agent Definition

```csharp
using AgentSchema.Core.Model;
using System.Text.Json;

// Load from JSON
var json = File.ReadAllText("my_agent.json");
var agent = JsonSerializer.Deserialize<AgentDefinition>(json);

Console.WriteLine($"Agent: {agent.Name}");
Console.WriteLine($"Description: {agent.Description}");
Console.WriteLine($"Kind: {agent.Kind}");

// Load from YAML
var yaml = File.ReadAllText("my_agent.yaml");
var deserializer = Yaml.GetDeserializer();
var agentFromYaml = deserializer.Deserialize<AgentDefinition>(yaml);
```

### Creating an Agent Programmatically

```csharp
using AgentSchema.Core.Model;

// Create a simple prompt-based agent
var agent = new PromptAgent
{
    Name = "my-assistant",
    Description = "A helpful assistant that can answer questions",
    Kind = "prompt",
    Model = new Model
    {
        Name = "gpt-4o"
    },
    Instructions = "You are a helpful assistant. Answer questions clearly and concisely.",
    Tools =
    [
        new FunctionTool
        {
            Name = "get_weather",
            Kind = "function",
            Description = "Get the current weather for a location"
        }
    ],
    InputSchema = new PropertySchema
    {
        Properties = new Dictionary<string, object?>
        {
            ["question"] = new Property
            {
                Type = "string",
                Description = "The user's question"
            }
        }
    }
};

// Save to YAML
var yamlOutput = agent.ToYaml();
Console.WriteLine(yamlOutput);

// Save to JSON
var jsonOutput = agent.ToJson();
Console.WriteLine(jsonOutput);
```

## Agent Types

AgentSchema supports multiple agent types:

### PromptAgent

A straightforward agent that uses a language model with optional tools:

```csharp
var agent = new PromptAgent
{
    Name = "chat-agent",
    Kind = "prompt",
    Model = new Model { Name = "gpt-4o" },
    Instructions = "You are a helpful assistant."
};
```

### ContainerAgent (Hosted)

An agent that runs in a container with custom logic:

```csharp
var agent = new ContainerAgent
{
    Name = "custom-agent",
    Kind = "hosted",
    Endpoint = "https://my-agent.azurewebsites.net"
};
```

## Tools

Agents can use various tool types:

### Function Tools

```csharp
var tool = new FunctionTool
{
    Name = "search",
    Kind = "function",
    Description = "Search for information"
};
```

### OpenAPI Tools

```csharp
var tool = new OpenApiTool
{
    Name = "weather_api",
    Kind = "openapi",
    Description = "Get weather information",
    Specification = "./weather.openapi.json",
    Connection = new RemoteConnection
    {
        Name = "weather_connection",
        Endpoint = "https://api.weather.com"
    }
};
```

### MCP Tools (Model Context Protocol)

```csharp
var tool = new McpTool
{
    Name = "filesystem",
    Kind = "mcp",
    Description = "Access filesystem operations",
    Command = "npx",
    Args = ["-y", "@modelcontextprotocol/server-filesystem", "/path/to/dir"]
};
```

## Context Customization

### LoadContext

Customize how data is loaded with pre/post processing hooks:

```csharp
var context = new LoadContext
{
    PreProcess = data =>
    {
        // Transform data before parsing
        data["name"] = data["name"]?.ToString()?.ToLower();
        return data;
    },
    PostProcess = obj =>
    {
        // Transform object after instantiation
        Console.WriteLine($"Loaded: {((AgentDefinition)obj).Name}");
        return obj;
    }
};

var agent = AgentDefinition.Load(data, context);
```

### SaveContext

Control serialization format and behavior:

```csharp
var context = new SaveContext
{
    CollectionFormat = "object",  // or "array" for list format
    UseShorthand = true           // Use compact representations when possible
};

var yaml = agent.ToYaml(context);
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

## Working with Files

### Load from File

```csharp
// JSON
var json = File.ReadAllText("agent.json");
var agent = JsonSerializer.Deserialize<AgentDefinition>(json);

// YAML
var yaml = File.ReadAllText("agent.yaml");
var deserializer = Yaml.GetDeserializer();
var agent = deserializer.Deserialize<AgentDefinition>(yaml);
```

### Save to File

```csharp
// YAML
File.WriteAllText("agent_output.yaml", agent.ToYaml());

// JSON
File.WriteAllText("agent_output.json", agent.ToJson());
```

## Documentation

For more information about the AgentSchema specification, visit:

- [AgentSchema Documentation](https://microsoft.github.io/AgentSchema/)
- [Object Model Reference](https://microsoft.github.io/AgentSchema/reference/)
- [GitHub Repository](https://github.com/microsoft/AgentSchema)

## Contributing

We welcome contributions! Please see the [main repository](https://github.com/microsoft/AgentSchema) for contribution guidelines.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/microsoft/AgentSchema/blob/main/LICENSE) file for details.
