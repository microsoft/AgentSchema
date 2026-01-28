using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class WorkflowConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: workflow
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<Workflow>(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("workflow", instance.Kind);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "kind": "workflow"
        }
        """;

        var instance = JsonSerializer.Deserialize<Workflow>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("workflow", instance.Kind);
    }
}