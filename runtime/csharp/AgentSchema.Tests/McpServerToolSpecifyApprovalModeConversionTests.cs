
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
kind: specify
alwaysRequireApprovalTools:
  - operation1
neverRequireApprovalTools:
  - operation2

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

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
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

        var original = McpServerToolSpecifyApprovalMode.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = McpServerToolSpecifyApprovalMode.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("specify", reloaded.Kind);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: specify
alwaysRequireApprovalTools:
  - operation1
neverRequireApprovalTools:
  - operation2

""";

        var original = McpServerToolSpecifyApprovalMode.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = McpServerToolSpecifyApprovalMode.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("specify", reloaded.Kind);
    }

    [Fact]
    public void ToJsonProducesValidJson()
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
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
kind: specify
alwaysRequireApprovalTools:
  - operation1
neverRequireApprovalTools:
  - operation2

""";

        var instance = McpServerToolSpecifyApprovalMode.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
