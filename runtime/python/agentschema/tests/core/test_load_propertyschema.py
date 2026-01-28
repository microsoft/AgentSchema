
import json
import yaml

from agentschema.core import PropertySchema


def test_load_json_propertyschema():
    json_data = '''
    {
      "examples": [
        {
          "key": "value"
        }
      ],
      "strict": true,
      "properties": {
        "firstName": {
          "kind": "string",
          "sample": "Jane"
        },
        "lastName": {
          "kind": "string",
          "sample": "Doe"
        },
        "question": {
          "kind": "string",
          "sample": "What is the meaning of life?"
        }
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = PropertySchema.load(data)
    assert instance is not None
    
    assert instance.strict
    

def test_load_yaml_propertyschema():
    yaml_data = '''
    examples:
      - key: value
    strict: true
    properties:
      firstName:
        kind: string
        sample: Jane
      lastName:
        kind: string
        sample: Doe
      question:
        kind: string
        sample: What is the meaning of life?
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = PropertySchema.load(data)
    assert instance is not None
    assert instance.strict


