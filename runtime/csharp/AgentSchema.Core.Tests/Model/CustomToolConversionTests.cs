using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class CustomToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        connection:
          kind: reference
        options:
          timeout: 30
          retries: 3
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<CustomTool>(yamlData);

        Assert.NotNull(instance);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "connection": {
            "kind": "reference"
          },
          "options": {
            "timeout": 30,
            "retries": 3
          }
        }
        """;

        var instance = JsonSerializer.Deserialize<CustomTool>(jsonData);
        Assert.NotNull(instance);
    }
}