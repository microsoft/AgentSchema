
import json
import yaml

from agentschema.core import RemoteConnection


def test_load_json_remoteconnection():
    json_data = '''
    {
      "kind": "remote",
      "name": "my-reference-connection",
      "endpoint": "https://{your-custom-endpoint}.openai.azure.com/"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = RemoteConnection.load(data)
    assert instance is not None
    assert instance.kind == "remote"
    assert instance.name == "my-reference-connection"
    assert instance.endpoint == "https://{your-custom-endpoint}.openai.azure.com/"
    

def test_load_yaml_remoteconnection():
    yaml_data = '''
    kind: remote
    name: my-reference-connection
    endpoint: https://{your-custom-endpoint}.openai.azure.com/
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = RemoteConnection.load(data)
    assert instance is not None
    assert instance.kind == "remote"
    assert instance.name == "my-reference-connection"
    assert instance.endpoint == "https://{your-custom-endpoint}.openai.azure.com/"


