using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class BindingConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        name: my-tool
        input: input-variable
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Binding>(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("my-tool", instance.Name);
        Assert.Equal("input-variable", instance.Input);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "name": "my-tool",
          "input": "input-variable"
        }
        """;

        var instance = JsonSerializer.Deserialize<Binding>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("my-tool", instance.Name);
        Assert.Equal("input-variable", instance.Input);
    }
    [Fact]
    public void LoadJsonFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var instance = JsonSerializer.Deserialize<Binding>(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Input);
    }


    [Fact]
    public void LoadYamlFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Binding>(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Input);
    }
    
}