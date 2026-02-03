---
title: Project Setup
description: Set up your development environment for AgentSchema
sidebar:
  order: 2
---

This guide walks you through setting up a local development environment for AgentSchema.

## Prerequisites

You'll need the following tools installed:

| Tool | Purpose | Installation |
|------|---------|--------------|
| **Node.js** (v18+) | TypeSpec compiler & emitter | [nodejs.org](https://nodejs.org/) |
| **.NET SDK** (9.0+) | C# runtime development | [dotnet.microsoft.com](https://dotnet.microsoft.com/download) |
| **Python** (3.11+) | Python runtime development | [python.org](https://www.python.org/downloads/) |
| **uv** | Python package manager | `pip install uv` or [docs.astral.sh](https://docs.astral.sh/uv/) |

## Clone & Install

```bash
# Clone the repository
git clone https://github.com/microsoft/AgentSchema.git
cd AgentSchema

# Install emitter dependencies
cd agentschema-emitter
npm install

# Install schema compiler dependencies
cd ../agentschema
npm install

# Set up Python environment
cd ../runtime/python/agentschema
uv venv
uv sync --all-extras

# Install TypeScript test dependencies
cd ../../typescript/agentschema
npm install
```

## Project Structure

```text
AgentSchema/
├── agentschema/              # TypeSpec source (schema definitions)
│   ├── model/                # .tsp files - THE SOURCE OF TRUTH
│   │   ├── main.tsp          # Entry point
│   │   ├── agent.tsp         # Agent definitions
│   │   ├── tools.tsp         # Tool definitions
│   │   └── ...
│   └── tspconfig.yaml        # Compiler configuration
│
├── agentschema-emitter/      # Code generator
│   ├── src/
│   │   ├── emitter.ts        # Main entry point
│   │   ├── csharp.ts         # C# generator
│   │   ├── python.ts         # Python generator
│   │   ├── typescript.ts     # TypeScript generator
│   │   └── templates/        # Nunjucks templates
│   └── dist/                 # Compiled output
│
├── runtime/                  # Generated libraries (DO NOT EDIT)
│   ├── csharp/
│   ├── python/
│   └── typescript/
│
├── schemas/                  # Generated JSON schemas (DO NOT EDIT)
│
└── docs/                     # Documentation site
```

## Verify Installation

Run the full build to verify everything works:

```bash
# Build the emitter
cd agentschema-emitter
npx tsc
cp -r src/templates dist/src/

# Generate all code
cd ../agentschema
npm run generate

# Run all tests
cd ../runtime/csharp && dotnet test
cd ../python/agentschema && uv run pytest tests/
cd ../../typescript/agentschema && npm test
```

All tests should pass. If you encounter issues, check:

- Node.js version is 18+
- .NET SDK is installed and in PATH
- Python environment is activated

## VS Code Setup

We recommend VS Code with these extensions:

- **TypeSpec for VS Code** - Syntax highlighting for `.tsp` files
- **Python** - Python language support
- **C# Dev Kit** - C# development
- **Nunjucks** - Template syntax highlighting

The repository includes `.vscode/settings.json` with recommended settings.

## Next Steps

- [TypeSpec Guide](/AgentSchema/contributing/typespec/) - Learn to edit schema definitions
- [Emitter Guide](/AgentSchema/contributing/emitter/) - Learn to modify code generation
