// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Model for defining the structure and behavior of AI agents.
/// This model includes properties for specifying the model&#39;s provider, connection details, and various options.
/// It allows for flexible configuration of AI models to suit different use cases and requirements.
/// </summary>
[JsonConverter(typeof(ModelJsonConverter))]
public class Model
{
    /// <summary>
    /// Initializes a new instance of <see cref="Model"/>.
    /// </summary>
    #pragma warning disable CS8618
    public Model()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The unique identifier of the model - can be used as the single property shorthand
    /// </summary>
    public string Id { get; set; } = string.Empty;
        
    /// <summary>
    /// The provider of the model (e.g., 'openai', 'azure', 'anthropic')
    /// </summary>
    public string? Provider { get; set; }
        
    /// <summary>
    /// The type of API to use for the model (e.g., 'chat', 'response', etc.)
    /// </summary>
    public string? ApiType { get; set; }
        
    /// <summary>
    /// The connection configuration for the model
    /// </summary>
    public Connection? Connection { get; set; }
        
    /// <summary>
    /// Additional options for the model
    /// </summary>
    public ModelOptions? Options { get; set; }
    
}
