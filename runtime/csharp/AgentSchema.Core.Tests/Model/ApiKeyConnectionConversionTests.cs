using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class ApiKeyConnectionConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: key
        endpoint: https://{your-custom-endpoint}.openai.azure.com/
        apiKey: your-api-key
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<ApiKeyConnection>(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("key", instance.Kind);
        Assert.Equal("https://{your-custom-endpoint}.openai.azure.com/", instance.Endpoint);
        Assert.Equal("your-api-key", instance.ApiKey);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "kind": "key",
          "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
          "apiKey": "your-api-key"
        }
        """;

        var instance = JsonSerializer.Deserialize<ApiKeyConnection>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("key", instance.Kind);
        Assert.Equal("https://{your-custom-endpoint}.openai.azure.com/", instance.Endpoint);
        Assert.Equal("your-api-key", instance.ApiKey);
    }
}