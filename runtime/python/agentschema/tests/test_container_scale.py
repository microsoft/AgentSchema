import json
import yaml

from agentschema import ContainerScale


def test_load_json_containerscale():
    json_data = """
    {
      "minReplicas": 1,
      "maxReplicas": 3
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerScale.load(data)
    assert instance is not None
    assert instance.minReplicas == 1
    assert instance.maxReplicas == 3


def test_load_yaml_containerscale():
    yaml_data = """
    minReplicas: 1
    maxReplicas: 3
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ContainerScale.load(data)
    assert instance is not None
    assert instance.minReplicas == 1
    assert instance.maxReplicas == 3


def test_roundtrip_json_containerscale():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "minReplicas": 1,
      "maxReplicas": 3
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = ContainerScale.load(original_data)
    saved_data = instance.save()
    reloaded = ContainerScale.load(saved_data)
    assert reloaded is not None
    assert reloaded.minReplicas == 1
    assert reloaded.maxReplicas == 3


def test_to_json_containerscale():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "minReplicas": 1,
      "maxReplicas": 3
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerScale.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_containerscale():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "minReplicas": 1,
      "maxReplicas": 3
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerScale.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
