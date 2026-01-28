
import json
import yaml

from agentschema.core import ArrayProperty


def test_load_json_arrayproperty():
    json_data = '''
    {
      "items": {
        "kind": "string"
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ArrayProperty.load(data)
    assert instance is not None
    

def test_load_yaml_arrayproperty():
    yaml_data = '''
    items:
      kind: string
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ArrayProperty.load(data)
    assert instance is not None


