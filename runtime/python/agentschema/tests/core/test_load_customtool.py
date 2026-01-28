
import json
import yaml

from agentschema.core import CustomTool


def test_load_json_customtool():
    json_data = '''
    {
      "connection": {
        "kind": "reference"
      },
      "options": {
        "timeout": 30,
        "retries": 3
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = CustomTool.load(data)
    assert instance is not None
    

def test_load_yaml_customtool():
    yaml_data = '''
    connection:
      kind: reference
    options:
      timeout: 30
      retries: 3
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = CustomTool.load(data)
    assert instance is not None


