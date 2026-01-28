import json
import yaml

from agentschema.core import McpServerToolNeverRequireApprovalMode


def test_load_json_mcpservertoolneverrequireapprovalmode():
    json_data = """
    {
      "kind": "never"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = McpServerToolNeverRequireApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "never"


def test_load_yaml_mcpservertoolneverrequireapprovalmode():
    yaml_data = """
    kind: never
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = McpServerToolNeverRequireApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "never"


def test_roundtrip_json_mcpservertoolneverrequireapprovalmode():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "never"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = McpServerToolNeverRequireApprovalMode.load(original_data)
    saved_data = instance.save()
    reloaded = McpServerToolNeverRequireApprovalMode.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "never"


def test_to_json_mcpservertoolneverrequireapprovalmode():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "never"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = McpServerToolNeverRequireApprovalMode.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_mcpservertoolneverrequireapprovalmode():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "never"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = McpServerToolNeverRequireApprovalMode.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
