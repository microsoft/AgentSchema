
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class McpServerToolAlwaysRequireApprovalModeConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
kind: always

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

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "kind": "always"
}
""";

        var original = McpServerToolAlwaysRequireApprovalMode.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = McpServerToolAlwaysRequireApprovalMode.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("always", reloaded.Kind);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: always

""";

        var original = McpServerToolAlwaysRequireApprovalMode.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = McpServerToolAlwaysRequireApprovalMode.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("always", reloaded.Kind);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "kind": "always"
}
""";

        var instance = McpServerToolAlwaysRequireApprovalMode.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
kind: always

""";

        var instance = McpServerToolAlwaysRequireApprovalMode.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
