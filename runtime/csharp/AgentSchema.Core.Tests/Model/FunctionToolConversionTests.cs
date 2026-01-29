using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class FunctionToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"kind": "function"
"parameters":
  "properties":
    "firstName":
      "kind": "string"
      "value": "Jane"
    "lastName":
      "kind": "string"
      "value": "Doe"
    "question":
      "kind": "string"
      "value": "What is the meaning of life?"
"strict": true

""";

        var instance = FunctionTool.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("function", instance.Kind);
        Assert.True(instance.Strict);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "function",
  "parameters": {
    "properties": {
      "firstName": {
        "kind": "string",
        "value": "Jane"
      },
      "lastName": {
        "kind": "string",
        "value": "Doe"
      },
      "question": {
        "kind": "string",
        "value": "What is the meaning of life?"
      }
    }
  },
  "strict": true
}
""";

        var instance = FunctionTool.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("function", instance.Kind);
        Assert.True(instance.Strict);
    }
    [Fact]
    public void LoadYamlInput1()
    {
        string yamlData = """
"kind": "function"
"parameters":
  "properties":
    - "name": "firstName"
      "kind": "string"
      "value": "Jane"
    - "name": "lastName"
      "kind": "string"
      "value": "Doe"
    - "name": "question"
      "kind": "string"
      "value": "What is the meaning of life?"
"strict": true

""";

        var instance = FunctionTool.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("function", instance.Kind);
        Assert.True(instance.Strict);
    }

    [Fact]
    public void LoadJsonInput1()
    {
        string jsonData = """
{
  "kind": "function",
  "parameters": {
    "properties": [
      {
        "name": "firstName",
        "kind": "string",
        "value": "Jane"
      },
      {
        "name": "lastName",
        "kind": "string",
        "value": "Doe"
      },
      {
        "name": "question",
        "kind": "string",
        "value": "What is the meaning of life?"
      }
    ]
  },
  "strict": true
}
""";

        var instance = FunctionTool.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("function", instance.Kind);
        Assert.True(instance.Strict);
    }
}