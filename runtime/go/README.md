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
    // From JSON
    jsonData := `{
        "name": "my-agent",
        "description": "A sample agent"
    }`
    
    agent, err := agentschema.AgentDefinitionFromJSON(jsonData)
    if err != nil {
        panic(err)
    }
    
    fmt.Printf("Agent: %s\n", agent.Name)
}
```

### Creating an Agent Definition

```go
package main

import (
    "fmt"
    "github.com/microsoft/agentschema-go/agentschema"
)

func main() {
    agent := agentschema.AgentDefinition{
        Name: "my-agent",
        Description: strPtr("A sample agent"),
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

func strPtr(s string) *string {
    return &s
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
