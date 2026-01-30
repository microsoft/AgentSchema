using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class AnonymousConnectionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "anonymous"
"endpoint": "https://{your-custom-endpoint}.openai.azure.com/"

""";

        var instance = AnonymousConnection.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("anonymous", instance.Kind);
        Assert.Equal("https://{your-custom-endpoint}.openai.azure.com/", instance.Endpoint);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "anonymous",
  "endpoint": "https://{your-custom-endpoint}.openai.azure.com/"
}
""";

        var instance = AnonymousConnection.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("anonymous", instance.Kind);
        Assert.Equal("https://{your-custom-endpoint}.openai.azure.com/", instance.Endpoint);
    }
}