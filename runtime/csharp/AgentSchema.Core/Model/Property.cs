// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents a single property
/// * This model defines the structure of properties that can be used in prompts,
/// including their type, description, whether they are required, and other attributes.
/// * It allows for the definition of dynamic inputs that can be filled with data
/// and processed to generate prompts for AI models.
/// </summary>
[JsonConverter(typeof(PropertyJsonConverter))]
public class Property
{
    /// <summary>
    /// Initializes a new instance of <see cref="Property"/>.
    /// </summary>
    #pragma warning disable CS8618
    public Property()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Name of the property
    /// </summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>
    /// The data type of the input property
    /// </summary>
    public virtual string Kind { get; set; } = string.Empty;
        
    /// <summary>
    /// A short description of the input property
    /// </summary>
    public string? Description { get; set; }
        
    /// <summary>
    /// Whether the property is required
    /// </summary>
    public bool? Required { get; set; }
        
    /// <summary>
    /// The default value of the property - this represents the default value if none is provided
    /// </summary>
    public object? Default { get; set; }
        
    /// <summary>
    /// Example value used for either initialization or tooling
    /// </summary>
    public object? Example { get; set; }
        
    /// <summary>
    /// Allowed enumeration values for the property
    /// </summary>
    public IList<object>? EnumValues { get; set; }
    
}
