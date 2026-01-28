
import json
import yaml

from agentschema.core import McpServerToolSpecifyApprovalMode


def test_load_json_mcpservertoolspecifyapprovalmode():
    json_data = '''
    {
      "kind": "specify",
      "alwaysRequireApprovalTools": [
        "operation1"
      ],
      "neverRequireApprovalTools": [
        "operation2"
      ]
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = McpServerToolSpecifyApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "specify"
    

def test_load_yaml_mcpservertoolspecifyapprovalmode():
    yaml_data = '''
    kind: specify
    alwaysRequireApprovalTools:
      - operation1
    neverRequireApprovalTools:
      - operation2
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = McpServerToolSpecifyApprovalMode.load(data)
    assert instance is not None
    assert instance.kind == "specify"


