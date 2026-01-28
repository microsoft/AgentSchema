
import json
import yaml

from agentschema.core import Model


def test_load_json_model():
    json_data = '''
    {
      "id": "gpt-35-turbo",
      "provider": "azure",
      "apiType": "chat",
      "connection": {
        "kind": "key",
        "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
        "key": "{your-api-key}"
      },
      "options": {
        "type": "chat",
        "temperature": 0.7,
        "maxTokens": 1000
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = Model.load(data)
    assert instance is not None
    assert instance.id == "gpt-35-turbo"
    assert instance.provider == "azure"
    assert instance.apiType == "chat"
    

def test_load_yaml_model():
    yaml_data = '''
    id: gpt-35-turbo
    provider: azure
    apiType: chat
    connection:
      kind: key
      endpoint: https://{your-custom-endpoint}.openai.azure.com/
      key: "{your-api-key}"
    options:
      type: chat
      temperature: 0.7
      maxTokens: 1000
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = Model.load(data)
    assert instance is not None
    assert instance.id == "gpt-35-turbo"
    assert instance.provider == "azure"
    assert instance.apiType == "chat"


def test_load_model_from_string():
    instance = Model.load("example")
    assert instance is not None
    assert instance.id == "example"


