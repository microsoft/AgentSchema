using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class McpServerToolSpecifyApprovalModeConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: specify
        alwaysRequireApprovalTools:
          - operation1
        neverRequireApprovalTools:
          - operation2
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<McpServerToolSpecifyApprovalMode>(yamlData);

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

        var instance = JsonSerializer.Deserialize<McpServerToolSpecifyApprovalMode>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("specify", instance.Kind);
    }
}