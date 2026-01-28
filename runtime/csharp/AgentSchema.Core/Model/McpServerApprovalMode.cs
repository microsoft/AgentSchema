// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// The approval mode for MCP server tools.
/// </summary>
[JsonConverter(typeof(McpServerApprovalModeJsonConverter))]
public abstract class McpServerApprovalMode
{
    /// <summary>
    /// Initializes a new instance of <see cref="McpServerApprovalMode"/>.
    /// </summary>
    #pragma warning disable CS8618
    protected McpServerApprovalMode()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for string approval modes
    /// </summary>
    public virtual string Kind { get; set; } = string.Empty;
    
}
