using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ReferenceConnectionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
kind: reference
name: my-reference-connection
target: my-target-resource

""";

        var instance = ReferenceConnection.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("reference", instance.Kind);
        Assert.Equal("my-reference-connection", instance.Name);
        Assert.Equal("my-target-resource", instance.Target);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "reference",
  "name": "my-reference-connection",
  "target": "my-target-resource"
}
""";

        var instance = ReferenceConnection.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("reference", instance.Kind);
        Assert.Equal("my-reference-connection", instance.Name);
        Assert.Equal("my-target-resource", instance.Target);
    }
}