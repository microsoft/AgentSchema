// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents a model resource required by the agent
/// </summary>
[JsonConverter(typeof(ModelResourceJsonConverter))]
public class ModelResource : Resource
{
    /// <summary>
    /// Initializes a new instance of <see cref="ModelResource"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ModelResource()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for model resources
    /// </summary>
    public override string Kind { get; set; } = "model";
        
    /// <summary>
    /// The unique identifier of the model resource
    /// </summary>
    public string Id { get; set; } = string.Empty;
    
}
