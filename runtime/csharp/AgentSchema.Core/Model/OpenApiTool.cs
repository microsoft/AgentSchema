// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(OpenApiToolJsonConverter))]
public class OpenApiTool : Tool
{
    /// <summary>
    /// Initializes a new instance of <see cref="OpenApiTool"/>.
    /// </summary>
    #pragma warning disable CS8618
    public OpenApiTool()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for OpenAPI tools
    /// </summary>
    public override string Kind { get; set; } = "openapi";
        
    /// <summary>
    /// The connection configuration for the OpenAPI tool
    /// </summary>
    public Connection Connection { get; set; }
        
    /// <summary>
    /// The full OpenAPI specification
    /// </summary>
    public string Specification { get; set; } = string.Empty;
    
}
