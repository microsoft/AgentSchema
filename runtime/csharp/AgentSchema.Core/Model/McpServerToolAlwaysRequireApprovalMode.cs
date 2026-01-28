// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(McpServerToolAlwaysRequireApprovalModeJsonConverter))]
public class McpServerToolAlwaysRequireApprovalMode : McpServerApprovalMode
{
    /// <summary>
    /// Initializes a new instance of <see cref="McpServerToolAlwaysRequireApprovalMode"/>.
    /// </summary>
    #pragma warning disable CS8618
    public McpServerToolAlwaysRequireApprovalMode()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for always approval mode
    /// </summary>
    public override string Kind { get; set; } = "always";
    
}
