using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class ConnectionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "reference"
"authenticationMode": "system"
"usageDescription": "This will allow the agent to respond to an email on your behalf"

""";

        var instance = Connection.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("reference", instance.Kind);
        Assert.Equal("system", instance.AuthenticationMode);
        Assert.Equal("This will allow the agent to respond to an email on your behalf", instance.UsageDescription);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "reference",
  "authenticationMode": "system",
  "usageDescription": "This will allow the agent to respond to an email on your behalf"
}
""";

        var instance = Connection.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("reference", instance.Kind);
        Assert.Equal("system", instance.AuthenticationMode);
        Assert.Equal("This will allow the agent to respond to an email on your behalf", instance.UsageDescription);
    }
}