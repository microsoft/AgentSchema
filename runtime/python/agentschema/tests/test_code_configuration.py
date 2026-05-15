import json
import yaml

from agentschema import CodeConfiguration


def test_load_json_codeconfiguration():
    json_data = r"""
    {
      "runtime": "python_3_11",
      "entryPoint": "main.py",
      "dependencyResolution": "remote_build"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = CodeConfiguration.load(data)
    assert instance is not None
    assert instance.runtime == "python_3_11"
    assert instance.entryPoint == "main.py"
    assert instance.dependencyResolution == "remote_build"


def test_load_yaml_codeconfiguration():
    yaml_data = r"""
    runtime: python_3_11
    entryPoint: main.py
    dependencyResolution: remote_build
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = CodeConfiguration.load(data)
    assert instance is not None
    assert instance.runtime == "python_3_11"
    assert instance.entryPoint == "main.py"
    assert instance.dependencyResolution == "remote_build"


def test_roundtrip_json_codeconfiguration():
    """Test that load -> save -> load produces equivalent data."""
    json_data = r"""
    {
      "runtime": "python_3_11",
      "entryPoint": "main.py",
      "dependencyResolution": "remote_build"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = CodeConfiguration.load(original_data)
    saved_data = instance.save()
    reloaded = CodeConfiguration.load(saved_data)
    assert reloaded is not None
    assert reloaded.runtime == "python_3_11"
    assert reloaded.entryPoint == "main.py"
    assert reloaded.dependencyResolution == "remote_build"


def test_to_json_codeconfiguration():
    """Test that to_json produces valid JSON."""
    json_data = r"""
    {
      "runtime": "python_3_11",
      "entryPoint": "main.py",
      "dependencyResolution": "remote_build"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = CodeConfiguration.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_codeconfiguration():
    """Test that to_yaml produces valid YAML."""
    json_data = r"""
    {
      "runtime": "python_3_11",
      "entryPoint": "main.py",
      "dependencyResolution": "remote_build"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = CodeConfiguration.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
