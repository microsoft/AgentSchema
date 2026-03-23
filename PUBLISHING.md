# Publishing AgentSchema Packages

See the full documentation at: <https://microsoft.github.io/AgentSchema/contributing/publishing/>

## Quick Reference

Push annotated tags to trigger publish workflows:

| Runtime        | Tag Format                | Example                    |
|----------------|---------------------------|----------------------------|
| Emitter/npm    | `emitter-v{version}`      | `emitter-v0.2.0-beta.7`    |
| C#/NuGet       | `csharp-v{version}`       | `csharp-v1.0.0-beta.7`     |
| Python/PyPI    | `python-v{version}`       | `python-v1.0.0b7`          |
| TypeScript/npm | `typescript-v{version}`   | `typescript-v1.0.0-beta.7` |
| Go             | `go-v{version}`           | `go-v1.0.0-beta.7`         |
| Rust/crates.io | `rust-v{version}`         | `rust-v1.0.0-beta.7`       |
| Docs           | `docs-v{version}`         | `docs-v1.0.0`              |

**Note**: Pushing multiple tags at once may not trigger workflows. Use `gh workflow run` instead:

```bash
# Emitter
gh workflow run "Publish Emitter to npm" -f version="0.2.0-beta.7"

# C#
gh workflow run "Publish C# to NuGet" -f version="1.0.0-beta.7"

# Python (reads version from pyproject.toml)
gh workflow run "Publish Python to PyPI"

# TypeScript
gh workflow run "Publish TypeScript to npm" -f version="1.0.0-beta.7"

# Go
gh workflow run "Publish Go Module" -f version="1.0.0-beta.7"

# Rust
gh workflow run "Publish Rust to crates.io" -f version="1.0.0-beta.7"

# Docs
gh workflow run "Publish Docs to GitHub Pages"
```
