
import json
import yaml

from agentschema.core import ObjectProperty


def test_load_json_objectproperty():
    json_data = '''
    {
      "properties": {
        "property1": {
          "kind": "string"
        },
        "property2": {
          "kind": "number"
        }
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ObjectProperty.load(data)
    assert instance is not None
    

def test_load_yaml_objectproperty():
    yaml_data = '''
    properties:
      property1:
        kind: string
      property2:
        kind: number
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ObjectProperty.load(data)
    assert instance is not None


