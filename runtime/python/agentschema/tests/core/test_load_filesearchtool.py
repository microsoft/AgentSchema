import json
import yaml

from agentschema.core import FileSearchTool


def test_load_json_filesearchtool():
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = FileSearchTool.load(data)
    assert instance is not None
    assert instance.kind == "file_search"
    assert instance.maximumResultCount == 10
    assert instance.ranker == "auto"
    assert instance.scoreThreshold == 0.5


def test_load_yaml_filesearchtool():
    yaml_data = """
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
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = FileSearchTool.load(data)
    assert instance is not None
    assert instance.kind == "file_search"
    assert instance.maximumResultCount == 10
    assert instance.ranker == "auto"
    assert instance.scoreThreshold == 0.5


def test_roundtrip_json_filesearchtool():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
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
    """
    original_data = json.loads(json_data, strict=False)
    instance = FileSearchTool.load(original_data)
    saved_data = instance.save()
    reloaded = FileSearchTool.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "file_search"
    assert reloaded.maximumResultCount == 10
    assert reloaded.ranker == "auto"
    assert reloaded.scoreThreshold == 0.5


def test_to_json_filesearchtool():
    """Test that to_json produces valid JSON."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = FileSearchTool.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_filesearchtool():
    """Test that to_yaml produces valid YAML."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = FileSearchTool.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
