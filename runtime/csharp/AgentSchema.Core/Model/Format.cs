// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Template format definition
/// </summary>
[JsonConverter(typeof(FormatJsonConverter))]
public class Format
{
    /// <summary>
    /// Initializes a new instance of <see cref="Format"/>.
    /// </summary>
    #pragma warning disable CS8618
    public Format()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Template rendering engine used for slot filling prompts (e.g., mustache, jinja2)
    /// </summary>
    public string Kind { get; set; } = string.Empty;
        
    /// <summary>
    /// Whether the template can emit structural text for parsing output
    /// </summary>
    public bool? Strict { get; set; }
        
    /// <summary>
    /// Options for the template engine
    /// </summary>
    public IDictionary<string, object>? Options { get; set; }
    
}
