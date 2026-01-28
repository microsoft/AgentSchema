import json
import yaml

from agentschema.core import Workflow


def test_load_json_workflow():
    json_data = """
    {
      "kind": "workflow"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Workflow.load(data)
    assert instance is not None
    assert instance.kind == "workflow"


def test_load_yaml_workflow():
    yaml_data = """
    kind: workflow
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = Workflow.load(data)
    assert instance is not None
    assert instance.kind == "workflow"


def test_roundtrip_json_workflow():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "workflow"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = Workflow.load(original_data)
    saved_data = instance.save()
    reloaded = Workflow.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "workflow"


def test_to_json_workflow():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "workflow"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Workflow.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_workflow():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "workflow"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Workflow.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
