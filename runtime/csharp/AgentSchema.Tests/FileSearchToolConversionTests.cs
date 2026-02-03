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
}