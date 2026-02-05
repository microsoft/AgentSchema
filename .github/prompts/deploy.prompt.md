# Deploy Changes

Commit changes and deploy documentation.

## Steps

### 1. Commit and Push

```bash
git add -A
git commit -m "your commit message"
git push origin main
```

### 2. Deploy Docs (Optional)

Docs deploy automatically on `docs-v*` tags. For manual deployment:

```bash
gh workflow run "Publish Docs to GitHub Pages" --repo microsoft/AgentSchema
```

To check workflow status:

```bash
gh run list --workflow="publish-web.yml"
```

## Notes

- Docs are built with Astro and deployed to GitHub Pages
- The workflow is defined in `.github/workflows/publish-web.yml`
- For version releases, create an annotated tag: `git tag -a "docs-v1.0.0" -m "Docs v1.0.0"`

