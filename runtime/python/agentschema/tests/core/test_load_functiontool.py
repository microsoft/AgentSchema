
import json
import yaml

from agentschema.core import FunctionTool


def test_load_json_functiontool():
    json_data = '''
    {
      "kind": "function",
      "parameters": {
        "properties": {
          "firstName": {
            "kind": "string",
            "value": "Jane"
          },
          "lastName": {
            "kind": "string",
            "value": "Doe"
          },
          "question": {
            "kind": "string",
            "value": "What is the meaning of life?"
          }
        }
      },
      "strict": true
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = FunctionTool.load(data)
    assert instance is not None
    assert instance.kind == "function"
    
    assert instance.strict
    

def test_load_yaml_functiontool():
    yaml_data = '''
    kind: function
    parameters:
      properties:
        firstName:
          kind: string
          value: Jane
        lastName:
          kind: string
          value: Doe
        question:
          kind: string
          value: What is the meaning of life?
    strict: true
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = FunctionTool.load(data)
    assert instance is not None
    assert instance.kind == "function"
    assert instance.strict

def test_load_json_functiontool_1():
    json_data = '''
    {
      "kind": "function",
      "parameters": {
        "properties": [
          {
            "name": "firstName",
            "kind": "string",
            "value": "Jane"
          },
          {
            "name": "lastName",
            "kind": "string",
            "value": "Doe"
          },
          {
            "name": "question",
            "kind": "string",
            "value": "What is the meaning of life?"
          }
        ]
      },
      "strict": true
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = FunctionTool.load(data)
    assert instance is not None
    assert instance.kind == "function"
    
    assert instance.strict
    

def test_load_yaml_functiontool_1():
    yaml_data = '''
    kind: function
    parameters:
      properties:
        - name: firstName
          kind: string
          value: Jane
        - name: lastName
          kind: string
          value: Doe
        - name: question
          kind: string
          value: What is the meaning of life?
    strict: true
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = FunctionTool.load(data)
    assert instance is not None
    assert instance.kind == "function"
    assert instance.strict


