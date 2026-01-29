using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class CustomToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"connection":
  "kind": "reference"
"options":
  "timeout": 30
  "retries": 3

""";

        var instance = CustomTool.FromYaml(yamlData);

        Assert.NotNull(instance);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "connection": {
    "kind": "reference"
  },
  "options": {
    "timeout": 30,
    "retries": 3
  }
}
""";

        var instance = CustomTool.FromJson(jsonData);
        Assert.NotNull(instance);
    }
}