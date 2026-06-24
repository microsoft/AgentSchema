// Copyright (c) Microsoft. All rights reserved.
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130

/// <summary>
/// Regression tests verifying that tools are correctly deserialized from YAML.
/// YamlDotNet 16.x deserializes nested YAML mappings as Dictionary&lt;object, object&gt;
/// rather than Dictionary&lt;string, object?&gt;, which previously caused the Tools
/// collection to always be empty after loading from YAML.
/// </summary>
public class PromptAgentToolsYamlTests
{
    [Fact]
    public void LoadYaml_ListFormat_ToolsAreDeserializedCorrectly()
    {
        string yaml = """
kind: prompt
name: my-agent
model: gpt-4o
template: Hello
tools:
  - name: search
    kind: function
    description: Search the web
""";

        var agent = AgentDefinition.FromYaml(yaml) as PromptAgent;

        Assert.NotNull(agent);
        Assert.NotNull(agent.Tools);
        Assert.NotEmpty(agent.Tools);
        Assert.Single(agent.Tools);
        Assert.Equal("search", agent.Tools[0].Name);
        Assert.Equal("function", agent.Tools[0].Kind);
        Assert.Equal("Search the web", agent.Tools[0].Description);
    }

    [Fact]
    public void LoadYaml_DictionaryFormat_ToolsAreDeserializedCorrectly()
    {
        string yaml = """
kind: prompt
name: my-agent
model: gpt-4o
template: Hello
tools:
  search:
    kind: function
    description: Search the web
  calculate:
    kind: function
    description: Run a calculation
""";

        var agent = AgentDefinition.FromYaml(yaml) as PromptAgent;

        Assert.NotNull(agent);
        Assert.NotNull(agent.Tools);
        Assert.NotEmpty(agent.Tools);
        Assert.Equal(2, agent.Tools.Count);

        var searchTool = agent.Tools.FirstOrDefault(t => t.Name == "search");
        Assert.NotNull(searchTool);
        Assert.Equal("function", searchTool.Kind);
        Assert.Equal("Search the web", searchTool.Description);

        var calcTool = agent.Tools.FirstOrDefault(t => t.Name == "calculate");
        Assert.NotNull(calcTool);
        Assert.Equal("function", calcTool.Kind);
        Assert.Equal("Run a calculation", calcTool.Description);
    }

    [Fact]
    public void LoadYaml_ListFormat_MultipleTool_CountIsCorrect()
    {
        string yaml = """
kind: prompt
name: my-agent
model: gpt-4o
template: Hello
tools:
  - name: search
    kind: function
    description: Search the web
  - name: calculate
    kind: function
    description: Run a calculation
""";

        var agent = AgentDefinition.FromYaml(yaml) as PromptAgent;

        Assert.NotNull(agent);
        Assert.NotNull(agent.Tools);
        Assert.Equal(2, agent.Tools.Count);
    }
}
