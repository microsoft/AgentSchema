# AgentSchema Rust SDK

Rust implementation of the AgentSchema specification for AI agent definitions.

## Installation

Add to your `Cargo.toml`:

```toml
[dependencies]
agentschema = { git = "https://github.com/microsoft/AgentSchema", subdirectory = "runtime/rust/agentschema" }
```

## Usage

### Loading an Agent Definition

```rust
use agentschema::PromptAgent;

fn main() {
    // From YAML
    let yaml_data = r#"
name: my-agent
kind: prompt
model: gpt-4o
instructions: You are a helpful assistant.
"#;

    let agent = PromptAgent::from_yaml(yaml_data).expect("Failed to parse agent");
    println!("Agent: {}", agent.name);

    // From JSON
    let json_data = r#"{
        "name": "my-agent",
        "kind": "prompt",
        "model": "gpt-4o",
        "instructions": "You are a helpful assistant."
    }"#;

    let json_agent = PromptAgent::from_json(json_data).expect("Failed to parse agent");
    println!("Agent: {}", json_agent.name);
}
```

### Creating an Agent Definition

```rust
use agentschema::{PromptAgent, Model};

fn main() {
    let model_json = r#"{"id": "gpt-4o"}"#;
    let model = Model::from_json(model_json).expect("Failed to parse model");

    let agent_json = serde_json::json!({
        "kind": "prompt",
        "name": "my-agent",
        "description": "A sample agent",
        "instructions": "You are a helpful assistant.",
        "model": { "id": "gpt-4o" }
    });

    let agent = PromptAgent::from_json(&agent_json.to_string()).expect("Failed to build agent");

    // Serialize to JSON
    let json_str = agent.to_json().expect("Failed to serialize to JSON");
    println!("{}", json_str);

    // Serialize to YAML
    let yaml_str = agent.to_yaml().expect("Failed to serialize to YAML");
    println!("{}", yaml_str);
}
```

## Features

- **Type-safe**: Strongly-typed Rust structs generated from TypeSpec schema
- **JSON/YAML Support**: Built-in serialization and deserialization via `serde`
- **Polymorphic Types**: Support for discriminated unions with dispatch methods
- **Shorthand Syntax**: Alternate scalar representations via custom serde `Deserialize`

## Code Generation

This code is auto-generated from the AgentSchema TypeSpec definitions. Do not edit the generated files in `src/` directly.

To regenerate:

```bash
cd agentschema-emitter
npm run generate
```

## Testing

```bash
cd runtime/rust/agentschema
cargo test
```

## License

MIT
