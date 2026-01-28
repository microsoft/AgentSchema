// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// The following is a specification for defining AI agents with structured metadata, inputs, outputs, tools, and templates.
/// It provides a way to create reusable and composable AI agents that can be executed with specific configurations.
/// The specification includes metadata about the agent, model configuration, input parameters, expected outputs,
/// available tools, and template configurations for prompt rendering.
/// </summary>
[JsonConverter(typeof(AgentDefinitionJsonConverter))]
public abstract class AgentDefinition
{
    /// <summary>
    /// Initializes a new instance of <see cref="AgentDefinition"/>.
    /// </summary>
    #pragma warning disable CS8618
    protected AgentDefinition()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Kind represented by the document
    /// </summary>
    public virtual string Kind { get; set; } = string.Empty;
        
    /// <summary>
    /// Human-readable name of the agent
    /// </summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>
    /// Display name of the agent for UI purposes
    /// </summary>
    public string? DisplayName { get; set; }
        
    /// <summary>
    /// Description of the agent's capabilities and purpose
    /// </summary>
    public string? Description { get; set; }
        
    /// <summary>
    /// Additional metadata including authors, tags, and other arbitrary properties
    /// </summary>
    public IDictionary<string, object>? Metadata { get; set; }
        
    /// <summary>
    /// Input parameters that participate in template rendering
    /// </summary>
    public PropertySchema? InputSchema { get; set; }
        
    /// <summary>
    /// Expected output format and structure from the agent
    /// </summary>
    public PropertySchema? OutputSchema { get; set; }
    
}
