using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class ModelResourceConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "model"
"id": "gpt-4o"

""";

        var instance = ModelResource.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("model", instance.Kind);
        Assert.Equal("gpt-4o", instance.Id);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "model",
  "id": "gpt-4o"
}
""";

        var instance = ModelResource.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("model", instance.Kind);
        Assert.Equal("gpt-4o", instance.Id);
    }
}