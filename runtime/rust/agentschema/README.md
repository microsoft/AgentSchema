# AgentSchema Rust SDK

Rust implementation of the AgentSchema specification for AI agent definitions.

## Installation

Add to your `Cargo.toml`:

```toml
[dependencies]
agentschema = "1.0.0-beta.10"
```

## Usage

### Loading an Agent Definition

```rust
use agentschema::PromptAgent;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    // From YAML
    let yaml_data = r#"
name: my-agent
kind: prompt
model: gpt-4o
instructions: You are a helpful assistant.
"#;
    
    let agent = PromptAgent::from_yaml(yaml_data)?;
    println!("Agent: {}", agent.name);

    // From JSON
    let json_data = r#"{
        "name": "my-agent",
        "kind": "prompt",
        "model": "gpt-4o",
        "instructions": "You are a helpful assistant."
    }"#;
    
    let json_agent = PromptAgent::from_json(json_data)?;
    println!("Agent: {}", json_agent.name);
    
    Ok(())
}
```

### Creating an Agent Definition

```rust
use agentschema::PromptAgent;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let mut agent = PromptAgent::new();
    agent.name = "my-agent".to_string();
    agent.model = "gpt-4o".to_string();
    agent.instructions = Some("You are a helpful assistant.".to_string());

    // Convert to YAML
    let yaml = agent.to_yaml()?;
    println!("{}", yaml);

    // Convert to JSON
    let json = agent.to_json()?;
    println!("{}", json);
    
    Ok(())
}
```

### Working with Tools

```rust
use agentschema::{PromptAgent, FunctionTool};

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let yaml_data = r#"
name: tool-agent
kind: prompt
model: gpt-4o
instructions: You help users with weather information.
tools:
  - kind: function
    name: get_weather
    description: Get the current weather for a location
"#;

    let agent = PromptAgent::from_yaml(yaml_data)?;
    
    // Access tools via typed accessor
    if let Some(tools) = agent.as_tools() {
        for tool in tools {
            println!("Tool: {:?}", tool);
        }
    }
    
    Ok(())
}
```

## Features

- Load agent definitions from JSON or YAML
- Create agent definitions programmatically
- Serialize to JSON or YAML
- Type-safe structs for all AgentSchema types
- Typed accessors for polymorphic fields

## Documentation

- [Full Documentation](https://microsoft.github.io/AgentSchema/)
- [Rust SDK Reference](https://microsoft.github.io/AgentSchema/runtimes/rust/)

## License

MIT
