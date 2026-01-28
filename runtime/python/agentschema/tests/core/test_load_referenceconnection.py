
import json
import yaml

from agentschema.core import ReferenceConnection


def test_load_json_referenceconnection():
    json_data = '''
    {
      "kind": "reference",
      "name": "my-reference-connection",
      "target": "my-target-resource"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ReferenceConnection.load(data)
    assert instance is not None
    assert instance.kind == "reference"
    assert instance.name == "my-reference-connection"
    assert instance.target == "my-target-resource"
    

def test_load_yaml_referenceconnection():
    yaml_data = '''
    kind: reference
    name: my-reference-connection
    target: my-target-resource
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ReferenceConnection.load(data)
    assert instance is not None
    assert instance.kind == "reference"
    assert instance.name == "my-reference-connection"
    assert instance.target == "my-target-resource"


