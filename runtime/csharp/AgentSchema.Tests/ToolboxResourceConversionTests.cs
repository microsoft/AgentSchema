
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ToolboxResourceConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
kind: toolbox
description: Shared platform tools
tools:
  - id: bing_grounding
  - id: azure_ai_search
    options:
      indexName: products-index
  - id: mcp
    name: github-copilot
    target: "https://api.githubcopilot.com/mcp"
    authType: OAuth2

""";

        var instance = ToolboxResource.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("toolbox", instance.Kind);
        Assert.Equal("Shared platform tools", instance.Description);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "toolbox",
  "description": "Shared platform tools",
  "tools": [
    {
      "id": "bing_grounding"
    },
    {
      "id": "azure_ai_search",
      "options": {
        "indexName": "products-index"
      }
    },
    {
      "id": "mcp",
      "name": "github-copilot",
      "target": "https://api.githubcopilot.com/mcp",
      "authType": "OAuth2"
    }
  ]
}
""";

        var instance = ToolboxResource.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("toolbox", instance.Kind);
        Assert.Equal("Shared platform tools", instance.Description);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "kind": "toolbox",
  "description": "Shared platform tools",
  "tools": [
    {
      "id": "bing_grounding"
    },
    {
      "id": "azure_ai_search",
      "options": {
        "indexName": "products-index"
      }
    },
    {
      "id": "mcp",
      "name": "github-copilot",
      "target": "https://api.githubcopilot.com/mcp",
      "authType": "OAuth2"
    }
  ]
}
""";

        var original = ToolboxResource.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = ToolboxResource.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("toolbox", reloaded.Kind);
        Assert.Equal("Shared platform tools", reloaded.Description);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: toolbox
description: Shared platform tools
tools:
  - id: bing_grounding
  - id: azure_ai_search
    options:
      indexName: products-index
  - id: mcp
    name: github-copilot
    target: "https://api.githubcopilot.com/mcp"
    authType: OAuth2

""";

        var original = ToolboxResource.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = ToolboxResource.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("toolbox", reloaded.Kind);
        Assert.Equal("Shared platform tools", reloaded.Description);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "kind": "toolbox",
  "description": "Shared platform tools",
  "tools": [
    {
      "id": "bing_grounding"
    },
    {
      "id": "azure_ai_search",
      "options": {
        "indexName": "products-index"
      }
    },
    {
      "id": "mcp",
      "name": "github-copilot",
      "target": "https://api.githubcopilot.com/mcp",
      "authType": "OAuth2"
    }
  ]
}
""";

        var instance = ToolboxResource.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
kind: toolbox
description: Shared platform tools
tools:
  - id: bing_grounding
  - id: azure_ai_search
    options:
      indexName: products-index
  - id: mcp
    name: github-copilot
    target: "https://api.githubcopilot.com/mcp"
    authType: OAuth2

""";

        var instance = ToolboxResource.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
