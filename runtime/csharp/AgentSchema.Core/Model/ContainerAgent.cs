// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// This represents a container based agent hosted by the provider/publisher.
/// The intent is to represent a container application that the user wants to run
/// in a hosted environment that the provider manages.
/// </summary>
[JsonConverter(typeof(ContainerAgentJsonConverter))]
public class ContainerAgent : AgentDefinition
{
    /// <summary>
    /// Initializes a new instance of <see cref="ContainerAgent"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ContainerAgent()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Type of agent, e.g., 'hosted'
    /// </summary>
    public override string Kind { get; set; } = "hosted";
        
    /// <summary>
    /// Protocol used by the containerized agent
    /// </summary>
    public IList<ProtocolVersionRecord> Protocols { get; set; } = [];
        
    /// <summary>
    /// Environment variables to set in the container
    /// </summary>
    public IList<EnvironmentVariable>? EnvironmentVariables { get; set; }
    
}
