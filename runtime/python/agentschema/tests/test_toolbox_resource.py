import json
import yaml

from agentschema import ToolboxResource


def test_load_json_toolboxresource():
    json_data = r"""
    {
      "kind": "toolbox",
      "description": "Shared platform tools",
      "tools": [
        {
          "id": "web_search"
        },
        {
          "id": "azure_ai_search",
          "options": {
            "indexName": "products-index"
          }
        },
        {
          "id": "mcp",
          "name": "github-copilot",
          "target": "https://api.githubcopilot.com/mcp",
          "authType": "OAuth2"
        },
        {
          "id": "a2a_preview",
          "name": "research-agent",
          "description": "Delegates research tasks to a specialized agent",
          "target": "https://research-agent.example.com"
        }
      ]
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolboxResource.load(data)
    assert instance is not None
    assert instance.kind == "toolbox"
    assert instance.description == "Shared platform tools"


def test_load_yaml_toolboxresource():
    yaml_data = r"""
    kind: toolbox
    description: Shared platform tools
    tools:
      - id: web_search
      - id: azure_ai_search
        options:
          indexName: products-index
      - id: mcp
        name: github-copilot
        target: "https://api.githubcopilot.com/mcp"
        authType: OAuth2
      - id: a2a_preview
        name: research-agent
        description: Delegates research tasks to a specialized agent
        target: "https://research-agent.example.com"
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ToolboxResource.load(data)
    assert instance is not None
    assert instance.kind == "toolbox"
    assert instance.description == "Shared platform tools"


def test_roundtrip_json_toolboxresource():
    """Test that load -> save -> load produces equivalent data."""
    json_data = r"""
    {
      "kind": "toolbox",
      "description": "Shared platform tools",
      "tools": [
        {
          "id": "web_search"
        },
        {
          "id": "azure_ai_search",
          "options": {
            "indexName": "products-index"
          }
        },
        {
          "id": "mcp",
          "name": "github-copilot",
          "target": "https://api.githubcopilot.com/mcp",
          "authType": "OAuth2"
        },
        {
          "id": "a2a_preview",
          "name": "research-agent",
          "description": "Delegates research tasks to a specialized agent",
          "target": "https://research-agent.example.com"
        }
      ]
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = ToolboxResource.load(original_data)
    saved_data = instance.save()
    reloaded = ToolboxResource.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "toolbox"
    assert reloaded.description == "Shared platform tools"


def test_to_json_toolboxresource():
    """Test that to_json produces valid JSON."""
    json_data = r"""
    {
      "kind": "toolbox",
      "description": "Shared platform tools",
      "tools": [
        {
          "id": "web_search"
        },
        {
          "id": "azure_ai_search",
          "options": {
            "indexName": "products-index"
          }
        },
        {
          "id": "mcp",
          "name": "github-copilot",
          "target": "https://api.githubcopilot.com/mcp",
          "authType": "OAuth2"
        },
        {
          "id": "a2a_preview",
          "name": "research-agent",
          "description": "Delegates research tasks to a specialized agent",
          "target": "https://research-agent.example.com"
        }
      ]
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolboxResource.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_toolboxresource():
    """Test that to_yaml produces valid YAML."""
    json_data = r"""
    {
      "kind": "toolbox",
      "description": "Shared platform tools",
      "tools": [
        {
          "id": "web_search"
        },
        {
          "id": "azure_ai_search",
          "options": {
            "indexName": "products-index"
          }
        },
        {
          "id": "mcp",
          "name": "github-copilot",
          "target": "https://api.githubcopilot.com/mcp",
          "authType": "OAuth2"
        },
        {
          "id": "a2a_preview",
          "name": "research-agent",
          "description": "Delegates research tasks to a specialized agent",
          "target": "https://research-agent.example.com"
        }
      ]
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolboxResource.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
