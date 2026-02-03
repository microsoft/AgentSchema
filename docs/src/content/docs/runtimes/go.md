---
title: Go Runtime
description: Official Go runtime library for AgentSchema - load, create, and manipulate agent definitions.
---

The Go runtime provides a type-safe interface for working with AgentSchema definitions in Go applications.

## Installation

```bash
go get github.com/microsoft/agentschema-go/agentschema
```

## Requirements

- Go 1.21 or later

## Quick Start

### Loading Agent Definitions

Load agent definitions from YAML or JSON:

```go
package main

import (
    "fmt"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    // Load from YAML
    yamlContent := `
name: my-agent
kind: prompt
model: gpt-4o
instructions: You are a helpful assistant.
`
    agent, err := agentschema.PromptAgentFromYAML(yamlContent)
    if err != nil {
        panic(err)
    }
    
    fmt.Println(agent.Name)  // "my-agent"

    // Load from JSON
    jsonContent := `{
      "name": "my-agent",
      "kind": "prompt",
      "model": "gpt-4o",
      "instructions": "You are a helpful assistant."
    }`
    
    jsonAgent, err := agentschema.PromptAgentFromJSON(jsonContent)
    if err != nil {
        panic(err)
    }
    
    fmt.Println(jsonAgent.Name)  // "my-agent"
}
```

### Loading from Files

Load directly from YAML or JSON files:

```go
package main

import (
    "os"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    // Load from a YAML file
    yamlBytes, err := os.ReadFile("agent.yaml")
    if err != nil {
        panic(err)
    }
    agent, err := agentschema.PromptAgentFromYAML(string(yamlBytes))
    if err != nil {
        panic(err)
    }

    // Load from a JSON file
    jsonBytes, err := os.ReadFile("agent.json")
    if err != nil {
        panic(err)
    }
    jsonAgent, err := agentschema.PromptAgentFromJSON(string(jsonBytes))
    if err != nil {
        panic(err)
    }
}
```

### Creating Agents Programmatically

Build agent definitions using Go structs:

```go
package main

import (
    "fmt"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    instructions := "You help users check the weather."
    description := "Get current weather for a location"
    
    agent := agentschema.PromptAgent{
        Kind:         "prompt",
        Name:         "weather-agent",
        Model:        agentschema.Model{Name: "gpt-4o"},
        Instructions: &instructions,
        Tools: []agentschema.Tool{
            agentschema.FunctionTool{
                Kind:        "function",
                Name:        "get_weather",
                Description: &description,
                Parameters: map[string]agentschema.Property{
                    "location": {
                        Kind:        "string",
                        Description: strPtr("City name"),
                        Required:    boolPtr(true),
                    },
                },
            },
        },
    }
    
    fmt.Printf("Created agent: %s\n", agent.Name)
}

func strPtr(s string) *string { return &s }
func boolPtr(b bool) *bool { return &b }
```

### Saving Agent Definitions

Export to YAML or JSON:

```go
package main

import (
    "fmt"
    "os"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    agent := agentschema.PromptAgent{
        Kind:  "prompt",
        Name:  "my-agent",
        Model: agentschema.Model{Name: "gpt-4o"},
    }

    // Convert to YAML
    yamlStr, err := agent.ToYAML()
    if err != nil {
        panic(err)
    }
    fmt.Println(yamlStr)

    // Convert to JSON
    jsonStr, err := agent.ToJSON()
    if err != nil {
        panic(err)
    }
    fmt.Println(jsonStr)

    // Convert to map
    ctx := agentschema.NewSaveContext()
    data := agent.Save(ctx)

    // Save to file
    os.WriteFile("output.yaml", []byte(yamlStr), 0644)
    os.WriteFile("output.json", []byte(jsonStr), 0644)
}
```

## Working with Context

### Load Context

Customize how data is loaded:

```go
package main

import (
    "fmt"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    ctx := agentschema.NewLoadContext()
    ctx.PreProcess = func(data map[string]interface{}) map[string]interface{} {
        fmt.Printf("Loading: %v\n", data["name"])
        return data
    }
    ctx.PostProcess = func(result interface{}) interface{} {
        fmt.Println("Load complete")
        return result
    }

    // Use context when loading
    var data map[string]interface{}
    // ... unmarshal YAML/JSON into data ...
    agent, err := agentschema.LoadPromptAgent(data, ctx)
}
```

### Save Context

Control output formatting:

```go
package main

import (
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    ctx := agentschema.NewSaveContext()
    ctx.CollectionFormat = agentschema.CollectionFormatObject
    ctx.UseShorthand = true

    agent := agentschema.PromptAgent{...}
    data := agent.Save(ctx)
}
```

## Available Types

The SDK exports all AgentSchema types as Go structs:

### Core Types

