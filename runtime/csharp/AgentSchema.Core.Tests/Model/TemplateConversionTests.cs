using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class TemplateConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        format:
          kind: mustache
        parser:
          kind: mustache
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Template>(yamlData);

        Assert.NotNull(instance);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "format": {
            "kind": "mustache"
          },
          "parser": {
            "kind": "mustache"
          }
        }
        """;

        var instance = JsonSerializer.Deserialize<Template>(jsonData);
        Assert.NotNull(instance);
    }
}