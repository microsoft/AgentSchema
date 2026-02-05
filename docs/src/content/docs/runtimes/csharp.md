---
title: C# Runtime
description: Official C# / .NET runtime library for AgentSchema - load, create, and manipulate agent definitions.
---

The C# runtime provides a strongly-typed interface for working with AgentSchema definitions in .NET applications.

## Installation

### NuGet Package Manager

```bash
dotnet add package AgentSchema
```

Or via the Package Manager Console:

```powershell
Install-Package AgentSchema
```

## Requirements

- .NET 9.0 or later

## Quick Start

### Loading Agent Definitions

Load agent definitions from YAML or JSON:

```csharp
using AgentSchema;

// Load from YAML
var agent = AgentDefinition.FromYaml(@"
name: my-agent
model: gpt-4o
instructions: You are a helpful assistant.
");

// Load from JSON
var jsonAgent = AgentDefinition.FromJson("""
{
  "name": "my-agent",
  "model": "gpt-4o",
  "instructions": "You are a helpful assistant."
}
""");

Console.WriteLine(agent.Name);  // "my-agent"
Console.WriteLine(agent.Model); // "gpt-4o"
```

### Loading from Files

Load directly from YAML or JSON files:

```csharp
using AgentSchema;

// Load from a YAML file
var agent = AgentDefinition.FromYamlFile("agent.yaml");

// Load from a JSON file
var agent = AgentDefinition.FromJsonFile("agent.json");

// Async versions
var agent = await AgentDefinition.FromYamlFileAsync("agent.yaml");
var agent = await AgentDefinition.FromJsonFileAsync("agent.json");
```

### Creating Agents Programmatically

Build agent definitions using C# classes:

```csharp
using AgentSchema;

var agent = new AgentDefinition
{
    Name = "weather-agent",
    Model = "gpt-4o",
    Instructions = "You help users check the weather.",
    Tools = new List<Tool>
    {
        new FunctionTool
        {
            Name = "get_weather",
            Description = "Get current weather for a location",
            Parameters = new Dictionary<string, Property>
            {
                ["location"] = new Property
                {
                    Kind = "string",
                    Description = "City name",
                    Required = true,
                },
            },
        },
    },
};
```

### Saving Agent Definitions

Export to YAML or JSON:

```csharp
// Convert to YAML
var yaml = agent.ToYaml();
Console.WriteLine(yaml);

// Convert to JSON (with indentation)
var json = agent.ToJson(indent: 2);
Console.WriteLine(json);

// Convert to dictionary
var data = agent.Save();

// Save to file
agent.ToYamlFile("output.yaml");
agent.ToJsonFile("output.json");

// Async versions
await agent.ToYamlFileAsync("output.yaml");
await agent.ToJsonFileAsync("output.json");
```

## Working with Context

### Load Context

Customize how data is loaded:

```csharp
using AgentSchema;

var context = new LoadContext
{
    PreProcess = data =>
    {
        Console.WriteLine($"Loading: {data["name"]}");
        return data;
    },
    PostProcess = result =>
    {
        Console.WriteLine("Load complete");
        return result;
    },
};

var agent = AgentDefinition.FromYaml(yamlContent, context);
```

### Save Context

Control output formatting:

```csharp
using AgentSchema;

var context = new SaveContext
{
    CollectionFormat = CollectionFormat.Object,  // Use object format for named collections
    UseShorthand = true,                         // Use shorthand notation when possible
};

var yaml = agent.ToYaml(context);
```

## Available Classes

The SDK exports all AgentSchema types as C# classes:

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

## Nullable Reference Types

The SDK is designed with nullable reference types enabled:

```csharp
using AgentSchema;

var agent = new AgentDefinition
{
    Name = "my-agent",           // Required - non-nullable
    Model = "gpt-4o",            // Required - non-nullable
    Instructions = "...",        // Optional - nullable
    Tools = null,                // Optional - nullable
    ModelOptions = null,         // Optional - nullable
};

// Safe navigation with null checks
var temperature = agent.ModelOptions?.Temperature ?? 0.7;
```

## LINQ Support

Work with collections using LINQ:

```csharp
using AgentSchema;
using System.Linq;

var agent = AgentDefinition.FromYamlFile("agent.yaml");

// Find all function tools
var functionTools = agent.Tools?
    .OfType<FunctionTool>()
    .Where(t => t.Name.StartsWith("get_"))
    .ToList();

// Get required properties
var requiredProps = agent.InputSchema?.Properties?
    .Where(p => p.Value.Required == true)
    .Select(p => p.Key)
    .ToList();
```

## Error Handling

Handle parsing errors gracefully:

```csharp
using AgentSchema;
using YamlDotNet.Core;

try
{
    var agent = AgentDefinition.FromYaml(invalidYaml);
}
catch (YamlException ex)
{
    Console.WriteLine($"Invalid YAML: {ex.Message}");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Invalid agent definition: {ex.Message}");
}
```

## Integration Examples

### With Azure OpenAI

```csharp
using AgentSchema;
using Azure.AI.OpenAI;

var agent = AgentDefinition.FromYamlFile("agent.yaml");

var client = new OpenAIClient(
    new Uri("https://your-resource.openai.azure.com/"),
    new AzureKeyCredential("your-key")
);

var options = new ChatCompletionsOptions
{
    DeploymentName = agent.Model,
    Messages =
    {
        new ChatRequestSystemMessage(agent.Instructions),
        new ChatRequestUserMessage("Hello!"),
    },
    Temperature = (float)(agent.ModelOptions?.Temperature ?? 0.7),
};

var response = await client.GetChatCompletionsAsync(options);
```

### With Semantic Kernel

```csharp
using AgentSchema;
using Microsoft.SemanticKernel;

var agent = AgentDefinition.FromYamlFile("agent.yaml");

var kernel = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(
        deploymentName: agent.Model,
        endpoint: "https://your-resource.openai.azure.com/",
        apiKey: "your-key"
    )
    .Build();

var result = await kernel.InvokePromptAsync(agent.Instructions);
```

### With Dependency Injection

```csharp
using AgentSchema;
using Microsoft.Extensions.DependencyInjection;

services.AddSingleton(sp =>
{
    return AgentDefinition.FromYamlFile("agent.yaml");
});

// Inject into your services
public class AgentService
{
    private readonly AgentDefinition _agent;
    
    public AgentService(AgentDefinition agent)
    {
        _agent = agent;
    }
}
```

## Next Steps

- [TypeScript SDK](../typescript/) - Node.js/browser version
- [Python SDK](../python/) - Python version
- [Go SDK](../go/) - Go version
- [AgentDefinition Reference](../../reference/AgentDefinition/) - Full API reference
