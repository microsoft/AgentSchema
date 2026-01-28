import json
import yaml

from agentschema.core import ProtocolVersionRecord


def test_load_json_protocolversionrecord():
    json_data = """
    {
      "protocol": "responses",
      "version": "v0.1.1"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ProtocolVersionRecord.load(data)
    assert instance is not None
    assert instance.protocol == "responses"
    assert instance.version == "v0.1.1"


def test_load_yaml_protocolversionrecord():
    yaml_data = """
    protocol: responses
    version: v0.1.1
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ProtocolVersionRecord.load(data)
    assert instance is not None
    assert instance.protocol == "responses"
    assert instance.version == "v0.1.1"


def test_roundtrip_json_protocolversionrecord():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "protocol": "responses",
      "version": "v0.1.1"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = ProtocolVersionRecord.load(original_data)
    saved_data = instance.save()
    reloaded = ProtocolVersionRecord.load(saved_data)
    assert reloaded is not None
    assert reloaded.protocol == "responses"
    assert reloaded.version == "v0.1.1"


def test_to_json_protocolversionrecord():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "protocol": "responses",
      "version": "v0.1.1"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ProtocolVersionRecord.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_protocolversionrecord():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "protocol": "responses",
      "version": "v0.1.1"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = ProtocolVersionRecord.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
