using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class McpServerToolNeverRequireApprovalModeConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: never
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<McpServerToolNeverRequireApprovalMode>(yamlData);

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

        var instance = JsonSerializer.Deserialize<McpServerToolNeverRequireApprovalMode>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }
}