using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class FormatConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: mustache
        strict: true
        options:
          key: value
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Format>(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("mustache", instance.Kind);
        Assert.True(instance.Strict);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "kind": "mustache",
          "strict": true,
          "options": {
            "key": "value"
          }
        }
        """;

        var instance = JsonSerializer.Deserialize<Format>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("mustache", instance.Kind);
        Assert.True(instance.Strict);
    }
    [Fact]
    public void LoadJsonFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var instance = JsonSerializer.Deserialize<Format>(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Kind);
    }


    [Fact]
    public void LoadYamlFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Format>(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Kind);
    }
    
}