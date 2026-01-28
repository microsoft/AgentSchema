
import json
import yaml

from agentschema.core import Connection


def test_load_json_connection():
    json_data = '''
    {
      "kind": "reference",
      "authenticationMode": "system",
      "usageDescription": "This will allow the agent to respond to an email on your behalf"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = Connection.load(data)
    assert instance is not None
    assert instance.kind == "reference"
    assert instance.authenticationMode == "system"
    assert instance.usageDescription == "This will allow the agent to respond to an email on your behalf"
    

def test_load_yaml_connection():
    yaml_data = '''
    kind: reference
    authenticationMode: system
    usageDescription: This will allow the agent to respond to an email on your behalf
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = Connection.load(data)
    assert instance is not None
    assert instance.kind == "reference"
    assert instance.authenticationMode == "system"
    assert instance.usageDescription == "This will allow the agent to respond to an email on your behalf"


