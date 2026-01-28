// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents an object property.
/// This extends the base Property model to represent a structured object.
/// </summary>
[JsonConverter(typeof(ObjectPropertyJsonConverter))]
public class ObjectProperty : Property
{
    /// <summary>
    /// Initializes a new instance of <see cref="ObjectProperty"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ObjectProperty()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// 
    /// </summary>
    public override string Kind { get; set; } = "object";
        
    /// <summary>
    /// The properties contained in the object
    /// </summary>
    public IList<Property> Properties { get; set; } = [];
    
}
