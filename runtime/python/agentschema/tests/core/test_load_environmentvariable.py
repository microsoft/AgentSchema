import json
import yaml

from agentschema.core import EnvironmentVariable


def test_load_json_environmentvariable():
    json_data = """
    {
      "name": "MY_ENV_VAR",
      "value": "my-value"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = EnvironmentVariable.load(data)
    assert instance is not None
    assert instance.name == "MY_ENV_VAR"
    assert instance.value == "my-value"


def test_load_yaml_environmentvariable():
    yaml_data = """
    name: MY_ENV_VAR
    value: my-value
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = EnvironmentVariable.load(data)
    assert instance is not None
    assert instance.name == "MY_ENV_VAR"
    assert instance.value == "my-value"


def test_roundtrip_json_environmentvariable():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "name": "MY_ENV_VAR",
      "value": "my-value"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = EnvironmentVariable.load(original_data)
    saved_data = instance.save()
    reloaded = EnvironmentVariable.load(saved_data)
    assert reloaded is not None
    assert reloaded.name == "MY_ENV_VAR"
    assert reloaded.value == "my-value"


def test_to_json_environmentvariable():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "name": "MY_ENV_VAR",
      "value": "my-value"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = EnvironmentVariable.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_environmentvariable():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "name": "MY_ENV_VAR",
      "value": "my-value"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = EnvironmentVariable.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
