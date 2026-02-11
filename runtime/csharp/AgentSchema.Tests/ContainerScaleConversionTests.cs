
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ContainerScaleConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
minReplicas: 1
maxReplicas: 3

""";

        var instance = ContainerScale.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal(1, instance.MinReplicas);
        Assert.Equal(3, instance.MaxReplicas);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "minReplicas": 1,
  "maxReplicas": 3
}
""";

        var instance = ContainerScale.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal(1, instance.MinReplicas);
        Assert.Equal(3, instance.MaxReplicas);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "minReplicas": 1,
  "maxReplicas": 3
}
""";

        var original = ContainerScale.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = ContainerScale.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal(1, reloaded.MinReplicas);
        Assert.Equal(3, reloaded.MaxReplicas);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
minReplicas: 1
maxReplicas: 3

""";

        var original = ContainerScale.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = ContainerScale.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal(1, reloaded.MinReplicas);
        Assert.Equal(3, reloaded.MaxReplicas);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "minReplicas": 1,
  "maxReplicas": 3
}
""";

        var instance = ContainerScale.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
minReplicas: 1
maxReplicas: 3

""";

        var instance = ContainerScale.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
