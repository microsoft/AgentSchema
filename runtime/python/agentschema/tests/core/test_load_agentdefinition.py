
import json
import yaml

from agentschema.core import AgentDefinition


def test_load_json_agentdefinition():
    json_data = '''
    {
      "kind": "prompt",
      "name": "basic-prompt",
      "displayName": "Basic Prompt Agent",
      "description": "A basic prompt that uses the GPT-3 chat API to answer questions",
      "metadata": {
        "authors": [
          "sethjuarez",
          "jietong"
        ],
        "tags": [
          "example",
          "prompt"
        ]
      },
      "inputSchema": {
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
      "outputSchema": {
        "properties": {
          "answer": {
            "kind": "string",
            "description": "The answer to the user's question."
          }
        }
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"
    

def test_load_yaml_agentdefinition():
    yaml_data = '''
    kind: prompt
    name: basic-prompt
    displayName: Basic Prompt Agent
    description: A basic prompt that uses the GPT-3 chat API to answer questions
    metadata:
      authors:
        - sethjuarez
        - jietong
      tags:
        - example
        - prompt
    inputSchema:
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
    outputSchema:
      properties:
        answer:
          kind: string
          description: The answer to the user's question.
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"

def test_load_json_agentdefinition_1():
    json_data = '''
    {
      "kind": "prompt",
      "name": "basic-prompt",
      "displayName": "Basic Prompt Agent",
      "description": "A basic prompt that uses the GPT-3 chat API to answer questions",
      "metadata": {
        "authors": [
          "sethjuarez",
          "jietong"
        ],
        "tags": [
          "example",
          "prompt"
        ]
      },
      "inputSchema": {
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
      "outputSchema": {
        "properties": [
          {
            "name": "answer",
            "kind": "string",
            "description": "The answer to the user's question."
          }
        ]
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"
    

def test_load_yaml_agentdefinition_1():
    yaml_data = '''
    kind: prompt
    name: basic-prompt
    displayName: Basic Prompt Agent
    description: A basic prompt that uses the GPT-3 chat API to answer questions
    metadata:
      authors:
        - sethjuarez
        - jietong
      tags:
        - example
        - prompt
    inputSchema:
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
    outputSchema:
      properties:
        - name: answer
          kind: string
          description: The answer to the user's question.
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"

def test_load_json_agentdefinition_2():
    json_data = '''
    {
      "kind": "prompt",
      "name": "basic-prompt",
      "displayName": "Basic Prompt Agent",
      "description": "A basic prompt that uses the GPT-3 chat API to answer questions",
      "metadata": {
        "authors": [
          "sethjuarez",
          "jietong"
        ],
        "tags": [
          "example",
          "prompt"
        ]
      },
      "inputSchema": {
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
      "outputSchema": {
        "properties": {
          "answer": {
            "kind": "string",
            "description": "The answer to the user's question."
          }
        }
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"
    

def test_load_yaml_agentdefinition_2():
    yaml_data = '''
    kind: prompt
    name: basic-prompt
    displayName: Basic Prompt Agent
    description: A basic prompt that uses the GPT-3 chat API to answer questions
    metadata:
      authors:
        - sethjuarez
        - jietong
      tags:
        - example
        - prompt
    inputSchema:
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
    outputSchema:
      properties:
        answer:
          kind: string
          description: The answer to the user's question.
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"

def test_load_json_agentdefinition_3():
    json_data = '''
    {
      "kind": "prompt",
      "name": "basic-prompt",
      "displayName": "Basic Prompt Agent",
      "description": "A basic prompt that uses the GPT-3 chat API to answer questions",
      "metadata": {
        "authors": [
          "sethjuarez",
          "jietong"
        ],
        "tags": [
          "example",
          "prompt"
        ]
      },
      "inputSchema": {
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
      "outputSchema": {
        "properties": [
          {
            "name": "answer",
            "kind": "string",
            "description": "The answer to the user's question."
          }
        ]
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"
    

def test_load_yaml_agentdefinition_3():
    yaml_data = '''
    kind: prompt
    name: basic-prompt
    displayName: Basic Prompt Agent
    description: A basic prompt that uses the GPT-3 chat API to answer questions
    metadata:
      authors:
        - sethjuarez
        - jietong
      tags:
        - example
        - prompt
    inputSchema:
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
    outputSchema:
      properties:
        - name: answer
          kind: string
          description: The answer to the user's question.
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"


