// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// The Bing search tool.
/// </summary>
[JsonConverter(typeof(WebSearchToolJsonConverter))]
public class WebSearchTool : Tool
{
    /// <summary>
    /// Initializes a new instance of <see cref="WebSearchTool"/>.
    /// </summary>
    #pragma warning disable CS8618
    public WebSearchTool()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for Bing search tools
    /// </summary>
    public override string Kind { get; set; } = "bing_search";
        
    /// <summary>
    /// The connection configuration for the Bing search tool
    /// </summary>
    public Connection Connection { get; set; }
        
    /// <summary>
    /// The configuration options for the Bing search tool
    /// </summary>
    public IDictionary<string, object>? Options { get; set; }
    
}
