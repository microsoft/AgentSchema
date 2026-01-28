
import json
import yaml

from agentschema.core import AnonymousConnection


def test_load_json_anonymousconnection():
    json_data = '''
    {
      "kind": "anonymous",
      "endpoint": "https://{your-custom-endpoint}.openai.azure.com/"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = AnonymousConnection.load(data)
    assert instance is not None
    assert instance.kind == "anonymous"
    assert instance.endpoint == "https://{your-custom-endpoint}.openai.azure.com/"
    

def test_load_yaml_anonymousconnection():
    yaml_data = '''
    kind: anonymous
    endpoint: https://{your-custom-endpoint}.openai.azure.com/
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AnonymousConnection.load(data)
    assert instance is not None
    assert instance.kind == "anonymous"
    assert instance.endpoint == "https://{your-custom-endpoint}.openai.azure.com/"


