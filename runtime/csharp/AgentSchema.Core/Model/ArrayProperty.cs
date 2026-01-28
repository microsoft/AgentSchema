// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents an array property.
/// This extends the base Property model to represent an array of items.
/// </summary>
[JsonConverter(typeof(ArrayPropertyJsonConverter))]
public class ArrayProperty : Property
{
    /// <summary>
    /// Initializes a new instance of <see cref="ArrayProperty"/>.
    /// </summary>
    #pragma warning disable CS8618
    public ArrayProperty()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// 
    /// </summary>
    public override string Kind { get; set; } = "array";
        
    /// <summary>
    /// The type of items contained in the array
    /// </summary>
    public Property Items { get; set; }
    
}
