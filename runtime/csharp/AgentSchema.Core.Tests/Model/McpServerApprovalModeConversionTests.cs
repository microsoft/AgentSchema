using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class McpServerApprovalModeConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: never
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<McpServerApprovalMode>(yamlData);

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

        var instance = JsonSerializer.Deserialize<McpServerApprovalMode>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }
    [Fact]
    public void LoadJsonFromString()
    {
        // alternate representation as string
        var data = "\"never\"";
        var instance = JsonSerializer.Deserialize<McpServerApprovalMode>(data);
        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }


    [Fact]
    public void LoadYamlFromString()
    {
        // alternate representation as string
        var data = "\"never\"";
        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<McpServerApprovalMode>(data);
        Assert.NotNull(instance);
        Assert.Equal("never", instance.Kind);
    }
    
}