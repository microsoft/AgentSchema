import json
import yaml

from agentschema.core import Resource


def test_load_json_resource():
    json_data = """
    {
      "name": "my-resource",
      "kind": "model"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Resource.load(data)
    assert instance is not None
    assert instance.name == "my-resource"
    assert instance.kind == "model"


def test_load_yaml_resource():
    yaml_data = """
    name: my-resource
    kind: model
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = Resource.load(data)
    assert instance is not None
    assert instance.name == "my-resource"
    assert instance.kind == "model"


def test_roundtrip_json_resource():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "name": "my-resource",
      "kind": "model"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = Resource.load(original_data)
    saved_data = instance.save()
    reloaded = Resource.load(saved_data)
    assert reloaded is not None
    assert reloaded.name == "my-resource"
    assert reloaded.kind == "model"


def test_to_json_resource():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "name": "my-resource",
      "kind": "model"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Resource.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_resource():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "name": "my-resource",
      "kind": "model"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Resource.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
