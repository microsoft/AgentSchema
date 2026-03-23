
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class EnvironmentVariableConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
name: MY_ENV_VAR
value: my-value

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

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "name": "MY_ENV_VAR",
  "value": "my-value"
}
""";

        var original = EnvironmentVariable.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = EnvironmentVariable.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("MY_ENV_VAR", reloaded.Name);
        Assert.Equal("my-value", reloaded.Value);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
name: MY_ENV_VAR
value: my-value

""";

        var original = EnvironmentVariable.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = EnvironmentVariable.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("MY_ENV_VAR", reloaded.Name);
        Assert.Equal("my-value", reloaded.Value);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "name": "MY_ENV_VAR",
  "value": "my-value"
}
""";

        var instance = EnvironmentVariable.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
name: MY_ENV_VAR
value: my-value

""";

        var instance = EnvironmentVariable.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
