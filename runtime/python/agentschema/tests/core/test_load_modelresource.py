
import json
import yaml

from agentschema.core import ModelResource


def test_load_json_modelresource():
    json_data = '''
    {
      "kind": "model",
      "id": "gpt-4o"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ModelResource.load(data)
    assert instance is not None
    assert instance.kind == "model"
    assert instance.id == "gpt-4o"
    

def test_load_yaml_modelresource():
    yaml_data = '''
    kind: model
    id: gpt-4o
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ModelResource.load(data)
    assert instance is not None
    assert instance.kind == "model"
    assert instance.id == "gpt-4o"


