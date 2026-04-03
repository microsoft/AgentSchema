import json
import yaml

from agentschema import ToolboxTool


def test_load_json_toolboxtool():
    json_data = r"""
    {
      "id": "bing_grounding",
      "name": "my-search-tool",
      "target": "https://api.githubcopilot.com/mcp",
      "authType": "OAuth2",
      "options": {
        "indexName": "products-index"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolboxTool.load(data)
    assert instance is not None
    assert instance.id == "bing_grounding"
    assert instance.name == "my-search-tool"
    assert instance.target == "https://api.githubcopilot.com/mcp"
    assert instance.authType == "OAuth2"


def test_load_yaml_toolboxtool():
    yaml_data = r"""
    id: bing_grounding
    name: my-search-tool
    target: "https://api.githubcopilot.com/mcp"
    authType: OAuth2
    options:
      indexName: products-index
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ToolboxTool.load(data)
    assert instance is not None
    assert instance.id == "bing_grounding"
    assert instance.name == "my-search-tool"
    assert instance.target == "https://api.githubcopilot.com/mcp"
    assert instance.authType == "OAuth2"


def test_roundtrip_json_toolboxtool():
    """Test that load -> save -> load produces equivalent data."""
    json_data = r"""
    {
      "id": "bing_grounding",
      "name": "my-search-tool",
      "target": "https://api.githubcopilot.com/mcp",
      "authType": "OAuth2",
      "options": {
        "indexName": "products-index"
      }
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = ToolboxTool.load(original_data)
    saved_data = instance.save()
    reloaded = ToolboxTool.load(saved_data)
    assert reloaded is not None
    assert reloaded.id == "bing_grounding"
    assert reloaded.name == "my-search-tool"
    assert reloaded.target == "https://api.githubcopilot.com/mcp"
    assert reloaded.authType == "OAuth2"


def test_to_json_toolboxtool():
    """Test that to_json produces valid JSON."""
    json_data = r"""
    {
      "id": "bing_grounding",
      "name": "my-search-tool",
      "target": "https://api.githubcopilot.com/mcp",
      "authType": "OAuth2",
      "options": {
        "indexName": "products-index"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolboxTool.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_toolboxtool():
    """Test that to_yaml produces valid YAML."""
    json_data = r"""
    {
      "id": "bing_grounding",
      "name": "my-search-tool",
      "target": "https://api.githubcopilot.com/mcp",
      "authType": "OAuth2",
      "options": {
        "indexName": "products-index"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolboxTool.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
