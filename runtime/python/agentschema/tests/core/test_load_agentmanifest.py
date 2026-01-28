import json
import yaml

from agentschema.core import AgentManifest


def test_load_json_agentmanifest():
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(data)
    assert instance is not None
    assert instance.name == "basic-prompt"
    assert instance.displayName == "My Basic Prompt"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_load_yaml_agentmanifest():
    yaml_data = """
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
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentManifest.load(data)
    assert instance is not None
    assert instance.name == "basic-prompt"
    assert instance.displayName == "My Basic Prompt"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_roundtrip_json_agentmanifest():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
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
    """
    original_data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(original_data)
    saved_data = instance.save()
    reloaded = AgentManifest.load(saved_data)
    assert reloaded is not None
    assert reloaded.name == "basic-prompt"
    assert reloaded.displayName == "My Basic Prompt"
    assert (
        reloaded.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_to_json_agentmanifest():
    """Test that to_json produces valid JSON."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_agentmanifest():
    """Test that to_yaml produces valid YAML."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)


def test_load_json_agentmanifest_1():
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(data)
    assert instance is not None
    assert instance.name == "basic-prompt"
    assert instance.displayName == "My Basic Prompt"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_load_yaml_agentmanifest_1():
    yaml_data = """
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
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentManifest.load(data)
    assert instance is not None
    assert instance.name == "basic-prompt"
    assert instance.displayName == "My Basic Prompt"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_roundtrip_json_agentmanifest_1():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
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
    """
    original_data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(original_data)
    saved_data = instance.save()
    reloaded = AgentManifest.load(saved_data)
    assert reloaded is not None
    assert reloaded.name == "basic-prompt"
    assert reloaded.displayName == "My Basic Prompt"
    assert (
        reloaded.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_to_json_agentmanifest_1():
    """Test that to_json produces valid JSON."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_agentmanifest_1():
    """Test that to_yaml produces valid YAML."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentManifest.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
