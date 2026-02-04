using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class AgentDefinitionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
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
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
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

        var original = AgentDefinition.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = AgentDefinition.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("Basic Prompt Agent", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
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

""";

        var original = AgentDefinition.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = AgentDefinition.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("Basic Prompt Agent", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void ToJsonProducesValidJson()
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

""";

        var instance = AgentDefinition.FromYaml(yamlData);
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
    public void RoundtripJson1()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
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

        var original = AgentDefinition.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = AgentDefinition.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("Basic Prompt Agent", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void RoundtripYaml1()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
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

""";

        var original = AgentDefinition.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = AgentDefinition.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("Basic Prompt Agent", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void ToJsonProducesValidJson1()
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

""";

        var instance = AgentDefinition.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
    [Fact]
    public void LoadYamlInput2()
    {
        string yamlData = """
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
    public void RoundtripJson2()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
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

        var original = AgentDefinition.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = AgentDefinition.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("Basic Prompt Agent", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void RoundtripYaml2()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
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

""";

        var original = AgentDefinition.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = AgentDefinition.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("Basic Prompt Agent", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void ToJsonProducesValidJson2()
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
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml2()
    {
        string yamlData = """
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

""";

        var instance = AgentDefinition.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
    [Fact]
    public void LoadYamlInput3()
    {
        string yamlData = """
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

    [Fact]
    public void RoundtripJson3()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
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

        var original = AgentDefinition.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = AgentDefinition.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("Basic Prompt Agent", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void RoundtripYaml3()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
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

""";

        var original = AgentDefinition.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = AgentDefinition.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("prompt", reloaded.Kind);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("Basic Prompt Agent", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void ToJsonProducesValidJson3()
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
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml3()
    {
        string yamlData = """
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

""";

        var instance = AgentDefinition.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}