using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class OpenApiToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: openapi
        connection:
          kind: reference
        specification: full_sepcification_here
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<OpenApiTool>(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("openapi", instance.Kind);
        Assert.Equal("full_sepcification_here", instance.Specification);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "kind": "openapi",
          "connection": {
            "kind": "reference"
          },
          "specification": "full_sepcification_here"
        }
        """;

        var instance = JsonSerializer.Deserialize<OpenApiTool>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("openapi", instance.Kind);
        Assert.Equal("full_sepcification_here", instance.Specification);
    }
}