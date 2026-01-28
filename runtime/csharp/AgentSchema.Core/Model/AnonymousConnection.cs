// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(AnonymousConnectionJsonConverter))]
public class AnonymousConnection : Connection
{
    /// <summary>
    /// Initializes a new instance of <see cref="AnonymousConnection"/>.
    /// </summary>
    #pragma warning disable CS8618
    public AnonymousConnection()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The Authentication kind for the AI service (e.g., 'key' for API key, 'oauth' for OAuth tokens)
    /// </summary>
    public override string Kind { get; set; } = "anonymous";
        
    /// <summary>
    /// The endpoint for authenticating with the AI service
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;
    
}
