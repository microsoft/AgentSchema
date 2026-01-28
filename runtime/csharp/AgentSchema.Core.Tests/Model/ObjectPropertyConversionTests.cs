using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class ObjectPropertyConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        properties:
          property1:
            kind: string
          property2:
            kind: number
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<ObjectProperty>(yamlData);

        Assert.NotNull(instance);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "properties": {
            "property1": {
              "kind": "string"
            },
            "property2": {
              "kind": "number"
            }
          }
        }
        """;

        var instance = JsonSerializer.Deserialize<ObjectProperty>(jsonData);
        Assert.NotNull(instance);
    }
}