import json
import yaml

from agentschema.core import PromptAgent


def test_load_json_promptagent():
    json_data = """
    {
      "kind": "prompt",
      "model": {
        "id": "gpt-35-turbo",
        "connection": {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "key": "{your-api-key}"
        }
      },
      "tools": [
        {
          "name": "getCurrentWeather",
          "kind": "function",
          "description": "Get the current weather in a given location",
          "parameters": {
            "location": {
              "kind": "string",
              "description": "The city and state, e.g. San Francisco, CA"
            },
            "unit": {
              "kind": "string",
              "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
            }
          }
        }
      ],
      "template": {
        "format": "mustache",
        "parser": "prompty"
      },
      "instructions": "system:\nYou are an AI assistant who helps people find information.\nAs the assistant, you answer questions briefly, succinctly,\nand in a personable manner using markdown and even add some \npersonal flair with appropriate emojis.\n\n# Customer\nYou are helping {{firstName}} {{lastName}} to find answers to \ntheir questions. Use their name to address them in your responses.\nuser:\n{{question}}"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = PromptAgent.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert (
        instance.instructions
        == """system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}"""
    )


def test_load_yaml_promptagent():
    yaml_data = """
    kind: prompt
    model:
      id: gpt-35-turbo
      connection:
        kind: key
        endpoint: https://{your-custom-endpoint}.openai.azure.com/
        key: "{your-api-key}"
    tools:
      - name: getCurrentWeather
        kind: function
        description: Get the current weather in a given location
        parameters:
          location:
            kind: string
            description: The city and state, e.g. San Francisco, CA
          unit:
            kind: string
            description: The unit of temperature, e.g. Celsius or Fahrenheit
    template:
      format: mustache
      parser: prompty
    instructions: |-
      system:
      You are an AI assistant who helps people find information.
      As the assistant, you answer questions briefly, succinctly,
      and in a personable manner using markdown and even add some 
      personal flair with appropriate emojis.
    
      # Customer
      You are helping {{firstName}} {{lastName}} to find answers to 
      their questions. Use their name to address them in your responses.
      user:
      {{question}}
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = PromptAgent.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert (
        instance.instructions
        == """system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}"""
    )


def test_roundtrip_json_promptagent():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "prompt",
      "model": {
        "id": "gpt-35-turbo",
        "connection": {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "key": "{your-api-key}"
        }
      },
      "tools": [
        {
          "name": "getCurrentWeather",
          "kind": "function",
          "description": "Get the current weather in a given location",
          "parameters": {
            "location": {
              "kind": "string",
              "description": "The city and state, e.g. San Francisco, CA"
            },
            "unit": {
              "kind": "string",
              "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
            }
          }
        }
      ],
      "template": {
        "format": "mustache",
        "parser": "prompty"
      },
      "instructions": "system:\nYou are an AI assistant who helps people find information.\nAs the assistant, you answer questions briefly, succinctly,\nand in a personable manner using markdown and even add some \npersonal flair with appropriate emojis.\n\n# Customer\nYou are helping {{firstName}} {{lastName}} to find answers to \ntheir questions. Use their name to address them in your responses.\nuser:\n{{question}}"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = PromptAgent.load(original_data)
    saved_data = instance.save()
    reloaded = PromptAgent.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "prompt"
    assert (
        reloaded.instructions
        == """system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}"""
    )


def test_to_json_promptagent():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "prompt",
      "model": {
        "id": "gpt-35-turbo",
        "connection": {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "key": "{your-api-key}"
        }
      },
      "tools": [
        {
          "name": "getCurrentWeather",
          "kind": "function",
          "description": "Get the current weather in a given location",
          "parameters": {
            "location": {
              "kind": "string",
              "description": "The city and state, e.g. San Francisco, CA"
            },
            "unit": {
              "kind": "string",
              "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
            }
          }
        }
      ],
      "template": {
        "format": "mustache",
        "parser": "prompty"
      },
      "instructions": "system:\nYou are an AI assistant who helps people find information.\nAs the assistant, you answer questions briefly, succinctly,\nand in a personable manner using markdown and even add some \npersonal flair with appropriate emojis.\n\n# Customer\nYou are helping {{firstName}} {{lastName}} to find answers to \ntheir questions. Use their name to address them in your responses.\nuser:\n{{question}}"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = PromptAgent.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_promptagent():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "prompt",
      "model": {
        "id": "gpt-35-turbo",
        "connection": {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "key": "{your-api-key}"
        }
      },
      "tools": [
        {
          "name": "getCurrentWeather",
          "kind": "function",
          "description": "Get the current weather in a given location",
          "parameters": {
            "location": {
              "kind": "string",
              "description": "The city and state, e.g. San Francisco, CA"
            },
            "unit": {
              "kind": "string",
              "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
            }
          }
        }
      ],
      "template": {
        "format": "mustache",
        "parser": "prompty"
      },
      "instructions": "system:\nYou are an AI assistant who helps people find information.\nAs the assistant, you answer questions briefly, succinctly,\nand in a personable manner using markdown and even add some \npersonal flair with appropriate emojis.\n\n# Customer\nYou are helping {{firstName}} {{lastName}} to find answers to \ntheir questions. Use their name to address them in your responses.\nuser:\n{{question}}"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = PromptAgent.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)


