# AgentSchema Go SDK

Go implementation of the AgentSchema specification for AI agent definitions.

## Installation

```bash
go get github.com/microsoft/agentschema-go/agentschema
```

## Usage

### Loading an Agent Definition

```go
package main

import (
    "fmt"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    // From YAML
    yamlData := `
name: my-agent
kind: prompt
model: gpt-4o
instructions: You are a helpful assistant.
`
    
    agent, err := agentschema.PromptAgentFromYAML(yamlData)
    if err != nil {
        panic(err)
    }
    
    fmt.Printf("Agent: %s\n", agent.Name)

    // From JSON
    jsonData := `{
        "name": "my-agent",
        "kind": "prompt",
        "model": "gpt-4o",
        "instructions": "You are a helpful assistant."
    }`
    
    jsonAgent, err := agentschema.PromptAgentFromJSON(jsonData)
    if err != nil {
        panic(err)
    }
    
    fmt.Printf("Agent: %s\n", jsonAgent.Name)
}
```

### Creating an Agent Definition

```go
package main

import (
    "fmt"
    "github.com/microsoft/agentschema-go/agentschema"
)

func strPtr(s string) *string { return &s }

func main() {
    agent := agentschema.PromptAgent{
        Kind:         "prompt",
        Name:         "my-agent",
        Description:  strPtr("A sample agent"),
        Instructions: strPtr("You are a helpful assistant."),
        Model:        agentschema.Model{Name: "gpt-4o"},
    }
    
    // Serialize to JSON
    jsonStr, err := agent.ToJSON()
    if err != nil {
        panic(err)
    }
    fmt.Println(jsonStr)
    
    // Serialize to YAML
    yamlStr, err := agent.ToYAML()
    if err != nil {
        panic(err)
    }
    fmt.Println(yamlStr)
}
```

## Features

- **Type-safe**: Strongly-typed Go structs generated from TypeSpec schema
- **JSON/YAML Support**: Built-in serialization and deserialization
- **Validation**: Load/Save with context for extensibility
- **Polymorphic Types**: Support for discriminated unions
- **Shorthand Syntax**: Alternate scalar representations

## Code Generation

This code is auto-generated from the AgentSchema TypeSpec definitions. Do not edit the generated files directly.

To regenerate:

```bash
cd agentschema
npm run generate
```

## License

MIT
