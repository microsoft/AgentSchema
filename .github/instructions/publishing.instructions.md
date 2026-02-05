---
applyTo: "PUBLISHING.md"
description: "Instructions for publishing AgentSchema runtime packages"
---

# Publishing AgentSchema Packages

Full documentation: https://microsoft.github.io/AgentSchema/contributing/publishing/

## Quick Reference

Push annotated tags to trigger publish workflows:

| Runtime        | Tag Format              | Example                    |
| -------------- | ----------------------- | -------------------------- |
| Emitter/npm    | `emitter-v{version}`    | `emitter-v0.2.0-beta.7`    |
| C#/NuGet       | `csharp-v{version}`     | `csharp-v1.0.0-beta.7`     |
| Python/PyPI    | `python-v{version}`     | `python-v1.0.0b7`          |
| TypeScript/npm | `typescript-v{version}` | `typescript-v1.0.0-beta.7` |
| Go             | `go-v{version}`         | `go-v1.0.0-beta.7`         |
| Docs           | `docs-v{version}`       | `docs-v1.0.0`              |

**Note**: Python uses PEP 440 format (`1.0.0b6` not `1.0.0-beta.6`)

## Workflow

1. Ensure all tests pass
2. Create annotated tag: `git tag -a "csharp-v1.0.0-beta.6" -m "C# SDK v1.0.0-beta.6"`
3. Push tag: `git push origin csharp-v1.0.0-beta.6`
4. Monitor [Actions](https://github.com/microsoft/AgentSchema/actions)

## Known Issue: Multiple Tags Don't Trigger Workflows

**IMPORTANT**: GitHub Actions does not reliably trigger workflows when multiple tags are pushed at once.

❌ **Don't do this:**
```bash
git push origin csharp-v1.0.0-beta.7 python-v1.0.0b7 typescript-v1.0.0-beta.7 go-v1.0.0-beta.7
```

✅ **Do this instead** - trigger workflows manually with `gh`:
```bash
# Emitter (optional version parameter)
gh workflow run "Publish Emitter to npm" -f version="0.2.0-beta.7"

# C# (takes version parameter)
gh workflow run "Publish C# to NuGet" -f version="1.0.0-beta.7"

# Python (no version parameter - reads from pyproject.toml)
gh workflow run "Publish Python to PyPI"

# TypeScript (optional version parameter)
gh workflow run "Publish TypeScript to npm" -f version="1.0.0-beta.7"

# Go (optional version parameter)
gh workflow run "Publish Go Module" -f version="1.0.0-beta.7"

# Docs (no version parameter - publishes current main)
gh workflow run "Publish Docs to GitHub Pages"
```

Or push tags one at a time, waiting for each workflow to start before pushing the next.

