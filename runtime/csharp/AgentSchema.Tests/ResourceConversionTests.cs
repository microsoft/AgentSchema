using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ResourceConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
name: my-resource
kind: model

""";

        var instance = Resource.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("my-resource", instance.Name);
        Assert.Equal("model", instance.Kind);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "name": "my-resource",
  "kind": "model"
}
""";

        var instance = Resource.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("my-resource", instance.Name);
        Assert.Equal("model", instance.Kind);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "name": "my-resource",
  "kind": "model"
}
""";

        var original = Resource.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = Resource.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("my-resource", reloaded.Name);
        Assert.Equal("model", reloaded.Kind);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
name: my-resource
kind: model

""";

        var original = Resource.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = Resource.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("my-resource", reloaded.Name);
        Assert.Equal("model", reloaded.Kind);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "name": "my-resource",
  "kind": "model"
}
""";

        var instance = Resource.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
name: my-resource
kind: model

""";

        var instance = Resource.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}