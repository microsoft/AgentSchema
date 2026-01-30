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
"protocol": "responses"
"version": "v0.1.1"

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
}