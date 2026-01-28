// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents a binding between an input property and a tool parameter.
/// </summary>
[JsonConverter(typeof(BindingJsonConverter))]
public class Binding
{
    /// <summary>
    /// Initializes a new instance of <see cref="Binding"/>.
    /// </summary>
    #pragma warning disable CS8618
    public Binding()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Name of the binding
    /// </summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>
    /// The input property that will be bound to the tool parameter argument
    /// </summary>
    public string Input { get; set; } = string.Empty;
    
}
