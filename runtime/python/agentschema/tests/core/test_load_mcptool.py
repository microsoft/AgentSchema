
import json
import yaml

from agentschema.core import McpTool


def test_load_json_mcptool():
    json_data = '''
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
    '''
    data = json.loads(json_data, strict=False)
    instance = McpTool.load(data)
    assert instance is not None
    assert instance.kind == "mcp"
    assert instance.serverName == "My MCP Server"
    assert instance.serverDescription == "This tool allows access to MCP services."
    

def test_load_yaml_mcptool():
    yaml_data = '''
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
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = McpTool.load(data)
    assert instance is not None
    assert instance.kind == "mcp"
    assert instance.serverName == "My MCP Server"
    assert instance.serverDescription == "This tool allows access to MCP services."


