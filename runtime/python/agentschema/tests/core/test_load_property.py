
import json
import yaml

from agentschema.core import Property


def test_load_json_property():
    json_data = '''
    {
      "name": "my-input",
      "kind": "string",
      "description": "A description of the input property",
      "required": true,
      "default": "default value",
      "example": "example value",
      "enumValues": [
        "value1",
        "value2",
        "value3"
      ]
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = Property.load(data)
    assert instance is not None
    assert instance.name == "my-input"
    assert instance.kind == "string"
    assert instance.description == "A description of the input property"
    
    assert instance.required
    assert instance.default == "default value"
    assert instance.example == "example value"
    

def test_load_yaml_property():
    yaml_data = '''
    name: my-input
    kind: string
    description: A description of the input property
    required: true
    default: default value
    example: example value
    enumValues:
      - value1
      - value2
      - value3
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = Property.load(data)
    assert instance is not None
    assert instance.name == "my-input"
    assert instance.kind == "string"
    assert instance.description == "A description of the input property"
    assert instance.required
    assert instance.default == "default value"
    assert instance.example == "example value"


def test_load_property_from_boolean():
    instance = Property.load(False)
    assert instance is not None
    assert instance.kind == "boolean"
    assert not instance.example
def test_load_property_from_float32():
    instance = Property.load(3.14)
    assert instance is not None
    assert instance.kind == "float"
    assert instance.example == 3.14
def test_load_property_from_integer():
    instance = Property.load(4)
    assert instance is not None
    assert instance.kind == "integer"
    assert instance.example == 4
def test_load_property_from_string():
    instance = Property.load("example")
    assert instance is not None
    assert instance.kind == "string"
    assert instance.example == "example"


