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
"kind": "prompt"
"model":
  "id": "gpt-35-turbo"
  "connection":
    "kind": "key"
    "endpoint": "https://{your-custom-endpoint}.openai.azure.com/"
    "key": "{your-api-key}"
"tools":
  - "name": "getCurrentWeather"
    "kind": "function"
    "description": "Get the current weather in a given location"
    "parameters":
      "location":
        "kind": "string"
        "description": "The city and state, e.g. San Francisco, CA"
      "unit":
        "kind": "string"
        "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
"template":
  "format": "mustache"
  "parser": "prompty"
"instructions": "system:

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
{{question}}", instance.Instructions);
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
{{question}}", instance.Instructions);
    }
    [Fact]
    public void LoadYamlInput1()
    {
        string yamlData = """
"kind": "prompt"
"model":
  "id": "gpt-35-turbo"
  "connection":
    "kind": "key"
    "endpoint": "https://{your-custom-endpoint}.openai.azure.com/"
    "key": "{your-api-key}"
"tools":
  "getCurrentWeather":
    "kind": "function"
    "description": "Get the current weather in a given location"
    "parameters":
      "location":
        "kind": "string"
        "description": "The city and state, e.g. San Francisco, CA"
      "unit":
        "kind": "string"
        "description": "The unit of temperature, e.g. Celsius or Fahrenheit"
"template":
  "format": "mustache"
  "parser": "prompty"
"instructions": "system:

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
{{question}}", instance.Instructions);
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
{{question}}", instance.Instructions);
    }
}