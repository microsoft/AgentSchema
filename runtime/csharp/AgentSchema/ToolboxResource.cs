// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130

/// <summary>
/// Represents a Foundry Toolbox resource — a named collection of tools
/// that is provisioned as a Foundry Toolbox and exposed via MCP endpoint.
/// </summary>
public class ToolboxResource : Resource
{
    /// <summary>
    /// The shorthand property name for this type, if any.
    /// </summary>
    public new static string? ShorthandProperty => null;

    /// <summary>
    /// Initializes a new instance of <see cref="ToolboxResource"/>.
    /// </summary>
#pragma warning disable CS8618
    public ToolboxResource()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// The kind identifier for toolbox resources
    /// </summary>
    public override string Kind { get; set; } = "toolbox";

    /// <summary>
    /// Description of the toolbox
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The tools contained in this toolbox
    /// </summary>
    public IList<ToolboxTool> Tools { get; set; } = [];


    #region Load Methods

    /// <summary>
    /// Load a ToolboxResource instance from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the data.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ToolboxResource instance.</returns>
    public new static ToolboxResource Load(Dictionary<string, object?> data, LoadContext? context = null)
    {
        if (context is not null)
        {
            data = context.ProcessInput(data);
        }


        // Create new instance
        var instance = new ToolboxResource();


        if (data.TryGetValue("kind", out var kindValue) && kindValue is not null)
        {
            instance.Kind = kindValue?.ToString()!;
        }

        if (data.TryGetValue("description", out var descriptionValue) && descriptionValue is not null)
        {
            instance.Description = descriptionValue?.ToString()!;
        }

        if (data.TryGetValue("tools", out var toolsValue) && toolsValue is not null)
        {
            instance.Tools = LoadTools(toolsValue, context);
        }

        if (context is not null)
        {
            instance = context.ProcessOutput(instance);
        }
        return instance;
    }


    /// <summary>
    /// Load a list of ToolboxTool from a dictionary or list.
    /// </summary>
    public static IList<ToolboxTool> LoadTools(object data, LoadContext? context)
    {
        var result = new List<ToolboxTool>();

        if (data is Dictionary<string, object?> dict)
        {
            // Convert named dictionary to list
            foreach (var kvp in dict)
            {
                if (kvp.Value is Dictionary<string, object?> itemDict)
                {
                    // Value is an object, add name to it
                    itemDict["name"] = kvp.Key;
                    result.Add(ToolboxTool.Load(itemDict, context));
                }
                else
                {
                    // Value is a scalar, use it as the primary property
                    var newDict = new Dictionary<string, object?>
                    {
                        ["name"] = kvp.Key,
                        [""] = kvp.Value
                    };
                    result.Add(ToolboxTool.Load(newDict, context));
                }
            }
        }
        else if (data is IEnumerable<object> list)
        {
            foreach (var item in list)
            {
                if (item is Dictionary<string, object?> itemDict)
                {
                    result.Add(ToolboxTool.Load(itemDict, context));
                }
            }
        }

        return result;
    }



    #endregion

    #region Save Methods

    /// <summary>
    /// Save the ToolboxResource instance to a dictionary.
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

        if (obj.Description is not null)
        {
            result["description"] = obj.Description;
        }

        if (obj.Tools is not null)
        {
            result["tools"] = SaveTools(obj.Tools, context);
        }


        return result;
    }


    /// <summary>
    /// Save a list of ToolboxTool to object or array format.
    /// </summary>
    public static object SaveTools(IList<ToolboxTool> items, SaveContext? context)
    {
        context ??= new SaveContext();

        // This collection type does not have a 'name' property, only array format is supported
        return items.Select(item => item.Save(context)).ToList();

    }


    /// <summary>
    /// Convert the ToolboxResource instance to a YAML string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The YAML string representation of this instance.</returns>
    public new string ToYaml(SaveContext? context = null)
    {
        context ??= new SaveContext();
        return context.ToYaml(Save(context));
    }

    /// <summary>
    /// Convert the ToolboxResource instance to a JSON string.
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
    /// Load a ToolboxResource instance from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ToolboxResource instance.</returns>
    public new static ToolboxResource FromJson(string json, LoadContext? context = null)
    {
        using var doc = JsonDocument.Parse(json);
        Dictionary<string, object?> dict;
        dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, JsonUtils.Options)
            ?? throw new ArgumentException("Failed to parse JSON as dictionary");

        return Load(dict, context);
    }

    /// <summary>
    /// Load a ToolboxResource instance from a YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ToolboxResource instance.</returns>
    public new static ToolboxResource FromYaml(string yaml, LoadContext? context = null)
    {
        var dict = YamlUtils.Deserializer.Deserialize<Dictionary<string, object?>>(yaml)
            ?? throw new ArgumentException("Failed to parse YAML as dictionary");

        return Load(dict, context);
    }

    #endregion
}
