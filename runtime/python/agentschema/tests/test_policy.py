import json
import yaml

from agentschema import Policy


def test_load_json_policy():
    json_data = r"""
    {
      "raiPolicyName": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Policy.load(data)
    assert instance is not None
    assert (
        instance.raiPolicyName
        == "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
    )


def test_load_yaml_policy():
    yaml_data = r"""
    raiPolicyName: /subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = Policy.load(data)
    assert instance is not None
    assert (
        instance.raiPolicyName
        == "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
    )


def test_roundtrip_json_policy():
    """Test that load -> save -> load produces equivalent data."""
    json_data = r"""
    {
      "raiPolicyName": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = Policy.load(original_data)
    saved_data = instance.save()
    reloaded = Policy.load(saved_data)
    assert reloaded is not None
    assert (
        reloaded.raiPolicyName
        == "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
    )


def test_to_json_policy():
    """Test that to_json produces valid JSON."""
    json_data = r"""
    {
      "raiPolicyName": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Policy.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_policy():
    """Test that to_yaml produces valid YAML."""
    json_data = r"""
    {
      "raiPolicyName": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/my-rg/providers/Microsoft.CognitiveServices/accounts/my-account/raiPolicies/Microsoft.DefaultV2"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = Policy.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
