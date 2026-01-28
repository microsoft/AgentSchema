// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// A tool for interpreting and executing code.
/// This tool allows an AI agent to run code snippets and analyze data files.
/// </summary>
[JsonConverter(typeof(CodeInterpreterToolJsonConverter))]
public class CodeInterpreterTool : Tool
{
    /// <summary>
    /// Initializes a new instance of <see cref="CodeInterpreterTool"/>.
    /// </summary>
    #pragma warning disable CS8618
    public CodeInterpreterTool()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for code interpreter tools
    /// </summary>
    public override string Kind { get; set; } = "code_interpreter";
        
    /// <summary>
    /// The IDs of the files to be used by the code interpreter tool.
    /// </summary>
    public IList<string> FileIds { get; set; } = [];
    
}
