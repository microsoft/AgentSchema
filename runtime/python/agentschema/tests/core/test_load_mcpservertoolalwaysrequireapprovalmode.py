import json
import yaml

from agentschema.core import McpServerToolAlwaysRequireApprovalMode


def test_load_json_mcpservertoolalwaysrequireapprovalmode():
    json_data = """
    {
      "kind": "always"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = McpServerToolAlwaysRequireApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "always"


def test_load_yaml_mcpservertoolalwaysrequireapprovalmode():
    yaml_data = """
    kind: always
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = McpServerToolAlwaysRequireApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "always"


def test_roundtrip_json_mcpservertoolalwaysrequireapprovalmode():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "always"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = McpServerToolAlwaysRequireApprovalMode.load(original_data)
    saved_data = instance.save()
    reloaded = McpServerToolAlwaysRequireApprovalMode.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "always"


def test_to_json_mcpservertoolalwaysrequireapprovalmode():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "always"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = McpServerToolAlwaysRequireApprovalMode.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_mcpservertoolalwaysrequireapprovalmode():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "always"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = McpServerToolAlwaysRequireApprovalMode.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
