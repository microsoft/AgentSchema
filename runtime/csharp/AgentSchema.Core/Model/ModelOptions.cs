// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Options for configuring the behavior of the AI model.
/// `kind` is a required property here, but this section can accept additional via options.
/// </summary>
[JsonConverter(typeof(ModelOptionsJsonConverter))]
public class ModelOptions
{
    /// <summary>
    /// Initializes a new instance of <see cref="ModelOptions"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ModelOptions()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The frequency penalty to apply to the model's output
    /// </summary>
    public float? FrequencyPenalty { get; set; }
        
    /// <summary>
    /// The maximum number of tokens to generate in the output
    /// </summary>
    public int? MaxOutputTokens { get; set; }
        
    /// <summary>
    /// The presence penalty to apply to the model's output
    /// </summary>
    public float? PresencePenalty { get; set; }
        
    /// <summary>
    /// A random seed for deterministic output
    /// </summary>
    public int? Seed { get; set; }
        
    /// <summary>
    /// The temperature to use for sampling
    /// </summary>
    public float? Temperature { get; set; }
        
    /// <summary>
    /// The top-K sampling value
    /// </summary>
    public int? TopK { get; set; }
        
    /// <summary>
    /// The top-P sampling value
    /// </summary>
    public float? TopP { get; set; }
        
    /// <summary>
    /// Stop sequences to end generation
    /// </summary>
    public IList<string>? StopSequences { get; set; }
        
    /// <summary>
    /// Whether to allow multiple tool calls in a single response
    /// </summary>
    public bool? AllowMultipleToolCalls { get; set; }
        
    /// <summary>
    /// Additional custom properties for model options
    /// </summary>
    public IDictionary<string, object>? AdditionalProperties { get; set; }
    
}
