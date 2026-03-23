
using Xunit;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130


public class FileSearchToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
kind: file_search
connection:
  kind: reference
vectorStoreIds:
  - vectorStore1
  - vectorStore2
maximumResultCount: 10
ranker: auto
scoreThreshold: 0.5
filters:
  fileType: pdf
  createdAfter: 2023-01-01

""";

        var instance = FileSearchTool.FromYaml(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("file_search", instance.Kind);
        Assert.Equal(10, instance.MaximumResultCount);
        Assert.Equal("auto", instance.Ranker);
        Assert.Equal(0.5f, instance.ScoreThreshold);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
{
  "kind": "file_search",
  "connection": {
    "kind": "reference"
  },
  "vectorStoreIds": [
    "vectorStore1",
    "vectorStore2"
  ],
  "maximumResultCount": 10,
  "ranker": "auto",
  "scoreThreshold": 0.5,
  "filters": {
    "fileType": "pdf",
    "createdAfter": "2023-01-01"
  }
}
""";

        var instance = FileSearchTool.FromJson(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("file_search", instance.Kind);
        Assert.Equal(10, instance.MaximumResultCount);
        Assert.Equal("auto", instance.Ranker);
        Assert.Equal(0.5f, instance.ScoreThreshold);
    }

    [Fact]
    public void RoundtripJson()
    {
        // Test that FromJson -> ToJson -> FromJson produces equivalent data
        string jsonData = """
{
  "kind": "file_search",
  "connection": {
    "kind": "reference"
  },
  "vectorStoreIds": [
    "vectorStore1",
    "vectorStore2"
  ],
  "maximumResultCount": 10,
  "ranker": "auto",
  "scoreThreshold": 0.5,
  "filters": {
    "fileType": "pdf",
    "createdAfter": "2023-01-01"
  }
}
""";

        var original = FileSearchTool.FromJson(jsonData);
        Assert.NotNull(original);
        
        var json = original.ToJson();
        Assert.False(string.IsNullOrEmpty(json));
        
        var reloaded = FileSearchTool.FromJson(json);
        Assert.NotNull(reloaded);
        Assert.Equal("file_search", reloaded.Kind);
        Assert.Equal(10, reloaded.MaximumResultCount);
        Assert.Equal("auto", reloaded.Ranker);
        Assert.Equal(0.5f, reloaded.ScoreThreshold);
    }

    [Fact]
    public void RoundtripYaml()
    {
        // Test that FromYaml -> ToYaml -> FromYaml produces equivalent data
        string yamlData = """
kind: file_search
connection:
  kind: reference
vectorStoreIds:
  - vectorStore1
  - vectorStore2
maximumResultCount: 10
ranker: auto
scoreThreshold: 0.5
filters:
  fileType: pdf
  createdAfter: 2023-01-01

""";

        var original = FileSearchTool.FromYaml(yamlData);
        Assert.NotNull(original);
        
        var yaml = original.ToYaml();
        Assert.False(string.IsNullOrEmpty(yaml));
        
        var reloaded = FileSearchTool.FromYaml(yaml);
        Assert.NotNull(reloaded);
        Assert.Equal("file_search", reloaded.Kind);
        Assert.Equal(10, reloaded.MaximumResultCount);
        Assert.Equal("auto", reloaded.Ranker);
        Assert.Equal(0.5f, reloaded.ScoreThreshold);
    }

    [Fact]
    public void ToJsonProducesValidJson()
    {
        string jsonData = """
{
  "kind": "file_search",
  "connection": {
    "kind": "reference"
  },
  "vectorStoreIds": [
    "vectorStore1",
    "vectorStore2"
  ],
  "maximumResultCount": 10,
  "ranker": "auto",
  "scoreThreshold": 0.5,
  "filters": {
    "fileType": "pdf",
    "createdAfter": "2023-01-01"
  }
}
""";

        var instance = FileSearchTool.FromJson(jsonData);
        var json = instance.ToJson();
        
        // Verify it's valid JSON by parsing it
        var parsed = System.Text.Json.JsonDocument.Parse(json);
        Assert.NotNull(parsed);
    }

    [Fact]
    public void ToYamlProducesValidYaml()
    {
        string yamlData = """
kind: file_search
connection:
  kind: reference
vectorStoreIds:
  - vectorStore1
  - vectorStore2
maximumResultCount: 10
ranker: auto
scoreThreshold: 0.5
filters:
  fileType: pdf
  createdAfter: 2023-01-01

""";

        var instance = FileSearchTool.FromYaml(yamlData);
        var yaml = instance.ToYaml();
        
        // Verify it's valid YAML by parsing it
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
        var parsed = deserializer.Deserialize<object>(yaml);
        Assert.NotNull(parsed);
    }
}
