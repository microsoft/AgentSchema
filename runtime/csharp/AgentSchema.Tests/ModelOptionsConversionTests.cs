using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class ModelOptionsConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
"frequencyPenalty": 0.5
"maxOutputTokens": 2048
"presencePenalty": 0.3
"seed": 42
"temperature": 0.7
"topK": 40
"topP": 0.9
"stopSequences":
  - "\n"
  - "###"
"allowMultipleToolCalls": true
"additionalProperties":
  "customProperty": "value"
  "anotherProperty": "anotherValue"

""";

        var instance = ModelOptions.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal(0.5f, instance.FrequencyPenalty);
        Assert.Equal(2048, instance.MaxOutputTokens);
        Assert.Equal(0.3f, instance.PresencePenalty);
        Assert.Equal(42, instance.Seed);
        Assert.Equal(0.7f, instance.Temperature);
        Assert.Equal(40, instance.TopK);
        Assert.Equal(0.9f, instance.TopP);
        Assert.True(instance.AllowMultipleToolCalls);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "frequencyPenalty": 0.5,
  "maxOutputTokens": 2048,
  "presencePenalty": 0.3,
  "seed": 42,
  "temperature": 0.7,
  "topK": 40,
  "topP": 0.9,
  "stopSequences": [
    "\n",
    "###"
  ],
  "allowMultipleToolCalls": true,
  "additionalProperties": {
    "customProperty": "value",
    "anotherProperty": "anotherValue"
  }
}
""";

        var instance = ModelOptions.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal(0.5f, instance.FrequencyPenalty);
        Assert.Equal(2048, instance.MaxOutputTokens);
        Assert.Equal(0.3f, instance.PresencePenalty);
        Assert.Equal(42, instance.Seed);
        Assert.Equal(0.7f, instance.Temperature);
        Assert.Equal(40, instance.TopK);
        Assert.Equal(0.9f, instance.TopP);
        Assert.True(instance.AllowMultipleToolCalls);
    }
}