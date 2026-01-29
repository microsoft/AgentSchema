using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class AgentDefinitionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "prompt"
"name": "basic-prompt"
"displayName": "Basic Prompt Agent"
"description": "A basic prompt that uses the GPT-3 chat API to answer questions"
"metadata":
  "authors":
    - "sethjuarez"
    - "jietong"
  "tags":
    - "example"
    - "prompt"
"inputSchema":
  "properties":
    "firstName":
      "kind": "string"
      "value": "Jane"
    "lastName":
      "kind": "string"
      "value": "Doe"
    "question":
      "kind": "string"
      "value": "What is the meaning of life?"
"outputSchema":
  "properties":
    "answer":
      "kind": "string"
      "description": "The answer to the user's question."

""";

        var instance = AgentDefinition.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("Basic Prompt Agent", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
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
""";

        var instance = AgentDefinition.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("Basic Prompt Agent", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }
    [Fact]
    public void LoadYamlInput1()
    {
        string yamlData = """
"kind": "prompt"
"name": "basic-prompt"
"displayName": "Basic Prompt Agent"
"description": "A basic prompt that uses the GPT-3 chat API to answer questions"
"metadata":
  "authors":
    - "sethjuarez"
    - "jietong"
  "tags":
    - "example"
    - "prompt"
"inputSchema":
  "properties":
    "firstName":
      "kind": "string"
      "value": "Jane"
    "lastName":
      "kind": "string"
      "value": "Doe"
    "question":
      "kind": "string"
      "value": "What is the meaning of life?"
"outputSchema":
  "properties":
    - "name": "answer"
      "kind": "string"
      "description": "The answer to the user's question."

""";

        var instance = AgentDefinition.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("Basic Prompt Agent", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }

    [Fact]
    public void LoadJsonInput1()
    {
        string jsonData = """
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
""";

        var instance = AgentDefinition.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("Basic Prompt Agent", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }
    [Fact]
    public void LoadYamlInput2()
    {
        string yamlData = """
"kind": "prompt"
"name": "basic-prompt"
"displayName": "Basic Prompt Agent"
"description": "A basic prompt that uses the GPT-3 chat API to answer questions"
"metadata":
  "authors":
    - "sethjuarez"
    - "jietong"
  "tags":
    - "example"
    - "prompt"
"inputSchema":
  "properties":
    - "name": "firstName"
      "kind": "string"
      "value": "Jane"
    - "name": "lastName"
      "kind": "string"
      "value": "Doe"
    - "name": "question"
      "kind": "string"
      "value": "What is the meaning of life?"
"outputSchema":
  "properties":
    "answer":
      "kind": "string"
      "description": "The answer to the user's question."

""";

        var instance = AgentDefinition.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("Basic Prompt Agent", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }

    [Fact]
    public void LoadJsonInput2()
    {
        string jsonData = """
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
""";

        var instance = AgentDefinition.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("Basic Prompt Agent", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }
    [Fact]
    public void LoadYamlInput3()
    {
        string yamlData = """
"kind": "prompt"
"name": "basic-prompt"
"displayName": "Basic Prompt Agent"
"description": "A basic prompt that uses the GPT-3 chat API to answer questions"
"metadata":
  "authors":
    - "sethjuarez"
    - "jietong"
  "tags":
    - "example"
    - "prompt"
"inputSchema":
  "properties":
    - "name": "firstName"
      "kind": "string"
      "value": "Jane"
    - "name": "lastName"
      "kind": "string"
      "value": "Doe"
    - "name": "question"
      "kind": "string"
      "value": "What is the meaning of life?"
"outputSchema":
  "properties":
    - "name": "answer"
      "kind": "string"
      "description": "The answer to the user's question."

""";

        var instance = AgentDefinition.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("Basic Prompt Agent", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }

    [Fact]
    public void LoadJsonInput3()
    {
        string jsonData = """
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
""";

        var instance = AgentDefinition.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("prompt", instance.Kind);
        Assert.Equal("basic-prompt", instance.Name);
        Assert.Equal("Basic Prompt Agent", instance.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", instance.Description);
    }
}