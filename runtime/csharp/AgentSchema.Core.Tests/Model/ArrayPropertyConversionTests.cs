using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class ArrayPropertyConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        items:
          kind: string
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<ArrayProperty>(yamlData);

        Assert.NotNull(instance);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "items": {
            "kind": "string"
          }
        }
        """;

        var instance = JsonSerializer.Deserialize<ArrayProperty>(jsonData);
        Assert.NotNull(instance);
    }
}