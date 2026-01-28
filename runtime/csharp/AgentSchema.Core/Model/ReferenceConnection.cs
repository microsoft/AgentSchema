// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Connection configuration for AI services using named connections.
/// </summary>
[JsonConverter(typeof(ReferenceConnectionJsonConverter))]
public class ReferenceConnection : Connection
{
    /// <summary>
    /// Initializes a new instance of <see cref="ReferenceConnection"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ReferenceConnection()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The Authentication kind for the AI service (e.g., 'key' for API key, 'oauth' for OAuth tokens)
    /// </summary>
    public override string Kind { get; set; } = "reference";
        
    /// <summary>
    /// The name of the connection
    /// </summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>
    /// The target resource or service that this connection refers to
    /// </summary>
    public string? Target { get; set; }
    
}
