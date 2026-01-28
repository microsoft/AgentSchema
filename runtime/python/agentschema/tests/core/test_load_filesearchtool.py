
import json
import yaml

from agentschema.core import FileSearchTool


def test_load_json_filesearchtool():
    json_data = '''
    {
      "kind": "file_search",
      "connection": {
        "kind": "reference"
      },
      "vectorStoreIds": [
        "vectorStore1",
        "vectorStore2"
      ],
      "maximumResultCount": 10,
      "ranker": "auto",
      "scoreThreshold": 0.5,
      "filters": {
        "fileType": "pdf",
        "createdAfter": "2023-01-01"
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = FileSearchTool.load(data)
    assert instance is not None
    assert instance.kind == "file_search"
    assert instance.maximumResultCount == 10
    assert instance.ranker == "auto"
    assert instance.scoreThreshold == 0.5
    

def test_load_yaml_filesearchtool():
    yaml_data = '''
    kind: file_search
    connection:
      kind: reference
    vectorStoreIds:
      - vectorStore1
      - vectorStore2
    maximumResultCount: 10
    ranker: auto
    scoreThreshold: 0.5
    filters:
      fileType: pdf
      createdAfter: 2023-01-01
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = FileSearchTool.load(data)
    assert instance is not None
    assert instance.kind == "file_search"
    assert instance.maximumResultCount == 10
    assert instance.ranker == "auto"
    assert instance.scoreThreshold == 0.5


