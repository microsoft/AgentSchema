using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
name: my-tool
kind: function
description: A description of the tool
bindings:
  input: value

""";

        var instance = Tool.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("my-tool", instance.Name);
        Assert.Equal("function", instance.Kind);
        Assert.Equal("A description of the tool", instance.Description);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "name": "my-tool",
  "kind": "function",
  "description": "A description of the tool",
  "bindings": {
    "input": "value"
  }
}
""";

        var instance = Tool.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("my-tool", instance.Name);
        Assert.Equal("function", instance.Kind);
        Assert.Equal("A description of the tool", instance.Description);
    }
}