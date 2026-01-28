// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(McpServerToolNeverRequireApprovalModeJsonConverter))]
public class McpServerToolNeverRequireApprovalMode : McpServerApprovalMode
{
    /// <summary>
    /// Initializes a new instance of <see cref="McpServerToolNeverRequireApprovalMode"/>.
    /// </summary>
    #pragma warning disable CS8618
    public McpServerToolNeverRequireApprovalMode()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for never approval mode
    /// </summary>
    public override string Kind { get; set; } = "never";
    
}
