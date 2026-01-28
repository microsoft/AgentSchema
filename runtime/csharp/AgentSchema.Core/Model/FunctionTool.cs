// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents a local function tool.
/// </summary>
[JsonConverter(typeof(FunctionToolJsonConverter))]
public class FunctionTool : Tool
{
    /// <summary>
    /// Initializes a new instance of <see cref="FunctionTool"/>.
    /// </summary>
    #pragma warning disable CS8618
    public FunctionTool()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for function tools
    /// </summary>
    public override string Kind { get; set; } = "function";
        
    /// <summary>
    /// Parameters accepted by the function tool
    /// </summary>
    public PropertySchema Parameters { get; set; }
        
    /// <summary>
    /// Indicates whether the function tool enforces strict validation on its parameters
    /// </summary>
    public bool? Strict { get; set; }
    
}
