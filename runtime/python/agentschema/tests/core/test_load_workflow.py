
import json
import yaml

from agentschema.core import Workflow


def test_load_json_workflow():
    json_data = '''
    {
      "kind": "workflow"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = Workflow.load(data)
    assert instance is not None
    assert instance.kind == "workflow"
    

def test_load_yaml_workflow():
    yaml_data = '''
    kind: workflow
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = Workflow.load(data)
    assert instance is not None
    assert instance.kind == "workflow"


