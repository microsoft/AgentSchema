using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
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

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<ToolResource>(yamlData);

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

        var instance = JsonSerializer.Deserialize<ToolResource>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("tool", instance.Kind);
        Assert.Equal("web-search", instance.Id);
    }
}