using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
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

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<ModelResource>(yamlData);

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

        var instance = JsonSerializer.Deserialize<ModelResource>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("model", instance.Kind);
        Assert.Equal("gpt-4o", instance.Id);
    }
}