// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Definition for the property schema of a model.
/// This includes the properties and example records.
/// </summary>
[JsonConverter(typeof(PropertySchemaJsonConverter))]
public class PropertySchema
{
    /// <summary>
    /// Initializes a new instance of <see cref="PropertySchema"/>.
    /// </summary>
    #pragma warning disable CS8618
    public PropertySchema()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Example records for the input schema
    /// </summary>
    public IList<IDictionary<string, object>>? Examples { get; set; }
        
    /// <summary>
    /// Whether the input schema is strict - if true, only the defined properties are allowed
    /// </summary>
    public bool? Strict { get; set; }
        
    /// <summary>
    /// The input properties for the schema
    /// </summary>
    public IList<Property> Properties { get; set; } = [];
    
}
