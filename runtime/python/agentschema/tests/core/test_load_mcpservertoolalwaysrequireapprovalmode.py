
import json
import yaml

from agentschema.core import McpServerToolAlwaysRequireApprovalMode


def test_load_json_mcpservertoolalwaysrequireapprovalmode():
    json_data = '''
    {
      "kind": "always"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = McpServerToolAlwaysRequireApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "always"
    

def test_load_yaml_mcpservertoolalwaysrequireapprovalmode():
    yaml_data = '''
    kind: always
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = McpServerToolAlwaysRequireApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "always"


