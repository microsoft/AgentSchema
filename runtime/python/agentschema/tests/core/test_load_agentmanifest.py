
import json
import yaml

from agentschema.core import AgentManifest


def test_load_json_agentmanifest():
    json_data = '''
    {
      "name": "basic-prompt",
      "displayName": "My Basic Prompt",
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
      "template": {
        "kind": "prompt",
        "model": "{{model_name}}",
        "instructions": "You are a poet named {{agent_name}}. Rhyme all your responses."
      },
      "parameters": {
        "strict": true,
        "properties": [
          {
            "name": "model_name",
            "kind": "string",
            "value": "gpt-4o"
          },
          {
            "name": "agent_name",
            "kind": "string",
            "value": "Research Agent"
          }
        ]
      },
      "resources": {
        "gptModelDeployment": {
          "kind": "model",
          "id": "gpt-4o"
        },
        "webSearchInstance": {
          "kind": "tool",
          "id": "web-search",
          "options": {
            "apiKey": "my-api-key"
          }
        }
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(data)
    assert instance is not None
    assert instance.name == "basic-prompt"
    assert instance.displayName == "My Basic Prompt"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"
    

def test_load_yaml_agentmanifest():
    yaml_data = '''
    name: basic-prompt
    displayName: My Basic Prompt
    description: A basic prompt that uses the GPT-3 chat API to answer questions
    metadata:
      authors:
        - sethjuarez
        - jietong
      tags:
        - example
        - prompt
    template:
      kind: prompt
      model: "{{model_name}}"
      instructions: You are a poet named {{agent_name}}. Rhyme all your responses.
    parameters:
      strict: true
      properties:
        - name: model_name
          kind: string
          value: gpt-4o
        - name: agent_name
          kind: string
          value: Research Agent
    resources:
      gptModelDeployment:
        kind: model
        id: gpt-4o
      webSearchInstance:
        kind: tool
        id: web-search
        options:
          apiKey: my-api-key
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentManifest.load(data)
    assert instance is not None
    assert instance.name == "basic-prompt"
    assert instance.displayName == "My Basic Prompt"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"

def test_load_json_agentmanifest_1():
    json_data = '''
    {
      "name": "basic-prompt",
      "displayName": "My Basic Prompt",
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
      "template": {
        "kind": "prompt",
        "model": "{{model_name}}",
        "instructions": "You are a poet named {{agent_name}}. Rhyme all your responses."
      },
      "parameters": {
        "strict": true,
        "properties": [
          {
            "name": "model_name",
            "kind": "string",
            "value": "gpt-4o"
          },
          {
            "name": "agent_name",
            "kind": "string",
            "value": "Research Agent"
          }
        ]
      },
      "resources": [
        {
          "kind": "model",
          "name": "gptModelDeployment",
          "id": "gpt-4o"
        },
        {
          "kind": "tool",
          "name": "webSearchInstance",
          "id": "web-search",
          "options": {
            "apiKey": "my-api-key"
          }
        }
      ]
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(data)
    assert instance is not None
    assert instance.name == "basic-prompt"
    assert instance.displayName == "My Basic Prompt"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"
    

def test_load_yaml_agentmanifest_1():
    yaml_data = '''
    name: basic-prompt
    displayName: My Basic Prompt
    description: A basic prompt that uses the GPT-3 chat API to answer questions
    metadata:
      authors:
        - sethjuarez
        - jietong
      tags:
        - example
        - prompt
    template:
      kind: prompt
      model: "{{model_name}}"
      instructions: You are a poet named {{agent_name}}. Rhyme all your responses.
    parameters:
      strict: true
      properties:
        - name: model_name
          kind: string
          value: gpt-4o
        - name: agent_name
          kind: string
          value: Research Agent
    resources:
      - kind: model
        name: gptModelDeployment
        id: gpt-4o
      - kind: tool
        name: webSearchInstance
        id: web-search
        options:
          apiKey: my-api-key
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentManifest.load(data)
    assert instance is not None
    assert instance.name == "basic-prompt"
    assert instance.displayName == "My Basic Prompt"
    assert instance.description == "A basic prompt that uses the GPT-3 chat API to answer questions"


