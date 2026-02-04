# Publishing AgentSchema Packages

This document describes how to publish new versions of the AgentSchema runtime packages.

## Overview

Each runtime package is published independently using GitHub Actions workflows triggered by tags. Push annotated tags to trigger the appropriate workflow.

## Tag Conventions

| Runtime    | Tag Format                | Example                   |
|------------|---------------------------|---------------------------|
| Emitter/npm| `emitter-v{version}`      | `emitter-v0.2.0-beta.6`   |
| C#/NuGet   | `csharp-v{version}`       | `csharp-v1.0.0-beta.6`    |
| Python/PyPI| `python-v{version}`       | `python-v1.0.0b6`         |
| TypeScript/npm | `typescript-v{version}` | `typescript-v1.0.0-beta.6`|
| Go         | `go-v{version}`           | `go-v1.0.0-beta.6`        |
| Docs       | `docs-v{version}`         | `docs-v1.0.0-beta.6`      |

> **Note**: Python uses PEP 440 format (`1.0.0b6` not `1.0.0-beta.6`)

## Publishing a New Version

### 1. Ensure All Tests Pass

Before publishing, verify all runtime tests pass:

```bash
# Run all tests
./tests.ps1

# Or run individually:
cd runtime/csharp && dotnet test
cd runtime/python/agentschema && uv run pytest tests/
cd runtime/typescript/agentschema && npm test
cd runtime/go/agentschema && go test ./...
```

### 2. Create Annotated Tags

Always use **annotated tags** with a descriptive message:

```bash
# TypeSpec Emitter / npm (agentschema-emitter)
git tag -a "emitter-v0.1.11" -m "Emitter v0.1.11: description of changes"

# C# / NuGet
git tag -a "csharp-v1.0.0-beta.6" -m "C# SDK v1.0.0-beta.6: description of changes"

# Python / PyPI (note PEP 440 format)
git tag -a "python-v1.0.0b6" -m "Python SDK v1.0.0-beta.6: description of changes"

# TypeScript / npm
git tag -a "typescript-v1.0.0-beta.6" -m "TypeScript SDK v1.0.0-beta.6: description of changes"

# Go
git tag -a "go-v1.0.0-beta.6" -m "Go SDK v1.0.0-beta.6: description of changes"

# Documentation
git tag -a "docs-v1.0.0-beta.6" -m "Docs v1.0.0-beta.6: description of changes"
```

### 3. Push Tags

Push the tag(s) to trigger the corresponding workflow(s):

```bash
# Push a single tag
git push origin csharp-v1.0.0-beta.6

# Or push all tags at once
git push origin --tags
```

### 4. Monitor Workflows

Check the [Actions tab](https://github.com/microsoft/AgentSchema/actions) to monitor the publish workflows.

## Publishing All Runtimes (Beta 6 Example)

To publish version 1.0.0-beta.6 for all runtimes:

```bash
# Create all tags
git tag -a "csharp-v1.0.0-beta.6" -m "C# SDK v1.0.0-beta.6"
git tag -a "python-v1.0.0b6" -m "Python SDK v1.0.0-beta.6"
git tag -a "typescript-v1.0.0-beta.6" -m "TypeScript SDK v1.0.0-beta.6"
git tag -a "go-v1.0.0-beta.6" -m "Go SDK v1.0.0-beta.6"

# Push all tags
git push origin --tags
```

## Manual Workflow Dispatch

You can also trigger workflows manually from the GitHub Actions UI:

1. Go to [Actions](https://github.com/microsoft/AgentSchema/actions)
2. Select the appropriate workflow (e.g., "Publish C# to NuGet")
3. Click "Run workflow"
4. Enter the version number if prompted
5. Click "Run workflow"

## Troubleshooting

### Tag Already Exists

If you need to recreate a tag:

```bash
# Delete local tag
git tag -d csharp-v1.0.0-beta.6

# Delete remote tag
git push origin :refs/tags/csharp-v1.0.0-beta.6

# Create new tag
git tag -a "csharp-v1.0.0-beta.6" -m "C# SDK v1.0.0-beta.6"
git push origin csharp-v1.0.0-beta.6
```

### Workflow Failed

1. Check the [Actions tab](https://github.com/microsoft/AgentSchema/actions) for error details
2. Common issues:
   - Tests failed - fix the tests and create a new tag
   - Authentication issues - check that secrets (NPM_TOKEN, NUGET_API_KEY) are configured
   - PyPI OIDC - ensure Trusted Publisher is configured on PyPI

## Package Registries

- **NuGet**: <https://www.nuget.org/packages/AgentSchema>
- **PyPI**: <https://pypi.org/project/agentschema/>
- **npm**: <https://www.npmjs.com/package/agentschema>
- **Go**: `go get github.com/microsoft/AgentSchema/runtime/go/agentschema`
