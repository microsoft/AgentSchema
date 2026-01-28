// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// Represents a generic server tool that runs on a server
/// This tool kind is designed for operations that require server-side execution
/// It may include features such as authentication, data storage, and long-running processes
/// This tool kind is ideal for tasks that involve complex computations or access to secure resources
/// Server tools can be used to offload heavy processing from client applications
/// </summary>
[JsonConverter(typeof(CustomToolJsonConverter))]
public class CustomTool : Tool
{
    /// <summary>
    /// Initializes a new instance of <see cref="CustomTool"/>.
    /// </summary>
    #pragma warning disable CS8618
    public CustomTool()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for server tools. This is a wildcard and can represent any server tool type not explicitly defined.
    /// </summary>
    public override string Kind { get; set; } = string.Empty;
        
    /// <summary>
    /// Connection configuration for the server tool
    /// </summary>
    public Connection Connection { get; set; }
        
    /// <summary>
    /// Configuration options for the server tool
    /// </summary>
    public IDictionary<string, object> Options { get; set; } = new Dictionary<string, object>();
    
}
