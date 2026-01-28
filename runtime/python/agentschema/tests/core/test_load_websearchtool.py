
import json
import yaml

from agentschema.core import WebSearchTool


def test_load_json_websearchtool():
    json_data = '''
    {
      "kind": "bing_search",
      "connection": {
        "kind": "reference"
      },
      "options": {
        "instanceName": "MyBingInstance",
        "market": "en-US",
        "setLang": "en",
        "count": 10,
        "freshness": "Day"
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = WebSearchTool.load(data)
    assert instance is not None
    assert instance.kind == "bing_search"
    

def test_load_yaml_websearchtool():
    yaml_data = '''
    kind: bing_search
    connection:
      kind: reference
    options:
      instanceName: MyBingInstance
      market: en-US
      setLang: en
      count: 10
      freshness: Day
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = WebSearchTool.load(data)
    assert instance is not None
    assert instance.kind == "bing_search"


