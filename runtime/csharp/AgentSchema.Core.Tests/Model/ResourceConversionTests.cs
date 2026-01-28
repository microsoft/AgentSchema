using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
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

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Resource>(yamlData);

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

        var instance = JsonSerializer.Deserialize<Resource>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("my-resource", instance.Name);
        Assert.Equal("model", instance.Kind);
    }
}