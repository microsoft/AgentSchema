using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class RemoteConnectionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "remote"
"name": "my-reference-connection"
"endpoint": "https://{your-custom-endpoint}.openai.azure.com/"

""";

        var instance = RemoteConnection.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("remote", instance.Kind);
        Assert.Equal("my-reference-connection", instance.Name);
        Assert.Equal("https://{your-custom-endpoint}.openai.azure.com/", instance.Endpoint);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "remote",
  "name": "my-reference-connection",
  "endpoint": "https://{your-custom-endpoint}.openai.azure.com/"
}
""";

        var instance = RemoteConnection.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("remote", instance.Kind);
        Assert.Equal("my-reference-connection", instance.Name);
        Assert.Equal("https://{your-custom-endpoint}.openai.azure.com/", instance.Endpoint);
    }
}