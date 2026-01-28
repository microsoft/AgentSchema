// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents a tool resource required by the agent
/// </summary>
[JsonConverter(typeof(ToolResourceJsonConverter))]
public class ToolResource : Resource
{
    /// <summary>
    /// Initializes a new instance of <see cref="ToolResource"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ToolResource()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for tool resources
    /// </summary>
    public override string Kind { get; set; } = "tool";
        
    /// <summary>
    /// The unique identifier of the tool resource
    /// </summary>
    public string Id { get; set; } = string.Empty;
        
    /// <summary>
    /// Configuration options for the tool resource
    /// </summary>
    public IDictionary<string, object> Options { get; set; } = new Dictionary<string, object>();
    
}
