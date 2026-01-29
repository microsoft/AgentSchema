// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// 
/// </summary>
public class McpServerToolSpecifyApprovalMode : McpServerApprovalMode
{
    /// <summary>
    /// The shorthand property name for this type, if any.
    /// </summary>
    public new static string? ShorthandProperty => null;

    /// <summary>
    /// Initializes a new instance of <see cref="McpServerToolSpecifyApprovalMode"/>.
    /// </summary>
#pragma warning disable CS8618
    public McpServerToolSpecifyApprovalMode()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// The kind identifier for specify approval mode
    /// </summary>
    public override string Kind { get; set; } = "specify";

    /// <summary>
    /// List of tools that always require approval
    /// </summary>
    public IList<string> AlwaysRequireApprovalTools { get; set; } = [];

    /// <summary>
    /// List of tools that never require approval
    /// </summary>
    public IList<string> NeverRequireApprovalTools { get; set; } = [];


    #region Load Methods

    /// <summary>
    /// Load a McpServerToolSpecifyApprovalMode instance from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the data.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded McpServerToolSpecifyApprovalMode instance.</returns>
    public new static McpServerToolSpecifyApprovalMode Load(Dictionary<string, object?> data, LoadContext? context = null)
    {
        if (context is not null)
        {
            data = context.ProcessInput(data);
        }


        // Create new instance
        var instance = new McpServerToolSpecifyApprovalMode();


        if (data.TryGetValue("kind", out var kindValue) && kindValue is not null)
        {
            instance.Kind = kindValue?.ToString()!;
        }

        if (data.TryGetValue("alwaysRequireApprovalTools", out var alwaysRequireApprovalToolsValue) && alwaysRequireApprovalToolsValue is not null)
        {
            instance.AlwaysRequireApprovalTools = (alwaysRequireApprovalToolsValue as IEnumerable<object>)?.Select(x => x?.ToString()!).ToList() ?? [];
        }

        if (data.TryGetValue("neverRequireApprovalTools", out var neverRequireApprovalToolsValue) && neverRequireApprovalToolsValue is not null)
        {
            instance.NeverRequireApprovalTools = (neverRequireApprovalToolsValue as IEnumerable<object>)?.Select(x => x?.ToString()!).ToList() ?? [];
        }

        if (context is not null)
        {
            instance = context.ProcessOutput(instance);
        }
        return instance;
    }



    #endregion

    #region Save Methods

    /// <summary>
    /// Save the McpServerToolSpecifyApprovalMode instance to a dictionary.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The dictionary representation of this instance.</returns>
    public override Dictionary<string, object?> Save(SaveContext? context = null)
    {
        var obj = this;
        if (context is not null)
        {
            obj = context.ProcessObject(obj);
        }


        // Start with parent class properties
        var result = base.Save(context);


        if (obj.Kind is not null)
        {
            result["kind"] = obj.Kind;
        }

        if (obj.AlwaysRequireApprovalTools is not null)
        {
            result["alwaysRequireApprovalTools"] = obj.AlwaysRequireApprovalTools;
        }

        if (obj.NeverRequireApprovalTools is not null)
        {
            result["neverRequireApprovalTools"] = obj.NeverRequireApprovalTools;
        }


        return result;
    }


    /// <summary>
    /// Convert the McpServerToolSpecifyApprovalMode instance to a YAML string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The YAML string representation of this instance.</returns>
    public new string ToYaml(SaveContext? context = null)
    {
        context ??= new SaveContext();
        return context.ToYaml(Save(context));
    }

    /// <summary>
    /// Convert the McpServerToolSpecifyApprovalMode instance to a JSON string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <param name="indent">Whether to indent the output. Defaults to true.</param>
    /// <returns>The JSON string representation of this instance.</returns>
    public new string ToJson(SaveContext? context = null, bool indent = true)
    {
        context ??= new SaveContext();
        return context.ToJson(Save(context), indent);
    }

    /// <summary>
    /// Load a McpServerToolSpecifyApprovalMode instance from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded McpServerToolSpecifyApprovalMode instance.</returns>
    public new static McpServerToolSpecifyApprovalMode FromJson(string json, LoadContext? context = null)
    {
        using var doc = JsonDocument.Parse(json);
        Dictionary<string, object?> dict;
        dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, JsonUtils.Options)
            ?? throw new ArgumentException("Failed to parse JSON as dictionary");

        return Load(dict, context);
    }

    /// <summary>
    /// Load a McpServerToolSpecifyApprovalMode instance from a YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded McpServerToolSpecifyApprovalMode instance.</returns>
    public new static McpServerToolSpecifyApprovalMode FromYaml(string yaml, LoadContext? context = null)
    {
        var dict = YamlUtils.Deserializer.Deserialize<Dictionary<string, object?>>(yaml)
            ?? throw new ArgumentException("Failed to parse YAML as dictionary");

        return Load(dict, context);
    }

    #endregion
}
