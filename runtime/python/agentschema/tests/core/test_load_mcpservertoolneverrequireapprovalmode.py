
import json
import yaml

from agentschema.core import McpServerToolNeverRequireApprovalMode


def test_load_json_mcpservertoolneverrequireapprovalmode():
    json_data = '''
    {
      "kind": "never"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = McpServerToolNeverRequireApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "never"
    

def test_load_yaml_mcpservertoolneverrequireapprovalmode():
    yaml_data = '''
    kind: never
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = McpServerToolNeverRequireApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "never"


