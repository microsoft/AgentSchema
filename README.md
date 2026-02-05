# AgentSchema

AgentSchema is a client facing spec designed to make creating agents easier in a code first YAML file format. It has a couple of goals:

1. Easy to read and understand
2. Easy to add to existing repos
3. Serve as an exchange format between MCS and Foundry (and hopefully more platforms in the future).
4. Create a unified agentic primitive object model (AgentSchema)

This spec is a collaboration between Microsoft Copilot Studio and Microsoft Foundry.

Read more about the project over at the [AgentSchema Documentation Site](https://microsoft.github.io/AgentSchema/).

## Object Model

You can review the existing object model in the [AgentSchema Documentation Site](https://microsoft.github.io/AgentSchema/reference/).
These documents are auto-generated from the [agentschema-emitter](./agentschema-emitter) project.

## Contributing

We welcome contributions to the AgentSchema specification! If you have suggestions, improvements, or new features you'd like to propose, please follow these steps:

1. Fork the repository
2. Create a new branch for your changes
3. Make your changes and commit them
4. Submit a pull request

Thank you for your interest in improving the AgentSchema!

## Project Setup

To set up the project locally (once forked), follow these steps:

- run `npm install` in `agentschema-emitter` folder
- run `npm install` in `agentschema` folder
- run `uv venv` and `uv sync --all-extras` in the `runtime/python/agentschema` folder

This will set up all the respective dependencies for the emitter and runtime code. Make sure to also install the [dotnet SDK](https://dotnet.microsoft.com/en-us/download) if you want to build and test the C# code, [Node.js](https://nodejs.org/en/download/) if you want to build the emitter, [Python](https://www.python.org/downloads/) if you want to run the Python code (with `uv`), and [Go](https://go.dev/dl/) if you want to build and test the Go code.

## Running the emitter

To run the emitter, navigate to the `agentschema-emitter` directory and execute the following command:

```bash
npm run build
npm run generate
```

This will generate documentation, schemas, and code in the `runtime` folder.

## Questions

If you have any questions or need further assistance, please open an issue in the repository or reach out to the maintainers directly.
