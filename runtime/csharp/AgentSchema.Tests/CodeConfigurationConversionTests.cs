
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class CodeConfigurationConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
runtime: python_3_11
entryPoint: main.py
dependencyResolution: remote_build

""";

        var instance = CodeConfiguration.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("python_3_11", instance.Runtime);
        Assert.Equal("main.py", instance.EntryPoint);
        Assert.Equal("remote_build", instance.DependencyResolution);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "runtime": "python_3_11",
  "entryPoint": "main.py",
  "dependencyResolution": "remote_build"
}
""";

        var instance = CodeConfiguration.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("python_3_11", instance.Runtime);
        Assert.Equal("main.py", instance.EntryPoint);
        Assert.Equal("remote_build", instance.DependencyResolution);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "runtime": "python_3_11",
  "entryPoint": "main.py",
  "dependencyResolution": "remote_build"
}
""";

        var original = CodeConfiguration.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = CodeConfiguration.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("python_3_11", reloaded.Runtime);
        Assert.Equal("main.py", reloaded.EntryPoint);
        Assert.Equal("remote_build", reloaded.DependencyResolution);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
runtime: python_3_11
entryPoint: main.py
dependencyResolution: remote_build

""";

        var original = CodeConfiguration.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = CodeConfiguration.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("python_3_11", reloaded.Runtime);
        Assert.Equal("main.py", reloaded.EntryPoint);
        Assert.Equal("remote_build", reloaded.DependencyResolution);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "runtime": "python_3_11",
  "entryPoint": "main.py",
  "dependencyResolution": "remote_build"
}
""";

        var instance = CodeConfiguration.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
runtime: python_3_11
entryPoint: main.py
dependencyResolution: remote_build

""";

        var instance = CodeConfiguration.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
