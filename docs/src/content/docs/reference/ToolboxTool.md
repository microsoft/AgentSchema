---
title: "ToolboxTool"
description: "Documentation for the ToolboxTool type."
slug: "reference/toolboxtool"
---

Represents a tool definition within a toolbox.
Tools can be Foundry-hosted (web_search, azure_ai_search, etc.)
or external (mcp, openapi, a2a_preview) with connection details.

## Class Diagram

```mermaid
---
title: ToolboxTool
config:
  look: handDrawn
  theme: colorful
  class:
    hideEmptyMembersBox: true
---
classDiagram
    class ToolboxTool {
      
        +string id
        +string name
        +string description
        +string target
        +string authType
        +dictionary options
    }
```

## Yaml Example

```yaml
id: web_search
name: my-search-tool
description: Searches the web for up-to-date information
target: https://api.githubcopilot.com/mcp
authType: OAuth2
options:
  indexName: products-index
```

## Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | string | The tool type identifier (e.g., &#39;web_search&#39;, &#39;azure_ai_search&#39;, &#39;mcp&#39;, &#39;a2a_preview&#39;) |
| name | string | Optional display name for the tool |
| description | string | Human-readable description of the tool&#39;s capabilities |
| target | string | Target endpoint URL for external tools (e.g., MCP server URL, A2A agent URL) |
| authType | string | Authentication type for the tool connection |
| options | dictionary | Additional configuration options for the tool |
