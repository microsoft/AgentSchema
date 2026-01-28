// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(ProtocolVersionRecordJsonConverter))]
public class ProtocolVersionRecord
{
    /// <summary>
    /// Initializes a new instance of <see cref="ProtocolVersionRecord"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ProtocolVersionRecord()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The protocol type.
    /// </summary>
    public string Protocol { get; set; } = string.Empty;
        
    /// <summary>
    /// The version string for the protocol, e.g. 'v0.1.1'.
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
}