| Type | Description |
|------|-------------|
| `AgentDefinition` | Base agent specification (polymorphic) |
| `PromptAgent` | Prompt-based agent definition |
| `Workflow` | Workflow agent definition |
| `ContainerAgent` | Container-hosted agent |
| `AgentManifest` | Parameterized agent template |
| `Model` | AI model configuration |
| `ModelOptions` | Model parameters (temperature, tokens, etc.) |

### Tools

| Type | Description |
|------|-------------|
| `FunctionTool` | Custom function definitions |
| `OpenApiTool` | OpenAPI/Swagger integrations |
| `McpTool` | Model Context Protocol tools |
| `CodeInterpreterTool` | Code execution capability |
| `FileSearchTool` | File search capability |
| `WebSearchTool` | Web search capability |
| `CustomTool` | Custom tool implementations |

### Connections

| Type | Description |
|------|-------------|
| `ApiKeyConnection` | API key authentication |
| `AnonymousConnection` | No authentication |
| `ReferenceConnection` | Reference to external connection |
| `RemoteConnection` | Remote service connection |

### Properties

| Type | Description |
|------|-------------|
| `Property` | Base property type |
| `ObjectProperty` | Object/nested property |
| `ArrayProperty` | Array/list property |
| `PropertySchema` | Schema with multiple properties |

## Pointer Helpers

Go requires pointers for optional fields. Use helper functions:

```go
package main

import "github.com/microsoft/agentschema-go/agentschema"

// Helper functions for creating pointers
func strPtr(s string) *string { return &s }
func intPtr(i int) *int { return &i }
func float64Ptr(f float64) *float64 { return &f }
func boolPtr(b bool) *bool { return &b }

func main() {
    agent := agentschema.PromptAgent{
        Kind:         "prompt",
        Name:         "my-agent",
        Description:  strPtr("Optional description"),  // Use pointer helper
        Instructions: strPtr("You are helpful."),
        Model: agentschema.Model{
            Name: "gpt-4o",
            Options: &agentschema.ModelOptions{
                Temperature: float64Ptr(0.7),
                MaxTokens:   intPtr(1000),
            },
        },
    }
}
```

## Error Handling

Handle parsing errors gracefully:

```go
package main

import (
    "fmt"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    invalidYAML := `invalid: yaml: content`
    
    agent, err := agentschema.PromptAgentFromYAML(invalidYAML)
    if err != nil {
        fmt.Printf("Failed to parse YAML: %v\n", err)
        return
    }
    
    // Validate required fields
    if agent.Name == "" {
        fmt.Println("Agent name is required")
        return
    }
}
```

## Integration Examples

### With Azure OpenAI SDK

```go
package main

import (
    "context"
    "fmt"
    "os"
    
    "github.com/Azure/azure-sdk-for-go/sdk/ai/azopenai"
    "github.com/Azure/azure-sdk-for-go/sdk/azcore"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    // Load agent definition
    yamlBytes, _ := os.ReadFile("agent.yaml")
    agent, _ := agentschema.PromptAgentFromYAML(string(yamlBytes))

    // Create Azure OpenAI client
    keyCredential := azcore.NewKeyCredential(os.Getenv("AZURE_OPENAI_KEY"))
    client, _ := azopenai.NewClientWithKeyCredential(
        os.Getenv("AZURE_OPENAI_ENDPOINT"),
        keyCredential,
        nil,
    )

    // Build chat request using agent config
    messages := []azopenai.ChatRequestMessageClassification{
        &azopenai.ChatRequestSystemMessage{
            Content: agent.Instructions,
        },
        &azopenai.ChatRequestUserMessage{
            Content: azopenai.NewChatRequestUserMessageContent("Hello!"),
        },
    }

    resp, _ := client.GetChatCompletions(
        context.Background(),
        azopenai.ChatCompletionsOptions{
            DeploymentName: &agent.Model.Name,
            Messages:       messages,
        },
        nil,
    )

    fmt.Println(*resp.Choices[0].Message.Content)
}
```

### As HTTP Handler

```go
package main

import (
    "encoding/json"
    "net/http"
    "os"
    
    "github.com/microsoft/agentschema-go/agentschema"
)

var agent agentschema.PromptAgent

func init() {
    yamlBytes, _ := os.ReadFile("agent.yaml")
    agent, _ = agentschema.PromptAgentFromYAML(string(yamlBytes))
}

func agentHandler(w http.ResponseWriter, r *http.Request) {
    w.Header().Set("Content-Type", "application/json")
    json.NewEncoder(w).Encode(map[string]interface{}{
        "name":         agent.Name,
        "model":        agent.Model.Name,
        "instructions": agent.Instructions,
    })
}

func main() {
    http.HandleFunc("/agent", agentHandler)
    http.ListenAndServe(":8080", nil)
}
```

## Next Steps

- [Python SDK](../python/) - Python version
- [TypeScript SDK](../typescript/) - Node.js/browser version
- [C# SDK](../csharp/) - .NET version
- [AgentDefinition Reference](../../reference/AgentDefinition/) - Full API reference
