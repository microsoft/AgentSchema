import "../core.tsp";
import "../tools.tsp";
import "../connection.tsp";

namespace AgentSchema;

/**
 * The MCP Server tool.
 */
model McpTool extends Tool {
  @doc("The kind identifier for MCP tools")
  @sample(#{ kind: "mcp" })
  kind: "mcp";

  @doc("The connection configuration for the MCP tool")
  @sample(#{ connection: #{ kind: "reference" } })
  connection: Connection;

  @doc("The server name of the MCP tool")
  @sample(#{ serverName: "My MCP Server" })
  serverName: string;

  @doc("The description of the MCP tool")
  @sample(#{ serverDescription: "This tool allows access to MCP services." })
  serverDescription?: string;

  @doc("The approval mode for the MCP tool, either 'auto' or 'manual'")
  @sample(#{ approvalMode: #{ kind: "always" } })
  approvalMode: McpServerApprovalMode;

  @doc("List of allowed operations or resources for the MCP tool")
  @sample(#{ allowedTools: #["operation1", "operation2"] })
  allowedTools?: string[];
}

alias mcpApprovalMode = "always" | "never" | "specify";

/**
 * The approval mode for MCP server tools.
 */
@abstract
@discriminator("kind")
@shorthand(string, #{ kind: "{value}" }, "kind", "Mcp Approval Mode", "never")
model McpServerApprovalMode {
  @doc("The kind identifier for string approval modes")
  @sample(#{ kind: "never" })
  kind: mcpApprovalMode;
}

model McpServerToolAlwaysRequireApprovalMode extends McpServerApprovalMode {
  @doc("The kind identifier for always approval mode")
  @sample(#{ kind: "always" })
  kind: "always";
}

model McpServerToolNeverRequireApprovalMode extends McpServerApprovalMode {
  @doc("The kind identifier for never approval mode")
  @sample(#{ kind: "never" })
  kind: "never";
}

model McpServerToolSpecifyApprovalMode extends McpServerApprovalMode {
  @doc("The kind identifier for specify approval mode")
  @sample(#{ kind: "specify" })
  kind: "specify";

  @doc("List of tools that always require approval")
  @sample(#{ alwaysRequireApprovalTools: #["operation1"] })
  alwaysRequireApprovalTools: string[];

  @doc("List of tools that never require approval")
  @sample(#{ neverRequireApprovalTools: #["operation2"] })
  neverRequireApprovalTools: string[];
}
