using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ResourceConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
name: my-resource
kind: model

""";

        var instance = Resource.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("my-resource", instance.Name);
        Assert.Equal("model", instance.Kind);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "name": "my-resource",
  "kind": "model"
}
""";

        var instance = Resource.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("my-resource", instance.Name);
        Assert.Equal("model", instance.Kind);
    }
}