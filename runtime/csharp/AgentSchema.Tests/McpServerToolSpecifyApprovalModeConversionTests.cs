using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class McpServerToolSpecifyApprovalModeConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "specify"
"alwaysRequireApprovalTools":
  - "operation1"
"neverRequireApprovalTools":
  - "operation2"

""";

        var instance = McpServerToolSpecifyApprovalMode.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("specify", instance.Kind);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "specify",
  "alwaysRequireApprovalTools": [
    "operation1"
  ],
  "neverRequireApprovalTools": [
    "operation2"
  ]
}
""";

        var instance = McpServerToolSpecifyApprovalMode.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("specify", instance.Kind);
    }
}