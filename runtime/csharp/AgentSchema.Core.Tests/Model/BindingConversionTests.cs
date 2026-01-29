using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class BindingConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"name": "my-tool"
"input": "input-variable"

""";

        var instance = Binding.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("my-tool", instance.Name);
        Assert.Equal("input-variable", instance.Input);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "name": "my-tool",
  "input": "input-variable"
}
""";

        var instance = Binding.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("my-tool", instance.Name);
        Assert.Equal("input-variable", instance.Input);
    }
    [Fact]
    public void LoadJsonFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var instance = Binding.FromJson(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Input);
    }


    [Fact]
    public void LoadYamlFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var instance = Binding.FromYaml(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Input);
    }
    
}