
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ModelResourceConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
kind: model
id: gpt-4o

""";

        var instance = ModelResource.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("model", instance.Kind);
        Assert.Equal("gpt-4o", instance.Id);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "model",
  "id": "gpt-4o"
}
""";

        var instance = ModelResource.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("model", instance.Kind);
        Assert.Equal("gpt-4o", instance.Id);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "kind": "model",
  "id": "gpt-4o"
}
""";

        var original = ModelResource.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = ModelResource.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("model", reloaded.Kind);
        Assert.Equal("gpt-4o", reloaded.Id);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: model
id: gpt-4o

""";

        var original = ModelResource.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = ModelResource.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("model", reloaded.Kind);
        Assert.Equal("gpt-4o", reloaded.Id);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "kind": "model",
  "id": "gpt-4o"
}
""";

        var instance = ModelResource.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
kind: model
id: gpt-4o

""";

        var instance = ModelResource.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
