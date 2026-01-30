using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class OpenApiToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "openapi"
"connection":
  "kind": "reference"
"specification": "full_sepcification_here"

""";

        var instance = OpenApiTool.FromYaml(yamlData);

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

        var instance = OpenApiTool.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("openapi", instance.Kind);
        Assert.Equal("full_sepcification_here", instance.Specification);
    }
}