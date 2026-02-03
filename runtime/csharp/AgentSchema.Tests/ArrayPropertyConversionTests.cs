using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
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

        var instance = ArrayProperty.FromYaml(yamlData);

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

        var instance = ArrayProperty.FromJson(jsonData);
        Assert.NotNull(instance);
    }
}