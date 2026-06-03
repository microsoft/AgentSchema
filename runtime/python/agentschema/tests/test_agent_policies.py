import json
import yaml

from agentschema import AgentPolicies


def test_load_json_agentpolicies():
    json_data = r"""
    {
      "raiConfig": {
        "raiPolicyName": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = AgentPolicies.load(data)
    assert instance is not None


def test_load_yaml_agentpolicies():
    yaml_data = r"""
    raiConfig:
      raiPolicyName: /subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentPolicies.load(data)
    assert instance is not None


def test_roundtrip_json_agentpolicies():
    """Test that load -> save -> load produces equivalent data."""
    json_data = r"""
    {
      "raiConfig": {
        "raiPolicyName": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
      }
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = AgentPolicies.load(original_data)
    saved_data = instance.save()
    reloaded = AgentPolicies.load(saved_data)
    assert reloaded is not None


def test_to_json_agentpolicies():
    """Test that to_json produces valid JSON."""
    json_data = r"""
    {
      "raiConfig": {
        "raiPolicyName": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = AgentPolicies.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_agentpolicies():
    """Test that to_yaml produces valid YAML."""
    json_data = r"""
    {
      "raiConfig": {
        "raiPolicyName": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
      }
    }
    """
    data = json.loads(json_data, strict=False)
    instance = AgentPolicies.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
