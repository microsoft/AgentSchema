import json
import yaml

from agentschema import ContainerResources


def test_load_json_containerresources():
    json_data = """
    {
      "cpu": "1",
      "memory": "2Gi"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerResources.load(data)
    assert instance is not None
    assert instance.cpu == "1"
    assert instance.memory == "2Gi"


def test_load_yaml_containerresources():
    yaml_data = """
    cpu: "1"
    memory: 2Gi
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ContainerResources.load(data)
    assert instance is not None
    assert instance.cpu == "1"
    assert instance.memory == "2Gi"


def test_roundtrip_json_containerresources():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "cpu": "1",
      "memory": "2Gi"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = ContainerResources.load(original_data)
    saved_data = instance.save()
    reloaded = ContainerResources.load(saved_data)
    assert reloaded is not None
    assert reloaded.cpu == "1"
    assert reloaded.memory == "2Gi"


def test_to_json_containerresources():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "cpu": "1",
      "memory": "2Gi"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerResources.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_containerresources():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "cpu": "1",
      "memory": "2Gi"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerResources.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
