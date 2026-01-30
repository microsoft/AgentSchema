using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class McpServerApprovalModeConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "never"

""";

        var instance = McpServerApprovalMode.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "never"
}
""";

        var instance = McpServerApprovalMode.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }
    [Fact]
    public void LoadJsonFromString()
    {
        // alternate representation as string
        var data = "\"never\"";
        var instance = McpServerApprovalMode.FromJson(data);
        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }


    [Fact]
    public void LoadYamlFromString()
    {
        // alternate representation as string
        var data = "\"never\"";
        var instance = McpServerApprovalMode.FromYaml(data);
        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }
    
}