using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ProtocolVersionRecordConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
protocol: responses
version: v0.1.1

""";

        var instance = ProtocolVersionRecord.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("responses", instance.Protocol);
        Assert.Equal("v0.1.1", instance.Version);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "protocol": "responses",
  "version": "v0.1.1"
}
""";

        var instance = ProtocolVersionRecord.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("responses", instance.Protocol);
        Assert.Equal("v0.1.1", instance.Version);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "protocol": "responses",
  "version": "v0.1.1"
}
""";

        var original = ProtocolVersionRecord.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = ProtocolVersionRecord.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("responses", reloaded.Protocol);
        Assert.Equal("v0.1.1", reloaded.Version);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
protocol: responses
version: v0.1.1

""";

        var original = ProtocolVersionRecord.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = ProtocolVersionRecord.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("responses", reloaded.Protocol);
        Assert.Equal("v0.1.1", reloaded.Version);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "protocol": "responses",
  "version": "v0.1.1"
}
""";

        var instance = ProtocolVersionRecord.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
protocol: responses
version: v0.1.1

""";

        var instance = ProtocolVersionRecord.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}