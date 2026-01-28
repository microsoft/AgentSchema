// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Connection configuration for AI services using API keys.
/// </summary>
[JsonConverter(typeof(ApiKeyConnectionJsonConverter))]
public class ApiKeyConnection : Connection
{
    /// <summary>
    /// Initializes a new instance of <see cref="ApiKeyConnection"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ApiKeyConnection()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The Authentication kind for the AI service (e.g., 'key' for API key, 'oauth' for OAuth tokens)
    /// </summary>
    public override string Kind { get; set; } = "key";
        
    /// <summary>
    /// The endpoint URL for the AI service
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;
        
    /// <summary>
    /// The API key for authenticating with the AI service
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
    
}
