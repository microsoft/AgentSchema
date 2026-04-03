// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130

/// <summary>
/// Represents a tool definition within a toolbox.
/// Tools can be Foundry-hosted (web_search, azure_ai_search, etc.)
/// or external (mcp, openapi, a2a_preview) with connection details.
/// </summary>
public class ToolboxTool
{
    /// <summary>
    /// The shorthand property name for this type, if any.
    /// </summary>
    public static string? ShorthandProperty => null;

    /// <summary>
    /// Initializes a new instance of <see cref="ToolboxTool"/>.
    /// </summary>
#pragma warning disable CS8618
    public ToolboxTool()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// The tool type identifier (e.g., 'web_search', 'azure_ai_search', 'mcp', 'a2a_preview')
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Optional display name for the tool
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Human-readable description of the tool's capabilities
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Target endpoint URL for external tools (e.g., MCP server URL, A2A agent URL)
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// Authentication type for the tool connection
    /// </summary>
    public string? AuthType { get; set; }

    /// <summary>
    /// Additional configuration options for the tool
    /// </summary>
    public IDictionary<string, object>? Options { get; set; }


    #region Load Methods

    /// <summary>
    /// Load a ToolboxTool instance from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the data.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ToolboxTool instance.</returns>
    public static ToolboxTool Load(Dictionary<string, object?> data, LoadContext? context = null)
    {
        if (context is not null)
        {
            data = context.ProcessInput(data);
        }


        // Create new instance
        var instance = new ToolboxTool();


        if (data.TryGetValue("id", out var idValue) && idValue is not null)
        {
            instance.Id = idValue?.ToString()!;
        }

        if (data.TryGetValue("name", out var nameValue) && nameValue is not null)
        {
            instance.Name = nameValue?.ToString()!;
        }

        if (data.TryGetValue("description", out var descriptionValue) && descriptionValue is not null)
        {
            instance.Description = descriptionValue?.ToString()!;
        }

        if (data.TryGetValue("target", out var targetValue) && targetValue is not null)
        {
            instance.Target = targetValue?.ToString()!;
        }

        if (data.TryGetValue("authType", out var authTypeValue) && authTypeValue is not null)
        {
            instance.AuthType = authTypeValue?.ToString()!;
        }

        if (data.TryGetValue("options", out var optionsValue) && optionsValue is not null)
        {
            instance.Options = optionsValue.GetDictionary()!;
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
    /// Save the ToolboxTool instance to a dictionary.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The dictionary representation of this instance.</returns>
    public Dictionary<string, object?> Save(SaveContext? context = null)
    {
        var obj = this;
        if (context is not null)
        {
            obj = context.ProcessObject(obj);
        }


        var result = new Dictionary<string, object?>();


        if (obj.Id is not null)
        {
            result["id"] = obj.Id;
        }

        if (obj.Name is not null)
        {
            result["name"] = obj.Name;
        }

        if (obj.Description is not null)
        {
            result["description"] = obj.Description;
        }

        if (obj.Target is not null)
        {
            result["target"] = obj.Target;
        }

        if (obj.AuthType is not null)
        {
            result["authType"] = obj.AuthType;
        }

        if (obj.Options is not null)
        {
            result["options"] = obj.Options;
        }


        if (context is not null)
        {
            result = context.ProcessDict(result);
        }

        return result;
    }


    /// <summary>
    /// Convert the ToolboxTool instance to a YAML string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The YAML string representation of this instance.</returns>
    public string ToYaml(SaveContext? context = null)
    {
        context ??= new SaveContext();
        return context.ToYaml(Save(context));
    }

    /// <summary>
    /// Convert the ToolboxTool instance to a JSON string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <param name="indent">Whether to indent the output. Defaults to true.</param>
    /// <returns>The JSON string representation of this instance.</returns>
    public string ToJson(SaveContext? context = null, bool indent = true)
    {
        context ??= new SaveContext();
        return context.ToJson(Save(context), indent);
    }

    /// <summary>
    /// Load a ToolboxTool instance from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ToolboxTool instance.</returns>
    public static ToolboxTool FromJson(string json, LoadContext? context = null)
    {
        using var doc = JsonDocument.Parse(json);
        Dictionary<string, object?> dict;
        dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, JsonUtils.Options)
            ?? throw new ArgumentException("Failed to parse JSON as dictionary");

        return Load(dict, context);
    }

    /// <summary>
    /// Load a ToolboxTool instance from a YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ToolboxTool instance.</returns>
    public static ToolboxTool FromYaml(string yaml, LoadContext? context = null)
    {
        var dict = YamlUtils.Deserializer.Deserialize<Dictionary<string, object?>>(yaml)
            ?? throw new ArgumentException("Failed to parse YAML as dictionary");

        return Load(dict, context);
    }

    #endregion
}
