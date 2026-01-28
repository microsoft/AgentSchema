using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class AnonymousConnectionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: anonymous
        endpoint: https://{your-custom-endpoint}.openai.azure.com/
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<AnonymousConnection>(yamlData);

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

        var instance = JsonSerializer.Deserialize<AnonymousConnection>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("anonymous", instance.Kind);
        Assert.Equal("https://{your-custom-endpoint}.openai.azure.com/", instance.Endpoint);
    }
}