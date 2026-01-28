import json
import yaml

from agentschema.core import ToolResource


def test_load_json_toolresource():
    json_data = """
    {
      "kind": "tool",
      "id": "web-search",
      "options": {
        "myToolResourceProperty": "myValue"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolResource.load(data)
    assert instance is not None
    assert instance.kind == "tool"
    assert instance.id == "web-search"


def test_load_yaml_toolresource():
    yaml_data = """
    kind: tool
    id: web-search
    options:
      myToolResourceProperty: myValue
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ToolResource.load(data)
    assert instance is not None
    assert instance.kind == "tool"
    assert instance.id == "web-search"


def test_roundtrip_json_toolresource():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "tool",
      "id": "web-search",
      "options": {
        "myToolResourceProperty": "myValue"
      }
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = ToolResource.load(original_data)
    saved_data = instance.save()
    reloaded = ToolResource.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "tool"
    assert reloaded.id == "web-search"


def test_to_json_toolresource():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "tool",
      "id": "web-search",
      "options": {
        "myToolResourceProperty": "myValue"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolResource.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_toolresource():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "tool",
      "id": "web-search",
      "options": {
        "myToolResourceProperty": "myValue"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ToolResource.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
