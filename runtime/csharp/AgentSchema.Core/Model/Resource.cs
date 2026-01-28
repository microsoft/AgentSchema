// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents a resource required by the agent
/// Resources can include databases, APIs, or other external systems
/// that the agent needs to interact with to perform its tasks
/// </summary>
[JsonConverter(typeof(ResourceJsonConverter))]
public abstract class Resource
{
    /// <summary>
    /// Initializes a new instance of <see cref="Resource"/>.
    /// </summary>
    #pragma warning disable CS8618
    protected Resource()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Name of the resource
    /// </summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>
    /// The kind of resource (e.g., model, tool)
    /// </summary>
    public virtual string Kind { get; set; } = string.Empty;
    
}
