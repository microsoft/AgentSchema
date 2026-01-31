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
using AgentSchema;

// Load from a YAML file
var yaml = File.ReadAllText("my_agent.yaml");
var agent = AgentDefinition.FromYaml(yaml);

Console.WriteLine($"Agent: {agent.Name}");
Console.WriteLine($"Description: {agent.Description}");
Console.WriteLine($"Kind: {agent.Kind}");

// Load from a JSON file
var json = File.ReadAllText("my_agent.json");
var agentFromJson = AgentDefinition.FromJson(json);
```

### Creating an Agent Programmatically

```csharp
using AgentSchema;

// Create a simple prompt-based agent
var agent = new PromptAgent
{
    Name = "my-assistant",
    Description = "A helpful assistant that can answer questions",
    Model = new Model
    {
        Id = "gpt-4o"
    },
    Instructions = "You are a helpful assistant. Answer questions clearly and concisely.",
    Tools =
    [
        new FunctionTool
        {
            Name = "get_weather",
            Description = "Get the current weather for a location"
        }
    ],
    InputSchema = new PropertySchema
    {
        Properties =
        [
            new Property
            {
                Name = "question",
                Kind = "string",
                Description = "The user's question"
            }
        ]
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
    Model = new Model { Id = "gpt-4o" },
    Instructions = "You are a helpful assistant."
};
```

### ContainerAgent (Hosted)

An agent that runs in a container with custom logic:

```csharp
var agent = new ContainerAgent
{
    Name = "custom-agent",
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
    Description = "Search for information"
};
```

### OpenAPI Tools

```csharp
var tool = new OpenApiTool
{
    Name = "weather_api",
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
    Description = "Access filesystem operations",
    Command = "npx",
    Args = ["-y", "@modelcontextprotocol/server-filesystem", "/path/to/dir"]
};
```

### Code Interpreter

```csharp
var tool = new CodeInterpreterTool
{
    Name = "code_interpreter",
    Description = "Execute Python code"
};
```

### File Search

```csharp
var tool = new FileSearchTool
{
    Name = "file_search",
    Description = "Search through documents"
};
```

## Connections

Define how tools connect to external services:

```csharp
using AgentSchema;

// Reference an existing connection by name
var refConn = new ReferenceConnection { Name = "my-existing-connection" };

// Remote endpoint connection
var remoteConn = new RemoteConnection
{
    Name = "api-connection",
    Endpoint = "https://api.example.com"
};

// API key authentication
var apiKeyConn = new ApiKeyConnection
{
    Name = "secure-connection",
    Endpoint = "https://api.example.com",
    Key = "${API_KEY}"  // Environment variable reference
};

// Anonymous (no auth) connection
var anonConn = new AnonymousConnection
{
    Name = "public-connection",
    Endpoint = "https://public-api.example.com"
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
        if (data.TryGetValue("name", out var name))
        {
            data["name"] = name?.ToString()?.ToLower();
        }
        return data;
    },
    PostProcess = obj =>
    {
        // Transform object after instantiation
        if (obj is AgentDefinition agent)
        {
            Console.WriteLine($"Loaded agent: {agent.Name}");
        }
        return obj;
    }
};

var agent = AgentDefinition.FromYaml(yaml, context);
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
// YAML
var yaml = File.ReadAllText("agent.yaml");
var agent = AgentDefinition.FromYaml(yaml);

// JSON
var json = File.ReadAllText("agent.json");
var agent = AgentDefinition.FromJson(json);
```

### Save to File

```csharp
// YAML
File.WriteAllText("agent_output.yaml", agent.ToYaml());

// JSON
File.WriteAllText("agent_output.json", agent.ToJson());
```

## Example: Complete Agent Definition

Here's a complete example of a travel assistant agent:

```csharp
using AgentSchema;

var agent = new PromptAgent
{
    Name = "travel-assistant",
    Description = "A travel assistant that helps plan trips",
    Model = new Model { Id = "gpt-4o" },
    Tools =
    [
        new FunctionTool
        {
            Name = "get_travel_info",
            Description = "Get basic travel information"
        },
        new OpenApiTool
        {
            Name = "tripadvisor",
            Description = "Get travel recommendations from TripAdvisor",
            Specification = "./tripadvisor.openapi.json",
            Connection = new RemoteConnection
            {
                Name = "tripadvisor_connection",
                Endpoint = "https://api.tripadvisor.com"
            }
        }
    ],
    InputSchema = new PropertySchema
    {
        Properties =
        [
            new Property
            {
                Name = "destination",
                Kind = "string",
                Description = "The travel destination"
            },
            new Property
            {
                Name = "duration",
                Kind = "integer",
                Description = "Trip duration in days"
            }
        ]
    },
    Instructions = """
        You are a knowledgeable travel assistant.
        Help users plan their trips by providing recommendations for:
        - Attractions and activities
        - Restaurants and dining
        - Hotels and accommodations
        - Local tips and customs

        Always provide specific, actionable recommendations.
        """
};

// Output as YAML
Console.WriteLine(agent.ToYaml());
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
