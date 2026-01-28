// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(McpServerToolSpecifyApprovalModeJsonConverter))]
public class McpServerToolSpecifyApprovalMode : McpServerApprovalMode
{
    /// <summary>
    /// Initializes a new instance of <see cref="McpServerToolSpecifyApprovalMode"/>.
    /// </summary>
    #pragma warning disable CS8618
    public McpServerToolSpecifyApprovalMode()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for specify approval mode
    /// </summary>
    public override string Kind { get; set; } = "specify";
        
    /// <summary>
    /// List of tools that always require approval
    /// </summary>
    public IList<string> AlwaysRequireApprovalTools { get; set; } = [];
        
    /// <summary>
    /// List of tools that never require approval
    /// </summary>
    public IList<string> NeverRequireApprovalTools { get; set; } = [];
    
}
