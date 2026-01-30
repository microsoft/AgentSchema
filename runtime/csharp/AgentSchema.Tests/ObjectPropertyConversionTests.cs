using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ObjectPropertyConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"properties":
  "property1":
    "kind": "string"
  "property2":
    "kind": "number"

""";

        var instance = ObjectProperty.FromYaml(yamlData);

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

        var instance = ObjectProperty.FromJson(jsonData);
        Assert.NotNull(instance);
    }
}