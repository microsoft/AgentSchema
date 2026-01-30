using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class WorkflowConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "workflow"

""";

        var instance = Workflow.FromYaml(yamlData);

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

        var instance = Workflow.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("workflow", instance.Kind);
    }
}