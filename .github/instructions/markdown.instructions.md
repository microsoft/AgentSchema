---
applyTo: "docs/**/*.md"
description: "Markdown linting rules for documentation files"
---

# Markdown Documentation Guidelines

## Linting

All markdown files in `docs/` must pass markdownlint. Run the linter with:

```bash
npx markdownlint-cli2 "docs/src/content/docs/**/*.md"
```

## Key Rules

### MD012 - No Multiple Blank Lines

Use only single blank lines between sections. Do NOT use consecutive blank lines.

### MD040 - Fenced Code Language

Always specify a language for code blocks:

```text
# ASCII diagrams or plain text
```

```bash
# Shell commands
```

```typespec
# TypeSpec code
```

### MD004 - Unordered List Style

Use dashes (`-`) for unordered lists, not asterisks (`*`):

```markdown
- Item one
- Item two
```

### MD047 - Single Trailing Newline

Files must end with exactly one newline character.

### MD031 - Blanks Around Fences

Fenced code blocks must have blank lines before and after.

### MD022 - Blanks Around Headings

Headings must have blank lines before and after.

## Auto-Generated Files

Files in `docs/src/content/docs/reference/` are auto-generated from the TypeSpec emitter.
To fix lint errors in these files:

1. Edit templates in `agentschema-emitter/src/templates/markdown/`
2. Rebuild: `cd agentschema-emitter && npx tsc && cp -r src/templates dist/src/`
3. Regenerate: `cd agentschema && npm run generate`

## Contributing Docs

Files in `docs/src/content/docs/contributing/` are manually maintained. Ensure:

1. Code blocks have language identifiers
2. Lists use dashes (`-`)
3. Single blank lines between sections

