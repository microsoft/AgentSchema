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
kind: never

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
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "kind": "never"
}
""";

        var original = McpServerApprovalMode.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = McpServerApprovalMode.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("never", reloaded.Kind);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: never

""";

        var original = McpServerApprovalMode.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = McpServerApprovalMode.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("never", reloaded.Kind);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "kind": "never"
}
""";

        var instance = McpServerApprovalMode.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
kind: never

""";

        var instance = McpServerApprovalMode.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
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