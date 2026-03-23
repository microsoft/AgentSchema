
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
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
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

        var original = AgentManifest.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = AgentManifest.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("My Basic Prompt", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
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

""";

        var original = AgentManifest.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = AgentManifest.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("My Basic Prompt", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void ToJsonProducesValidJson()
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
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
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

""";

        var instance = AgentManifest.FromYaml(yamlData);
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

    [Fact]
    public void RoundtripJson1()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
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

        var original = AgentManifest.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = AgentManifest.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("My Basic Prompt", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void RoundtripYaml1()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
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

""";

        var original = AgentManifest.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = AgentManifest.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("basic-prompt", reloaded.Name);
        Assert.Equal("My Basic Prompt", reloaded.DisplayName);
        Assert.Equal("A basic prompt that uses the GPT-3 chat API to answer questions", reloaded.Description);
    }

    [Fact]
    public void ToJsonProducesValidJson1()
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
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml1()
    {
        string yamlData = """
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

""";

        var instance = AgentManifest.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
