---
applyTo: ""
description: "Checklist for adding a new language runtime"
---

# Adding a New Language Checklist

When adding support for a new language (e.g., Java, Rust), update all these locations:

## Emitter Source (`agentschema-emitter/src/`)

- [ ] Create `{language}.ts` code generator
- [ ] Create `templates/{language}/` directory with templates:
  - `file.{ext}.njk` - Main class template
  - `test.njk` - Test template
  - `_macros.njk` - Shared macros (if needed)
- [ ] Update `emitter.ts` to import and call the new generator
- [ ] Update `generate.ts` to include the new target in defaults

## Documentation (`docs/src/content/docs/`)

### Main Pages

- [ ] `index.mdx` - Add TabItem for installation and usage examples
- [ ] `index.mdx` - Update "Runtimes" card prose text

### Runtimes Section

- [ ] Create `runtimes/{language}.md` - Full runtime documentation
- [ ] `runtimes/index.mdx` - Add TabItem to all tab groups (5-6 groups)
- [ ] `runtimes/index.mdx` - Add Card in CardGrid
- [ ] `runtimes/index.mdx` - Add bullet in "Choosing a Runtime" list
- [ ] Update "Next Steps" in other runtime pages (`csharp.md`, `python.md`, `typescript.md`, `go.md`)

### Guides Section

- [ ] `guides/index.mdx` - Update prose listing languages
- [ ] `guides/emitter.mdx` - Add TabItem for output example
- [ ] `guides/code-generation.mdx` - Update tables and target lists

### Contributing Section

- [ ] `contributing/index.mdx` - Update Mermaid diagram and prose
- [ ] `contributing/emitter.mdx` - Update FileTree, Mermaid, test commands, template examples
- [ ] `contributing/testing.mdx` - Add TabItem for test commands, update Mermaid diagram, update tables
- [ ] `contributing/typespec.mdx` - Add column to type mappings table, add test command
- [ ] `contributing/setup.mdx` - Add to prerequisites table, update test commands

## GitHub Configuration (`.github/`)

### Instructions Files

- [ ] `instructions/emitter.instructions.md` - Update prose listing languages
- [ ] `instructions/templates.instructions.md` - Add language-specific conventions section
- [ ] `instructions/markdown.instructions.md` - Add icon to available icons list

### Prompts

- [ ] `prompts/add-property.prompt.md` - Add test command, add column to type table
- [ ] `prompts/regenerate.prompt.md` - Add test command
- [ ] `copilot-instructions.md` - Update FileTree and test commands

## Root Files

- [ ] `README.md` - Update if language-specific instructions exist
- [ ] `PUBLISHING.md` - Add publishing instructions for the new package

## Runtime

- [ ] Create `runtime/{language}/` directory structure
- [ ] Add package configuration (e.g., `go.mod`, `setup.py`, `package.json`)
- [ ] Add README.md for the package

## CI/CD (`.github/workflows/`)

- [ ] Create `publish-{language}.yml` for package publishing
- [ ] Update any shared CI workflows if needed

## Verification

After all updates:

```bash
cd agentschema-emitter && npm run build && npm run generate
# Test all runtimes including the new one
```
