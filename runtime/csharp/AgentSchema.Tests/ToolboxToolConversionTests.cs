
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ToolboxToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
id: bing_grounding
name: my-search-tool
target: "https://api.githubcopilot.com/mcp"
authType: OAuth2
options:
  indexName: products-index

""";

        var instance = ToolboxTool.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("bing_grounding", instance.Id);
        Assert.Equal("my-search-tool", instance.Name);
        Assert.Equal("https://api.githubcopilot.com/mcp", instance.Target);
        Assert.Equal("OAuth2", instance.AuthType);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "id": "bing_grounding",
  "name": "my-search-tool",
  "target": "https://api.githubcopilot.com/mcp",
  "authType": "OAuth2",
  "options": {
    "indexName": "products-index"
  }
}
""";

        var instance = ToolboxTool.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("bing_grounding", instance.Id);
        Assert.Equal("my-search-tool", instance.Name);
        Assert.Equal("https://api.githubcopilot.com/mcp", instance.Target);
        Assert.Equal("OAuth2", instance.AuthType);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "id": "bing_grounding",
  "name": "my-search-tool",
  "target": "https://api.githubcopilot.com/mcp",
  "authType": "OAuth2",
  "options": {
    "indexName": "products-index"
  }
}
""";

        var original = ToolboxTool.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = ToolboxTool.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("bing_grounding", reloaded.Id);
        Assert.Equal("my-search-tool", reloaded.Name);
        Assert.Equal("https://api.githubcopilot.com/mcp", reloaded.Target);
        Assert.Equal("OAuth2", reloaded.AuthType);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
id: bing_grounding
name: my-search-tool
target: "https://api.githubcopilot.com/mcp"
authType: OAuth2
options:
  indexName: products-index

""";

        var original = ToolboxTool.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = ToolboxTool.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("bing_grounding", reloaded.Id);
        Assert.Equal("my-search-tool", reloaded.Name);
        Assert.Equal("https://api.githubcopilot.com/mcp", reloaded.Target);
        Assert.Equal("OAuth2", reloaded.AuthType);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "id": "bing_grounding",
  "name": "my-search-tool",
  "target": "https://api.githubcopilot.com/mcp",
  "authType": "OAuth2",
  "options": {
    "indexName": "products-index"
  }
}
""";

        var instance = ToolboxTool.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
id: bing_grounding
name: my-search-tool
target: "https://api.githubcopilot.com/mcp"
authType: OAuth2
options:
  indexName: products-index

""";

        var instance = ToolboxTool.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
