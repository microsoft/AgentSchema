// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Template model for defining prompt templates.
/// 
/// This model specifies the rendering engine used for slot filling prompts,
/// the parser used to process the rendered template into API-compatible format,
/// and additional options for the template engine.
/// 
/// It allows for the creation of reusable templates that can be filled with dynamic data
/// and processed to generate prompts for AI models.
/// </summary>
[JsonConverter(typeof(TemplateJsonConverter))]
public class Template
{
    /// <summary>
    /// Initializes a new instance of <see cref="Template"/>.
    /// </summary>
    #pragma warning disable CS8618
    public Template()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Template rendering engine used for slot filling prompts (e.g., mustache, jinja2)
    /// </summary>
    public Format Format { get; set; }
        
    /// <summary>
    /// Parser used to process the rendered template into API-compatible format
    /// </summary>
    public Parser Parser { get; set; }
    
}
