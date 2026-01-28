using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class PropertyConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        name: my-input
        kind: string
        description: A description of the input property
        required: true
        default: default value
        example: example value
        enumValues:
          - value1
          - value2
          - value3
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Property>(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("my-input", instance.Name);
        Assert.Equal("string", instance.Kind);
        Assert.Equal("A description of the input property", instance.Description);
        Assert.True(instance.Required);
        Assert.Equal("default value", instance.Default);
        Assert.Equal("example value", instance.Example);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "name": "my-input",
          "kind": "string",
          "description": "A description of the input property",
          "required": true,
          "default": "default value",
          "example": "example value",
          "enumValues": [
            "value1",
            "value2",
            "value3"
          ]
        }
        """;

        var instance = JsonSerializer.Deserialize<Property>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("my-input", instance.Name);
        Assert.Equal("string", instance.Kind);
        Assert.Equal("A description of the input property", instance.Description);
        Assert.True(instance.Required);
        Assert.Equal("default value", instance.Default);
        Assert.Equal("example value", instance.Example);
    }
    [Fact]
    public void LoadJsonFromBoolean()
    {
        // alternate representation as boolean
        var data = false;
        var instance = JsonSerializer.Deserialize<Property>(data);
        Assert.NotNull(instance);
        Assert.Equal("boolean", instance.Kind);
        Assert.NotNull(instance.Example);
        Assert.IsType<bool>(instance.Example);
        Assert.False((bool)instance.Example);
    }


    [Fact]
    public void LoadYamlFromBoolean()
    {
        // alternate representation as boolean
        var data = false;
        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Property>(data.ToString());
        Assert.NotNull(instance);
        Assert.Equal("boolean", instance.Kind);
        Assert.NotNull(instance.Example);
        Assert.IsType<bool>(instance.Example);
        Assert.False((bool)instance.Example);
    }
    [Fact]
    public void LoadJsonFromFloat32()
    {
        // alternate representation as float32
        var data = 3.14;
        var instance = JsonSerializer.Deserialize<Property>(data);
        Assert.NotNull(instance);
        Assert.Equal("float", instance.Kind);
        Assert.IsType<float>(instance.Example);
        Assert.Equal(3.14, (float)instance.Example, precision: 5);
    }


    [Fact]
    public void LoadYamlFromFloat32()
    {
        // alternate representation as float32
        var data = 3.14;
        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Property>(data.ToString());
        Assert.NotNull(instance);
        Assert.Equal("float", instance.Kind);
        Assert.IsType<float>(instance.Example);
        Assert.Equal(3.14, (float)instance.Example, precision: 5);
    }
    [Fact]
    public void LoadJsonFromInteger()
    {
        // alternate representation as integer
        var data = 4;
        var instance = JsonSerializer.Deserialize<Property>(data);
        Assert.NotNull(instance);
        Assert.Equal("integer", instance.Kind);
        Assert.Equal(4, instance.Example);
    }


    [Fact]
    public void LoadYamlFromInteger()
    {
        // alternate representation as integer
        var data = 4;
        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Property>(data.ToString());
        Assert.NotNull(instance);
        Assert.Equal("integer", instance.Kind);
        Assert.Equal(4, instance.Example);
    }
    [Fact]
    public void LoadJsonFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var instance = JsonSerializer.Deserialize<Property>(data);
        Assert.NotNull(instance);
        Assert.Equal("string", instance.Kind);
        Assert.Equal("example", instance.Example);
    }


    [Fact]
    public void LoadYamlFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Property>(data);
        Assert.NotNull(instance);
        Assert.Equal("string", instance.Kind);
        Assert.Equal("example", instance.Example);
    }
    
}