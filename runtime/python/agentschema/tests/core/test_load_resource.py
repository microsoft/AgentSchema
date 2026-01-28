
import json
import yaml

from agentschema.core import Resource


def test_load_json_resource():
    json_data = '''
    {
      "name": "my-resource",
      "kind": "model"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = Resource.load(data)
    assert instance is not None
    assert instance.name == "my-resource"
    assert instance.kind == "model"
    

def test_load_yaml_resource():
    yaml_data = '''
    name: my-resource
    kind: model
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = Resource.load(data)
    assert instance is not None
    assert instance.name == "my-resource"
    assert instance.kind == "model"


