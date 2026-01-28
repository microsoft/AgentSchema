using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class ParserConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: prompty
        options:
          key: value
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Parser>(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("prompty", instance.Kind);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "kind": "prompty",
          "options": {
            "key": "value"
          }
        }
        """;

        var instance = JsonSerializer.Deserialize<Parser>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("prompty", instance.Kind);
    }
    [Fact]
    public void LoadJsonFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var instance = JsonSerializer.Deserialize<Parser>(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Kind);
    }


    [Fact]
    public void LoadYamlFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Parser>(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Kind);
    }
    
}