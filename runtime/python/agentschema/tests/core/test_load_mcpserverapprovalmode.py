
import json
import yaml

from agentschema.core import McpServerApprovalMode


def test_load_json_mcpserverapprovalmode():
    json_data = '''
    {
      "kind": "never"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = McpServerApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "never"
    

def test_load_yaml_mcpserverapprovalmode():
    yaml_data = '''
    kind: never
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = McpServerApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "never"


def test_load_mcpserverapprovalmode_from_string():
    instance = McpServerApprovalMode.load("never")
    assert instance is not None
    assert instance.kind == "never"


