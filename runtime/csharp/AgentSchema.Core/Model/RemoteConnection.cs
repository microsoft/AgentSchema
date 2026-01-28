// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Connection configuration for AI services using named connections.
/// </summary>
[JsonConverter(typeof(RemoteConnectionJsonConverter))]
public class RemoteConnection : Connection
{
    /// <summary>
    /// Initializes a new instance of <see cref="RemoteConnection"/>.
    /// </summary>
    #pragma warning disable CS8618
    public RemoteConnection()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The Authentication kind for the AI service (e.g., 'key' for API key, 'oauth' for OAuth tokens)
    /// </summary>
    public override string Kind { get; set; } = "remote";
        
    /// <summary>
    /// The name of the connection
    /// </summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>
    /// The endpoint URL for the AI service
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;
    
}
