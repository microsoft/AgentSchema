using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class TemplateConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"format":
  "kind": "mustache"
"parser":
  "kind": "mustache"

""";

        var instance = Template.FromYaml(yamlData);

        Assert.NotNull(instance);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "format": {
    "kind": "mustache"
  },
  "parser": {
    "kind": "mustache"
  }
}
""";

        var instance = Template.FromJson(jsonData);
        Assert.NotNull(instance);
    }
}