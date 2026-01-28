import json
import yaml

from agentschema.core import AgentDefinition


def test_load_json_agentdefinition():
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_load_yaml_agentdefinition():
    yaml_data = """
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
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_roundtrip_json_agentdefinition():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
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
    """
    original_data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(original_data)
    saved_data = instance.save()
    reloaded = AgentDefinition.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "prompt"
    assert reloaded.name == "basic-prompt"
    assert reloaded.displayName == "Basic Prompt Agent"
    assert (
        reloaded.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_to_json_agentdefinition():
    """Test that to_json produces valid JSON."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_agentdefinition():
    """Test that to_yaml produces valid YAML."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)


def test_load_json_agentdefinition_1():
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_load_yaml_agentdefinition_1():
    yaml_data = """
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
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_roundtrip_json_agentdefinition_1():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
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
    """
    original_data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(original_data)
    saved_data = instance.save()
    reloaded = AgentDefinition.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "prompt"
    assert reloaded.name == "basic-prompt"
    assert reloaded.displayName == "Basic Prompt Agent"
    assert (
        reloaded.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_to_json_agentdefinition_1():
    """Test that to_json produces valid JSON."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_agentdefinition_1():
    """Test that to_yaml produces valid YAML."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)


def test_load_json_agentdefinition_2():
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_load_yaml_agentdefinition_2():
    yaml_data = """
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
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_roundtrip_json_agentdefinition_2():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
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
    """
    original_data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(original_data)
    saved_data = instance.save()
    reloaded = AgentDefinition.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "prompt"
    assert reloaded.name == "basic-prompt"
    assert reloaded.displayName == "Basic Prompt Agent"
    assert (
        reloaded.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_to_json_agentdefinition_2():
    """Test that to_json produces valid JSON."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_agentdefinition_2():
    """Test that to_yaml produces valid YAML."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)


def test_load_json_agentdefinition_3():
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_load_yaml_agentdefinition_3():
    yaml_data = """
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
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = AgentDefinition.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert instance.name == "basic-prompt"
    assert instance.displayName == "Basic Prompt Agent"
    assert (
        instance.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_roundtrip_json_agentdefinition_3():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
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
    """
    original_data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(original_data)
    saved_data = instance.save()
    reloaded = AgentDefinition.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "prompt"
    assert reloaded.name == "basic-prompt"
    assert reloaded.displayName == "Basic Prompt Agent"
    assert (
        reloaded.description
        == "A basic prompt that uses the GPT-3 chat API to answer questions"
    )


def test_to_json_agentdefinition_3():
    """Test that to_json produces valid JSON."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_agentdefinition_3():
    """Test that to_yaml produces valid YAML."""
    json_data = """
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
    """
    data = json.loads(json_data, strict=False)
    instance = AgentDefinition.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
