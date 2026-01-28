using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class PropertySchemaConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        examples:
          - key: value
        strict: true
        properties:
          firstName:
            kind: string
            sample: Jane
          lastName:
            kind: string
            sample: Doe
          question:
            kind: string
            sample: What is the meaning of life?
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<PropertySchema>(yamlData);

        Assert.NotNull(instance);
        Assert.True(instance.Strict);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "examples": [
            {
              "key": "value"
            }
          ],
          "strict": true,
          "properties": {
            "firstName": {
              "kind": "string",
              "sample": "Jane"
            },
            "lastName": {
              "kind": "string",
              "sample": "Doe"
            },
            "question": {
              "kind": "string",
              "sample": "What is the meaning of life?"
            }
          }
        }
        """;

        var instance = JsonSerializer.Deserialize<PropertySchema>(jsonData);
        Assert.NotNull(instance);
        Assert.True(instance.Strict);
    }
}