def test_load_json_promptagent_1():
    json_data = """
    {
      "kind": "prompt",
      "model": {
        "id": "gpt-35-turbo",
        "connection": {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "key": "{your-api-key}"
        }
      },
      "tools": {
        "getCurrentWeather": {
          "kind": "function",
          "description": "Get the current weather in a given location",
          "parameters": {
            "location": {
              "kind": "string",
              "description": "The city and state, e.g. San Francisco, CA"
            },
            "unit": {
              "kind": "string",
              "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
            }
          }
        }
      },
      "template": {
        "format": "mustache",
        "parser": "prompty"
      },
      "instructions": "system:\nYou are an AI assistant who helps people find information.\nAs the assistant, you answer questions briefly, succinctly,\nand in a personable manner using markdown and even add some \npersonal flair with appropriate emojis.\n\n# Customer\nYou are helping {{firstName}} {{lastName}} to find answers to \ntheir questions. Use their name to address them in your responses.\nuser:\n{{question}}"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = PromptAgent.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert (
        instance.instructions
        == """system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}"""
    )


def test_load_yaml_promptagent_1():
    yaml_data = """
    kind: prompt
    model:
      id: gpt-35-turbo
      connection:
        kind: key
        endpoint: https://{your-custom-endpoint}.openai.azure.com/
        key: "{your-api-key}"
    tools:
      getCurrentWeather:
        kind: function
        description: Get the current weather in a given location
        parameters:
          location:
            kind: string
            description: The city and state, e.g. San Francisco, CA
          unit:
            kind: string
            description: The unit of temperature, e.g. Celsius or Fahrenheit
    template:
      format: mustache
      parser: prompty
    instructions: |-
      system:
      You are an AI assistant who helps people find information.
      As the assistant, you answer questions briefly, succinctly,
      and in a personable manner using markdown and even add some 
      personal flair with appropriate emojis.
    
      # Customer
      You are helping {{firstName}} {{lastName}} to find answers to 
      their questions. Use their name to address them in your responses.
      user:
      {{question}}
    
    """
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = PromptAgent.load(data)
    assert instance is not None
    assert instance.kind == "prompt"
    assert (
        instance.instructions
        == """system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}"""
    )


def test_roundtrip_json_promptagent_1():
    """Test that load -> save -> load produces equivalent data."""
    json_data = """
    {
      "kind": "prompt",
      "model": {
        "id": "gpt-35-turbo",
        "connection": {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "key": "{your-api-key}"
        }
      },
      "tools": {
        "getCurrentWeather": {
          "kind": "function",
          "description": "Get the current weather in a given location",
          "parameters": {
            "location": {
              "kind": "string",
              "description": "The city and state, e.g. San Francisco, CA"
            },
            "unit": {
              "kind": "string",
              "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
            }
          }
        }
      },
      "template": {
        "format": "mustache",
        "parser": "prompty"
      },
      "instructions": "system:\nYou are an AI assistant who helps people find information.\nAs the assistant, you answer questions briefly, succinctly,\nand in a personable manner using markdown and even add some \npersonal flair with appropriate emojis.\n\n# Customer\nYou are helping {{firstName}} {{lastName}} to find answers to \ntheir questions. Use their name to address them in your responses.\nuser:\n{{question}}"
    }
    """
    original_data = json.loads(json_data, strict=False)
    instance = PromptAgent.load(original_data)
    saved_data = instance.save()
    reloaded = PromptAgent.load(saved_data)
    assert reloaded is not None
    assert reloaded.kind == "prompt"
    assert (
        reloaded.instructions
        == """system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}"""
    )


def test_to_json_promptagent_1():
    """Test that to_json produces valid JSON."""
    json_data = """
    {
      "kind": "prompt",
      "model": {
        "id": "gpt-35-turbo",
        "connection": {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "key": "{your-api-key}"
        }
      },
      "tools": {
        "getCurrentWeather": {
          "kind": "function",
          "description": "Get the current weather in a given location",
          "parameters": {
            "location": {
              "kind": "string",
              "description": "The city and state, e.g. San Francisco, CA"
            },
            "unit": {
              "kind": "string",
              "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
            }
          }
        }
      },
      "template": {
        "format": "mustache",
        "parser": "prompty"
      },
      "instructions": "system:\nYou are an AI assistant who helps people find information.\nAs the assistant, you answer questions briefly, succinctly,\nand in a personable manner using markdown and even add some \npersonal flair with appropriate emojis.\n\n# Customer\nYou are helping {{firstName}} {{lastName}} to find answers to \ntheir questions. Use their name to address them in your responses.\nuser:\n{{question}}"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = PromptAgent.load(data)
    json_output = instance.to_json()
    assert json_output is not None
    parsed = json.loads(json_output)
    assert isinstance(parsed, dict)


def test_to_yaml_promptagent_1():
    """Test that to_yaml produces valid YAML."""
    json_data = """
    {
      "kind": "prompt",
      "model": {
        "id": "gpt-35-turbo",
        "connection": {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "key": "{your-api-key}"
        }
      },
      "tools": {
        "getCurrentWeather": {
          "kind": "function",
          "description": "Get the current weather in a given location",
          "parameters": {
            "location": {
              "kind": "string",
              "description": "The city and state, e.g. San Francisco, CA"
            },
            "unit": {
              "kind": "string",
              "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
            }
          }
        }
      },
      "template": {
        "format": "mustache",
        "parser": "prompty"
      },
      "instructions": "system:\nYou are an AI assistant who helps people find information.\nAs the assistant, you answer questions briefly, succinctly,\nand in a personable manner using markdown and even add some \npersonal flair with appropriate emojis.\n\n# Customer\nYou are helping {{firstName}} {{lastName}} to find answers to \ntheir questions. Use their name to address them in your responses.\nuser:\n{{question}}"
    }
    """
    data = json.loads(json_data, strict=False)
    instance = PromptAgent.load(data)
    yaml_output = instance.to_yaml()
    assert yaml_output is not None
    parsed = yaml.safe_load(yaml_output)
    assert isinstance(parsed, dict)
