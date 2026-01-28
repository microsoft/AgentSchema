// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// The following represents a manifest that can be used to create agents dynamically.
/// It includes parameters that can be used to configure the agent&#39;s behavior.
/// These parameters include values that can be used as publisher parameters that can
/// be used to describe additional variables that have been tested and are known to work.
/// 
/// Variables described here are then used to project into a prompt agent that can be executed.
/// Once parameters are provided, these can be referenced in the manifest using the following notation:
/// 
/// `{{myParameter}}`
/// 
/// This allows for dynamic configuration of the agent based on the provided parameters.
/// (This notation is used elsewhere, but only the `param` scope is supported here)
/// </summary>
[JsonConverter(typeof(AgentManifestJsonConverter))]
public class AgentManifest
{
    /// <summary>
    /// Initializes a new instance of <see cref="AgentManifest"/>.
    /// </summary>
    #pragma warning disable CS8618
    public AgentManifest()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Name of the manifest
    /// </summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>
    /// Human-readable name of the manifest
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;
        
    /// <summary>
    /// Description of the agent's capabilities and purpose
    /// </summary>
    public string? Description { get; set; }
        
    /// <summary>
    /// Additional metadata including authors, tags, and other arbitrary properties
    /// </summary>
    public IDictionary<string, object>? Metadata { get; set; }
        
    /// <summary>
    /// The agent that this manifest is based on
    /// </summary>
    public AgentDefinition Template { get; set; }
        
    /// <summary>
    /// Parameters for configuring the agent's behavior and execution
    /// </summary>
    public PropertySchema Parameters { get; set; }
        
    /// <summary>
    /// Resources required by the agent, such as models or tools
    /// </summary>
    public IList<Resource> Resources { get; set; } = [];
    
}
