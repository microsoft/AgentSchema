# @agentschema/emitter

TypeSpec emitter for generating AgentSchema runtime libraries in multiple programming languages.

## Installation

```bash
npm install @agentschema/emitter
```

## CLI Usage

Generate runtime code for Python, C#, TypeScript, and Go:

```bash
# Generate all default targets
npx agentschema-generate -o ./generated

# Generate specific targets
npx agentschema-generate -o ./lib -t python,csharp

# Use a different root object
npx agentschema-generate -o ./lib -r AgentSchema.AgentDefinition

# Exclude specific models
npx agentschema-generate -o ./lib --omit AgentManifest,ContainerAgent
```

### Options

| Option | Short | Description |
|--------|-------|-------------|
| `--output <dir>` | `-o` | Output directory (required) |
| `--targets <list>` | `-t` | Comma-separated targets (default: `python,csharp,typescript,go`) |
| `--root-object <name>` | `-r` | Root model (default: `AgentSchema.AgentManifest`) |
| `--omit <list>` | | Models to exclude |
| `--namespace <name>` | `-n` | Override namespace |
| `--no-tests` | | Skip test generation |
| `--no-format` | | Skip formatters |

## Programmatic API

```typescript
import { generate } from "@agentschema/emitter/generate";

const result = await generate({
  output: "./generated",
  targets: ["python", "csharp", "typescript", "go"],
  rootObject: "AgentSchema.AgentManifest",
  omit: ["AgentManifest"],
});

console.log(`Generated ${result.filesGenerated} files`);
```

## Supported Targets

| Target | Description |
|--------|-------------|
| `python` | Python dataclasses with YAML/JSON serialization |
| `csharp` | C# classes with System.Text.Json |
| `typescript` | TypeScript interfaces with js-yaml |
| `go` | Go structs with encoding/json and gopkg.in/yaml.v3 |
| `markdown` | Markdown documentation |

## TypeSpec Emitter Usage

As a TypeSpec emitter in `tspconfig.yaml`:

```yaml
emit:
  - "@agentschema/emitter"
options:
  "@agentschema/emitter":
    root-object: "AgentSchema.AgentManifest"
    emit-targets:
      - type: Python
        output-dir: "./python"
      - type: CSharp
        output-dir: "./csharp"
```

## License

MIT
