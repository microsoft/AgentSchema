using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class EnvironmentVariableConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"name": "MY_ENV_VAR"
"value": "my-value"

""";

        var instance = EnvironmentVariable.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("MY_ENV_VAR", instance.Name);
        Assert.Equal("my-value", instance.Value);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "name": "MY_ENV_VAR",
  "value": "my-value"
}
""";

        var instance = EnvironmentVariable.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("MY_ENV_VAR", instance.Name);
        Assert.Equal("my-value", instance.Value);
    }
}