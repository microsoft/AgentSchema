
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ContainerResourcesConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
cpu: "1"
memory: 2Gi

""";

        var instance = ContainerResources.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("1", instance.Cpu);
        Assert.Equal("2Gi", instance.Memory);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "cpu": "1",
  "memory": "2Gi"
}
""";

        var instance = ContainerResources.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("1", instance.Cpu);
        Assert.Equal("2Gi", instance.Memory);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "cpu": "1",
  "memory": "2Gi"
}
""";

        var original = ContainerResources.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = ContainerResources.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("1", reloaded.Cpu);
        Assert.Equal("2Gi", reloaded.Memory);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
cpu: "1"
memory: 2Gi

""";

        var original = ContainerResources.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = ContainerResources.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("1", reloaded.Cpu);
        Assert.Equal("2Gi", reloaded.Memory);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "cpu": "1",
  "memory": "2Gi"
}
""";

        var instance = ContainerResources.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
cpu: "1"
memory: 2Gi

""";

        var instance = ContainerResources.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
