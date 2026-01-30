using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class AgentManifestConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"name": "basic-prompt"
"displayName": "My Basic Prompt"
"description": "A basic prompt that uses the GPT-3 chat API to answer questions"
"metadata":
  "authors":
    - "sethjuarez"
    - "jietong"
  "tags":
    - "example"
    - "prompt"
"template":
  "kind": "prompt"
  "model": "{{model_name}}"
  "instructions": "You are a poet named {{agent_name}}. Rhyme all your responses."
"parameters":
  "strict": true
  "properties":
    - "name": "model_name"
      "kind": "string"
      "value": "gpt-4o"
    - "name": "agent_name"
      "kind": "string"
      "value": "Research Agent"
"resources":
  "gptModelDeployment":
    "kind": "model"
    "id": "gpt-4o"
  "webSearchInstance":
    "kind": "tool"
    "id": "web-search"
    "options":
      "apiKey": "my-api-key"

""";

        var instance = AgentManifest.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("My Basic Prompt", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
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
""";

        var instance = AgentManifest.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("My Basic Prompt", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }
    [Fact]
    public void LoadYamlInput1()
    {
        string yamlData = """
"name": "basic-prompt"
"displayName": "My Basic Prompt"
"description": "A basic prompt that uses the GPT-3 chat API to answer questions"
"metadata":
  "authors":
    - "sethjuarez"
    - "jietong"
  "tags":
    - "example"
    - "prompt"
"template":
  "kind": "prompt"
  "model": "{{model_name}}"
  "instructions": "You are a poet named {{agent_name}}. Rhyme all your responses."
"parameters":
  "strict": true
  "properties":
    - "name": "model_name"
      "kind": "string"
      "value": "gpt-4o"
    - "name": "agent_name"
      "kind": "string"
      "value": "Research Agent"
"resources":
  - "kind": "model"
    "name": "gptModelDeployment"
    "id": "gpt-4o"
  - "kind": "tool"
    "name": "webSearchInstance"
    "id": "web-search"
    "options":
      "apiKey": "my-api-key"

""";

        var instance = AgentManifest.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("My Basic Prompt", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }

    [Fact]
    public void LoadJsonInput1()
    {
        string jsonData = """
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
""";

        var instance = AgentManifest.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("My Basic Prompt", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }
}