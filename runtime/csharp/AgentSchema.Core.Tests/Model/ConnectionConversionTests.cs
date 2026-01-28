using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class ConnectionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: reference
        authenticationMode: system
        usageDescription: This will allow the agent to respond to an email on your behalf
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Connection>(yamlData);

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

        var instance = JsonSerializer.Deserialize<Connection>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("reference", instance.Kind);
        Assert.Equal("system", instance.AuthenticationMode);
        Assert.Equal("This will allow the agent to respond to an email on your behalf", instance.UsageDescription);
    }
}