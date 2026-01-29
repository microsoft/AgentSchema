using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class McpServerToolAlwaysRequireApprovalModeConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "always"

""";

        var instance = McpServerToolAlwaysRequireApprovalMode.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("always", instance.Kind);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "always"
}
""";

        var instance = McpServerToolAlwaysRequireApprovalMode.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("always", instance.Kind);
    }
}