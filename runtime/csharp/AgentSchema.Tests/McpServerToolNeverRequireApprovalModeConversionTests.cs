using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class McpServerToolNeverRequireApprovalModeConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "never"

""";

        var instance = McpServerToolNeverRequireApprovalMode.FromYaml(yamlData);

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

        var instance = McpServerToolNeverRequireApprovalMode.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }
}