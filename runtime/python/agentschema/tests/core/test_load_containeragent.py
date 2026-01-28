import json
import yaml

from agentschema.core import ContainerAgent


def test_load_json_containeragent():
    json_data = """
    {
      "kind": "hosted",
      "protocols": [
        {
          "protocol": "responses",
          "version": "v0.1.1"
        }
      ],
      "environmentVariables": [
        {
          "name": "MY_ENV_VAR",
          "value": "my-value"
        }
      ]
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerAgent.load(data)
    assert instance is not None
    assert instance.kind == "hosted"


def test_load_yaml_containeragent():
    yaml_data = """
    kind: hosted
    protocols:
      - protocol: responses
        version: v0.1.1
    environmentVariables:
      - name: MY_ENV_VAR
        value: my-value
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ContainerAgent.load(data)
    assert instance is not None
    assert instance.kind == "hosted"


def test_roundtrip_json_containeragent():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "hosted",
      "protocols": [
        {
          "protocol": "responses",
          "version": "v0.1.1"
        }
      ],
      "environmentVariables": [
        {
          "name": "MY_ENV_VAR",
          "value": "my-value"
        }
      ]
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = ContainerAgent.load(original_data)
    saved_data = instance.save()
    reloaded = ContainerAgent.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "hosted"


def test_to_json_containeragent():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "hosted",
      "protocols": [
        {
          "protocol": "responses",
          "version": "v0.1.1"
        }
      ],
      "environmentVariables": [
        {
          "name": "MY_ENV_VAR",
          "value": "my-value"
        }
      ]
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerAgent.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_containeragent():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "hosted",
      "protocols": [
        {
          "protocol": "responses",
          "version": "v0.1.1"
        }
      ],
      "environmentVariables": [
        {
          "name": "MY_ENV_VAR",
          "value": "my-value"
        }
      ]
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ContainerAgent.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
