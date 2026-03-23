
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class PromptAgentConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
kind: prompt
model:
  id: gpt-35-turbo
  connection:
    kind: key
    endpoint: "https://{your-custom-endpoint}.openai.azure.com/"
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
instructions: "system:

  You are an AI assistant who helps people find information.

  As the assistant, you answer questions briefly, succinctly,

  and in a personable manner using markdown and even add some\ 

  personal flair with appropriate emojis.


  # Customer

  You are helping {{firstName}} {{lastName}} to find answers to\ 

  their questions. Use their name to address them in your responses.

  user:

  {{question}}"

""";

        var instance = PromptAgent.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal(@"system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}".Replace("\r\n", "\n"), instance.Instructions);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
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
""";

        var instance = PromptAgent.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal(@"system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}".Replace("\r\n", "\n"), instance.Instructions);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
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
""";

        var original = PromptAgent.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = PromptAgent.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal(@"system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}".Replace("\r\n", "\n"), reloaded.Instructions);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: prompt
model:
  id: gpt-35-turbo
  connection:
    kind: key
    endpoint: "https://{your-custom-endpoint}.openai.azure.com/"
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
instructions: "system:

  You are an AI assistant who helps people find information.

  As the assistant, you answer questions briefly, succinctly,

  and in a personable manner using markdown and even add some\ 

  personal flair with appropriate emojis.


  # Customer

  You are helping {{firstName}} {{lastName}} to find answers to\ 

  their questions. Use their name to address them in your responses.

  user:

  {{question}}"

""";

        var original = PromptAgent.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = PromptAgent.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal(@"system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}".Replace("\r\n", "\n"), reloaded.Instructions);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
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
""";

        var instance = PromptAgent.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
kind: prompt
model:
  id: gpt-35-turbo
  connection:
    kind: key
    endpoint: "https://{your-custom-endpoint}.openai.azure.com/"
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
instructions: "system:

  You are an AI assistant who helps people find information.

  As the assistant, you answer questions briefly, succinctly,

  and in a personable manner using markdown and even add some\ 

  personal flair with appropriate emojis.


  # Customer

  You are helping {{firstName}} {{lastName}} to find answers to\ 

  their questions. Use their name to address them in your responses.

  user:

  {{question}}"

""";

        var instance = PromptAgent.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
    [Fact]
    public void LoadYamlInput1()
    {
        string yamlData = """
kind: prompt
model:
  id: gpt-35-turbo
  connection:
    kind: key
    endpoint: "https://{your-custom-endpoint}.openai.azure.com/"
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
instructions: "system:

  You are an AI assistant who helps people find information.

  As the assistant, you answer questions briefly, succinctly,

  and in a personable manner using markdown and even add some\ 

  personal flair with appropriate emojis.


  # Customer

  You are helping {{firstName}} {{lastName}} to find answers to\ 

  their questions. Use their name to address them in your responses.

  user:

  {{question}}"

""";

        var instance = PromptAgent.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal(@"system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}".Replace("\r\n", "\n"), instance.Instructions);
    }

    [Fact]
    public void LoadJsonInput1()
    {
        string jsonData = """
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
""";

        var instance = PromptAgent.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal(@"system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}".Replace("\r\n", "\n"), instance.Instructions);
    }

    [Fact]
    public void RoundtripJson1()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
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
""";

        var original = PromptAgent.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = PromptAgent.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal(@"system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}".Replace("\r\n", "\n"), reloaded.Instructions);
    }

    [Fact]
    public void RoundtripYaml1()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: prompt
model:
  id: gpt-35-turbo
  connection:
    kind: key
    endpoint: "https://{your-custom-endpoint}.openai.azure.com/"
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
instructions: "system:

  You are an AI assistant who helps people find information.

  As the assistant, you answer questions briefly, succinctly,

  and in a personable manner using markdown and even add some\ 

  personal flair with appropriate emojis.


  # Customer

  You are helping {{firstName}} {{lastName}} to find answers to\ 

  their questions. Use their name to address them in your responses.

  user:

  {{question}}"

""";

        var original = PromptAgent.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = PromptAgent.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal(@"system:
You are an AI assistant who helps people find information.
As the assistant, you answer questions briefly, succinctly,
and in a personable manner using markdown and even add some 
personal flair with appropriate emojis.

# Customer
You are helping {{firstName}} {{lastName}} to find answers to 
their questions. Use their name to address them in your responses.
user:
{{question}}".Replace("\r\n", "\n"), reloaded.Instructions);
    }

    [Fact]
    public void ToJsonProducesValidJson1()
    {
        string jsonData = """
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
""";

        var instance = PromptAgent.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml1()
    {
        string yamlData = """
kind: prompt
model:
  id: gpt-35-turbo
  connection:
    kind: key
    endpoint: "https://{your-custom-endpoint}.openai.azure.com/"
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
instructions: "system:

  You are an AI assistant who helps people find information.

  As the assistant, you answer questions briefly, succinctly,

  and in a personable manner using markdown and even add some\ 

  personal flair with appropriate emojis.


  # Customer

  You are helping {{firstName}} {{lastName}} to find answers to\ 

  their questions. Use their name to address them in your responses.

  user:

  {{question}}"

""";

        var instance = PromptAgent.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
