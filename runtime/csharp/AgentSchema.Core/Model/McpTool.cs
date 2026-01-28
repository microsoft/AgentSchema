// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// The MCP Server tool.
/// </summary>
[JsonConverter(typeof(McpToolJsonConverter))]
public class McpTool : Tool
{
    /// <summary>
    /// Initializes a new instance of <see cref="McpTool"/>.
    /// </summary>
    #pragma warning disable CS8618
    public McpTool()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for MCP tools
    /// </summary>
    public override string Kind { get; set; } = "mcp";
        
    /// <summary>
    /// The connection configuration for the MCP tool
    /// </summary>
    public Connection Connection { get; set; }
        
    /// <summary>
    /// The server name of the MCP tool
    /// </summary>
    public string ServerName { get; set; } = string.Empty;
        
    /// <summary>
    /// The description of the MCP tool
    /// </summary>
    public string? ServerDescription { get; set; }
        
    /// <summary>
    /// The approval mode for the MCP tool, either 'auto' or 'manual'
    /// </summary>
    public McpServerApprovalMode ApprovalMode { get; set; }
        
    /// <summary>
    /// List of allowed operations or resources for the MCP tool
    /// </summary>
    public IList<string>? AllowedTools { get; set; }
    
}
