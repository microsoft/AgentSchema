using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ToolResourceConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
kind: tool
id: web-search
options:
  myToolResourceProperty: myValue

""";

        var instance = ToolResource.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("tool", instance.Kind);
        Assert.Equal("web-search", instance.Id);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "tool",
  "id": "web-search",
  "options": {
    "myToolResourceProperty": "myValue"
  }
}
""";

        var instance = ToolResource.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("tool", instance.Kind);
        Assert.Equal("web-search", instance.Id);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "kind": "tool",
  "id": "web-search",
  "options": {
    "myToolResourceProperty": "myValue"
  }
}
""";

        var original = ToolResource.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = ToolResource.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("tool", reloaded.Kind);
        Assert.Equal("web-search", reloaded.Id);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: tool
id: web-search
options:
  myToolResourceProperty: myValue

""";

        var original = ToolResource.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = ToolResource.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("tool", reloaded.Kind);
        Assert.Equal("web-search", reloaded.Id);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "kind": "tool",
  "id": "web-search",
  "options": {
    "myToolResourceProperty": "myValue"
  }
}
""";

        var instance = ToolResource.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
kind: tool
id: web-search
options:
  myToolResourceProperty: myValue

""";

        var instance = ToolResource.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}