# Publishing AgentSchema Packages

See the full documentation at: <https://microsoft.github.io/AgentSchema/contributing/publishing/>

## Quick Reference

Push annotated tags to trigger publish workflows:

| Runtime    | Tag Format                | Example                   |
|------------|---------------------------|---------------------------|
| Emitter/npm| `emitter-v{version}`      | `emitter-v0.2.0-beta.6`   |
| C#/NuGet   | `csharp-v{version}`       | `csharp-v1.0.0-beta.6`    |
| Python/PyPI| `python-v{version}`       | `python-v1.0.0b6`         |
| TypeScript/npm | `typescript-v{version}` | `typescript-v1.0.0-beta.6`|
| Go         | `go-v{version}`           | `go-v1.0.0-beta.6`        |

```bash
git tag -a "csharp-v1.0.0-beta.6" -m "C# SDK v1.0.0-beta.6"
git push origin csharp-v1.0.0-beta.6
```
