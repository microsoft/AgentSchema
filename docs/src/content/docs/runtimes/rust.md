---
title: Rust Runtime
description: Official Rust runtime library for AgentSchema - load, create, and manipulate agent definitions.
---

The Rust runtime provides a type-safe interface for working with AgentSchema definitions in Rust applications, built on `serde` for fast and reliable serialization.

## Installation

Add to your `Cargo.toml`:

```toml
[dependencies]
agentschema = "1.0.0-beta.7"
```

## Requirements

- Rust 1.75 or later

## Quick Start

### Loading Agent Definitions

Load agent definitions from YAML or JSON:

```rust
use agentschema::AgentDefinition;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    // Load from YAML
    let yaml_content = r#"
name: my-agent
kind: prompt
model: gpt-4o
instructions: You are a helpful assistant.
"#;
    let agent = AgentDefinition::from_yaml(yaml_content)?;
    println!("{}", agent.name);  // "my-agent"

    // Load from JSON
    let json_content = r#"{
      "name": "my-agent",
      "kind": "prompt",
      "model": "gpt-4o",
      "instructions": "You are a helpful assistant."
    }"#;
    let json_agent = AgentDefinition::from_json(json_content)?;
    println!("{}", json_agent.name);  // "my-agent"

    Ok(())
}
```

### Loading from Files

Load directly from YAML or JSON files:

```rust
use agentschema::AgentDefinition;
use std::fs;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    // Load from a YAML file
    let yaml = fs::read_to_string("agent.yaml")?;
    let agent = AgentDefinition::from_yaml(&yaml)?;

    // Load from a JSON file
    let json = fs::read_to_string("agent.json")?;
    let json_agent = AgentDefinition::from_json(&json)?;

    Ok(())
}
```

### Creating Agents Programmatically

Build agent definitions using Rust structs:

```rust
use agentschema::{AgentDefinition, FunctionTool, Property};
use std::collections::HashMap;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let mut parameters = HashMap::new();
    parameters.insert(
        "location".to_string(),
        Property {
            kind: "string".to_string(),
            description: Some("City name".to_string()),
            required: Some(true),
            ..Default::default()
        },
    );

    let agent = AgentDefinition {
        name: "weather-agent".to_string(),
        description: Some("Helps users check the weather".to_string()),
        model: "gpt-4o".to_string(),
        instructions: Some("You help users check the weather.".to_string()),
        tools: Some(vec![
            FunctionTool {
                kind: "function".to_string(),
                name: "get_weather".to_string(),
                description: Some("Get current weather for a location".to_string()),
                parameters: Some(parameters),
                ..Default::default()
            }
            .into(),
        ]),
        ..Default::default()
    };

    println!("Created agent: {}", agent.name);
    Ok(())
}
```

### Saving Agent Definitions

Export to YAML or JSON:

```rust
use agentschema::AgentDefinition;
use std::fs;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let agent = AgentDefinition {
        name: "my-agent".to_string(),
        model: "gpt-4o".to_string(),
        ..Default::default()
    };

    // Convert to YAML string
    let yaml_str = agent.to_yaml()?;
    println!("{}", yaml_str);

    // Convert to JSON string
    let json_str = agent.to_json()?;
    println!("{}", json_str);

    // Save to files
    fs::write("output.yaml", &yaml_str)?;
    fs::write("output.json", &json_str)?;

    Ok(())
}
```

## Working with Context

### Load Context

Customize how data is loaded:

```rust
use agentschema::{AgentDefinition, LoadContext};

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let ctx = LoadContext::new();
    let agent = AgentDefinition::from_yaml_with_context(yaml_content, &ctx)?;
    Ok(())
}
```

### Save Context

Control output formatting:

```rust
use agentschema::{AgentDefinition, SaveContext, CollectionFormat};

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let agent = AgentDefinition {
        name: "my-agent".to_string(),
        model: "gpt-4o".to_string(),
        ..Default::default()
    };

    let ctx = SaveContext {
        collection_format: CollectionFormat::Object,
        use_shorthand: true,
        ..Default::default()
    };

    let yaml_str = agent.to_yaml_with_context(&ctx)?;
    Ok(())
}
```

## Available Types

The crate exports all AgentSchema types as Rust structs:

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

## Error Handling

Handle parsing errors gracefully:

```rust
use agentschema::AgentDefinition;

fn main() {
    let invalid_yaml = "invalid: yaml: content:";

    match AgentDefinition::from_yaml(invalid_yaml) {
        Ok(agent) => println!("Loaded: {}", agent.name),
        Err(e) => eprintln!("Failed to parse YAML: {}", e),
    }
}
```

## Integration Examples

### With Reqwest (HTTP Client)

```rust
use agentschema::AgentDefinition;
use std::fs;

async fn run_agent() -> Result<(), Box<dyn std::error::Error>> {
    let yaml = fs::read_to_string("agent.yaml")?;
    let agent = AgentDefinition::from_yaml(&yaml)?;

    let client = reqwest::Client::new();
    let response = client
        .post("https://api.openai.com/v1/chat/completions")
        .bearer_auth(std::env::var("OPENAI_API_KEY")?)
        .json(&serde_json::json!({
            "model": agent.model,
            "messages": [
                {"role": "system", "content": agent.instructions},
                {"role": "user", "content": "Hello!"}
            ]
        }))
        .send()
        .await?;

    println!("{}", response.text().await?);
    Ok(())
}
```

### As Axum Handler

```rust
use agentschema::AgentDefinition;
use axum::{routing::get, Json, Router};
use std::fs;
use std::sync::Arc;

struct AppState {
    agent: AgentDefinition,
}

async fn agent_handler(
    axum::extract::State(state): axum::extract::State<Arc<AppState>>,
) -> Json<serde_json::Value> {
    Json(serde_json::json!({
        "name": state.agent.name,
        "model": state.agent.model,
        "instructions": state.agent.instructions,
    }))
}

#[tokio::main]
async fn main() {
    let yaml = fs::read_to_string("agent.yaml").unwrap();
    let agent = AgentDefinition::from_yaml(&yaml).unwrap();
    let state = Arc::new(AppState { agent });

    let app = Router::new()
        .route("/agent", get(agent_handler))
        .with_state(state);

    let listener = tokio::net::TcpListener::bind("0.0.0.0:8080").await.unwrap();
    axum::serve(listener, app).await.unwrap();
}
```

## Next Steps

- [Python SDK](../python/) - Python version
- [TypeScript SDK](../typescript/) - Node.js/browser version
- [C# SDK](../csharp/) - .NET version
- [Go SDK](../go/) - Go version
- [AgentDefinition Reference](../../reference/AgentDefinition/) - Full API reference
