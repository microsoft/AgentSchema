
import json
import yaml

from agentschema.core import OpenApiTool


def test_load_json_openapitool():
    json_data = '''
    {
      "kind": "openapi",
      "connection": {
        "kind": "reference"
      },
      "specification": "full_sepcification_here"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = OpenApiTool.load(data)
    assert instance is not None
    assert instance.kind == "openapi"
    assert instance.specification == "full_sepcification_here"
    

def test_load_yaml_openapitool():
    yaml_data = '''
    kind: openapi
    connection:
      kind: reference
    specification: full_sepcification_here
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = OpenApiTool.load(data)
    assert instance is not None
    assert instance.kind == "openapi"
    assert instance.specification == "full_sepcification_here"


