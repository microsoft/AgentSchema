
import json
import yaml

from agentschema.core import ToolResource


def test_load_json_toolresource():
    json_data = '''
    {
      "kind": "tool",
      "id": "web-search",
      "options": {
        "myToolResourceProperty": "myValue"
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ToolResource.load(data)
    assert instance is not None
    assert instance.kind == "tool"
    assert instance.id == "web-search"
    

def test_load_yaml_toolresource():
    yaml_data = '''
    kind: tool
    id: web-search
    options:
      myToolResourceProperty: myValue
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ToolResource.load(data)
    assert instance is not None
    assert instance.kind == "tool"
    assert instance.id == "web-search"


