import json
import yaml

from agentschema.core import WebSearchTool


def test_load_json_websearchtool():
    json_data = """
    {
      "kind": "bing_search",
      "connection": {
        "kind": "reference"
      },
      "options": {
        "instanceName": "MyBingInstance",
        "market": "en-US",
        "setLang": "en",
        "count": 10,
        "freshness": "Day"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = WebSearchTool.load(data)
    assert instance is not None
    assert instance.kind == "bing_search"


def test_load_yaml_websearchtool():
    yaml_data = """
    kind: bing_search
    connection:
      kind: reference
    options:
      instanceName: MyBingInstance
      market: en-US
      setLang: en
      count: 10
      freshness: Day
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = WebSearchTool.load(data)
    assert instance is not None
    assert instance.kind == "bing_search"


def test_roundtrip_json_websearchtool():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "bing_search",
      "connection": {
        "kind": "reference"
      },
      "options": {
        "instanceName": "MyBingInstance",
        "market": "en-US",
        "setLang": "en",
        "count": 10,
        "freshness": "Day"
      }
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = WebSearchTool.load(original_data)
    saved_data = instance.save()
    reloaded = WebSearchTool.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "bing_search"


def test_to_json_websearchtool():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "bing_search",
      "connection": {
        "kind": "reference"
      },
      "options": {
        "instanceName": "MyBingInstance",
        "market": "en-US",
        "setLang": "en",
        "count": 10,
        "freshness": "Day"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = WebSearchTool.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_websearchtool():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "bing_search",
      "connection": {
        "kind": "reference"
      },
      "options": {
        "instanceName": "MyBingInstance",
        "market": "en-US",
        "setLang": "en",
        "count": 10,
        "freshness": "Day"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = WebSearchTool.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
