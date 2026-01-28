// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// A workflow agent that can orchestrate multiple steps and actions.
/// This agent type is designed to handle complex workflows that may involve
/// multiple tools, models, and decision points.
/// 
/// The workflow agent can be configured with a series of steps that define
/// the flow of execution, including conditional logic and parallel processing.
/// This allows for the creation of sophisticated AI-driven processes that can
/// adapt to various scenarios and requirements.
/// 
/// Note: The detailed structure of the workflow steps and actions is not defined here
/// and would need to be implemented based on specific use cases and requirements.
/// </summary>
[JsonConverter(typeof(WorkflowJsonConverter))]
public class Workflow : AgentDefinition
{
    /// <summary>
    /// Initializes a new instance of <see cref="Workflow"/>.
    /// </summary>
    #pragma warning disable CS8618
    public Workflow()
    {
    }
    #pragma warning restore CS8618
        
    /// <summary>
    /// Type of agent, e.g., 'workflow'
    /// </summary>
    public override string Kind { get; set; } = "workflow";
        
    /// <summary>
    /// The steps that make up the workflow
    /// </summary>
    public IDictionary<string, object>? Trigger { get; set; }
    
}
