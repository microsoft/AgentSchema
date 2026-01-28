using Xunit;
using System.Text.Json;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130


public class McpToolConversionTests
{   
    [Fact]
    public void LoadYamlInput()
    {
        string yamlData = """
        kind: mcp
        connection:
          kind: reference
        serverName: My MCP Server
        serverDescription: This tool allows access to MCP services.
        approvalMode:
          kind: always
        allowedTools:
          - operation1
          - operation2
        
        """;

        var serializer = Yaml.GetDeserializer();
        var instance = serializer.Deserialize<McpTool>(yamlData);

        Assert.NotNull(instance);
        Assert.Equal("mcp", instance.Kind);
        Assert.Equal("My MCP Server", instance.ServerName);
        Assert.Equal("This tool allows access to MCP services.", instance.ServerDescription);
    }

    [Fact]
    public void LoadJsonInput()
    {
        string jsonData = """
        {
          "kind": "mcp",
          "connection": {
            "kind": "reference"
          },
          "serverName": "My MCP Server",
          "serverDescription": "This tool allows access to MCP services.",
          "approvalMode": {
            "kind": "always"
          },
          "allowedTools": [
            "operation1",
            "operation2"
          ]
        }
        """;

        var instance = JsonSerializer.Deserialize<McpTool>(jsonData);
        Assert.NotNull(instance);
        Assert.Equal("mcp", instance.Kind);
        Assert.Equal("My MCP Server", instance.ServerName);
        Assert.Equal("This tool allows access to MCP services.", instance.ServerDescription);
    }
}