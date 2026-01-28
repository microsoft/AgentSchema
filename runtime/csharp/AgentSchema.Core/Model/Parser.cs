// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Template parser definition
/// </summary>
[JsonConverter(typeof(ParserJsonConverter))]
public class Parser
{
    /// <summary>
    /// Initializes a new instance of <see cref="Parser"/>.
    /// </summary>
    #pragma warning disable CS8618
    public Parser()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Parser used to process the rendered template into API-compatible format
    /// </summary>
    public string Kind { get; set; } = string.Empty;
        
    /// <summary>
    /// Options for the parser
    /// </summary>
    public IDictionary<string, object>? Options { get; set; }
    
}
