import json
import yaml

from agentschema.core import ModelResource


def test_load_json_modelresource():
    json_data = """
    {
      "kind": "model",
      "id": "gpt-4o"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ModelResource.load(data)
    assert instance is not None
    assert instance.kind == "model"
    assert instance.id == "gpt-4o"


def test_load_yaml_modelresource():
    yaml_data = """
    kind: model
    id: gpt-4o
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ModelResource.load(data)
    assert instance is not None
    assert instance.kind == "model"
    assert instance.id == "gpt-4o"


def test_roundtrip_json_modelresource():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "model",
      "id": "gpt-4o"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = ModelResource.load(original_data)
    saved_data = instance.save()
    reloaded = ModelResource.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "model"
    assert reloaded.id == "gpt-4o"


def test_to_json_modelresource():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "model",
      "id": "gpt-4o"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ModelResource.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_modelresource():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "model",
      "id": "gpt-4o"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ModelResource.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
