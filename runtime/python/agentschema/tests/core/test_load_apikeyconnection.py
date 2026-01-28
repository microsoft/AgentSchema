
import json
import yaml

from agentschema.core import ApiKeyConnection


def test_load_json_apikeyconnection():
    json_data = '''
    {
      "kind": "key",
      "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
      "apiKey": "your-api-key"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ApiKeyConnection.load(data)
    assert instance is not None
    assert instance.kind == "key"
    assert instance.endpoint == "https://{your-custom-endpoint}.openai.azure.com/"
    assert instance.apiKey == "your-api-key"
    

def test_load_yaml_apikeyconnection():
    yaml_data = '''
    kind: key
    endpoint: https://{your-custom-endpoint}.openai.azure.com/
    apiKey: your-api-key
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ApiKeyConnection.load(data)
    assert instance is not None
    assert instance.kind == "key"
    assert instance.endpoint == "https://{your-custom-endpoint}.openai.azure.com/"
    assert instance.apiKey == "your-api-key"


