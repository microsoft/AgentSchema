using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ContainerAgentConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
kind: hosted
protocols:
  - protocol: responses
    version: v0.1.1
environmentVariables:
  - name: MY_ENV_VAR
    value: my-value

""";

        var instance = ContainerAgent.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("hosted", instance.Kind);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "hosted",
  "protocols": [
    {
      "protocol": "responses",
      "version": "v0.1.1"
    }
  ],
  "environmentVariables": [
    {
      "name": "MY_ENV_VAR",
      "value": "my-value"
    }
  ]
}
""";

        var instance = ContainerAgent.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("hosted", instance.Kind);
    }
}