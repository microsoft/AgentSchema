// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// A tool for searching files.
/// This tool allows an AI agent to search for files based on a query.
/// </summary>
[JsonConverter(typeof(FileSearchToolJsonConverter))]
public class FileSearchTool : Tool
{
    /// <summary>
    /// Initializes a new instance of <see cref="FileSearchTool"/>.
    /// </summary>
    #pragma warning disable CS8618
    public FileSearchTool()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// The kind identifier for file search tools
    /// </summary>
    public override string Kind { get; set; } = "file_search";
        
    /// <summary>
    /// The connection configuration for the file search tool
    /// </summary>
    public Connection Connection { get; set; }
        
    /// <summary>
    /// The IDs of the vector stores to search within.
    /// </summary>
    public IList<string> VectorStoreIds { get; set; } = [];
        
    /// <summary>
    /// The maximum number of search results to return.
    /// </summary>
    public int? MaximumResultCount { get; set; }
        
    /// <summary>
    /// File search ranker.
    /// </summary>
    public string? Ranker { get; set; }
        
    /// <summary>
    /// Ranker search threshold.
    /// </summary>
    public float? ScoreThreshold { get; set; }
        
    /// <summary>
    /// Additional filters to apply during the file search.
    /// </summary>
    public IDictionary<string, object>? Filters { get; set; }
    
}
