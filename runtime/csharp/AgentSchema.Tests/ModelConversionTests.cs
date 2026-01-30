using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ModelConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"id": "gpt-35-turbo"
"provider": "azure"
"apiType": "chat"
"connection":
  "kind": "key"
  "endpoint": "https://{your-custom-endpoint}.openai.azure.com/"
  "key": "{your-api-key}"
"options":
  "type": "chat"
  "temperature": 0.7
  "maxTokens": 1000

""";

        var instance = Model.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("gpt-35-turbo", instance.Id);
        Assert.Equal("azure", instance.Provider);
        Assert.Equal("chat", instance.ApiType);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "id": "gpt-35-turbo",
  "provider": "azure",
  "apiType": "chat",
  "connection": {
    "kind": "key",
    "endpoint": "https://{your-custom-endpoint}.openai.azure.com/",
    "key": "{your-api-key}"
  },
  "options": {
    "type": "chat",
    "temperature": 0.7,
    "maxTokens": 1000
  }
}
""";

        var instance = Model.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("gpt-35-turbo", instance.Id);
        Assert.Equal("azure", instance.Provider);
        Assert.Equal("chat", instance.ApiType);
    }
    [Fact]
    public void LoadJsonFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var instance = Model.FromJson(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Id);
    }


    [Fact]
    public void LoadYamlFromString()
    {
        // alternate representation as string
        var data = "\"example\"";
        var instance = Model.FromYaml(data);
        Assert.NotNull(instance);
        Assert.Equal("example", instance.Id);
    }
    
}