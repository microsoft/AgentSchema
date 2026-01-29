// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

/// <summary>
/// The following represents a manifest that can be used to create agents dynamically.
/// It includes parameters that can be used to configure the agent&#39;s behavior.
/// These parameters include values that can be used as publisher parameters that can
/// be used to describe additional variables that have been tested and are known to work.
/// 
/// Variables described here are then used to project into a prompt agent that can be executed.
/// Once parameters are provided, these can be referenced in the manifest using the following notation:
/// 
/// `{{myParameter}}`
/// 
/// This allows for dynamic configuration of the agent based on the provided parameters.
/// (This notation is used elsewhere, but only the `param` scope is supported here)
/// </summary>
public class AgentManifest
{
    /// <summary>
    /// The shorthand property name for this type, if any.
    /// </summary>
    public static string? ShorthandProperty => null;

    /// <summary>
    /// Initializes a new instance of <see cref="AgentManifest"/>.
    /// </summary>
#pragma warning disable CS8618
    public AgentManifest()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Name of the manifest
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable name of the manifest
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Description of the agent's capabilities and purpose
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Additional metadata including authors, tags, and other arbitrary properties
    /// </summary>
    public IDictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// The agent that this manifest is based on
    /// </summary>
    public AgentDefinition Template { get; set; }

    /// <summary>
    /// Parameters for configuring the agent's behavior and execution
    /// </summary>
    public PropertySchema Parameters { get; set; }

    /// <summary>
    /// Resources required by the agent, such as models or tools
    /// </summary>
    public IList<Resource> Resources { get; set; } = [];


    #region Load Methods

    /// <summary>
    /// Load a AgentManifest instance from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the data.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded AgentManifest instance.</returns>
    public static AgentManifest Load(Dictionary<string, object?> data, LoadContext? context = null)
    {
        if (context is not null)
        {
            data = context.ProcessInput(data);
        }


        // Create new instance
        var instance = new AgentManifest();


        if (data.TryGetValue("name", out var nameValue) && nameValue is not null)
        {
            instance.Name = nameValue?.ToString()!;
        }

        if (data.TryGetValue("displayName", out var displayNameValue) && displayNameValue is not null)
        {
            instance.DisplayName = displayNameValue?.ToString()!;
        }

        if (data.TryGetValue("description", out var descriptionValue) && descriptionValue is not null)
        {
            instance.Description = descriptionValue?.ToString()!;
        }

        if (data.TryGetValue("metadata", out var metadataValue) && metadataValue is not null)
        {
            instance.Metadata = metadataValue.GetDictionary()!;
        }

        if (data.TryGetValue("template", out var templateValue) && templateValue is not null)
        {
            instance.Template = AgentDefinition.Load(templateValue.GetDictionary(), context);
        }

        if (data.TryGetValue("parameters", out var parametersValue) && parametersValue is not null)
        {
            instance.Parameters = PropertySchema.Load(parametersValue.GetDictionary(), context);
        }

        if (data.TryGetValue("resources", out var resourcesValue) && resourcesValue is not null)
        {
            instance.Resources = LoadResources(resourcesValue, context);
        }

        if (context is not null)
        {
            instance = context.ProcessOutput(instance);
        }
        return instance;
    }


    /// <summary>
    /// Load a list of Resource from a dictionary or list.
    /// </summary>
    public static IList<Resource> LoadResources(object data, LoadContext? context)
    {
        var result = new List<Resource>();

        if (data is Dictionary<string, object?> dict)
        {
            // Convert named dictionary to list
            foreach (var kvp in dict)
            {
                if (kvp.Value is Dictionary<string, object?> itemDict)
                {
                    // Value is an object, add name to it
                    itemDict["name"] = kvp.Key;
                    result.Add(Resource.Load(itemDict, context));
                }
                else
                {
                    // Value is a scalar, use it as the primary property
                    var newDict = new Dictionary<string, object?>
                    {
                        ["name"] = kvp.Key,
                        [""] = kvp.Value
                    };
                    result.Add(Resource.Load(newDict, context));
                }
            }
        }
        else if (data is IEnumerable<object> list)
        {
            foreach (var item in list)
            {
                if (item is Dictionary<string, object?> itemDict)
                {
                    result.Add(Resource.Load(itemDict, context));
                }
            }
        }

        return result;
    }



    #endregion

    #region Save Methods

    /// <summary>
    /// Save the AgentManifest instance to a dictionary.
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


        if (obj.Name is not null)
        {
            result["name"] = obj.Name;
        }

        if (obj.DisplayName is not null)
        {
            result["displayName"] = obj.DisplayName;
        }

        if (obj.Description is not null)
        {
            result["description"] = obj.Description;
        }

        if (obj.Metadata is not null)
        {
            result["metadata"] = obj.Metadata;
        }

        if (obj.Template is not null)
        {
            result["template"] = obj.Template?.Save(context);
        }

        if (obj.Parameters is not null)
        {
            result["parameters"] = obj.Parameters?.Save(context);
        }

        if (obj.Resources is not null)
        {
            result["resources"] = SaveResources(obj.Resources, context);
        }


        if (context is not null)
        {
            result = context.ProcessDict(result);
        }

        return result;
    }


    /// <summary>
    /// Save a list of Resource to object or array format.
    /// </summary>
    public static object SaveResources(IList<Resource> items, SaveContext? context)
    {
        context ??= new SaveContext();

        if (context.CollectionFormat == "array")
        {
            return items.Select(item => item.Save(context)).ToList();
        }

        // Object format: use name as key
        var result = new Dictionary<string, object?>();
        foreach (var item in items)
        {
            var itemData = item.Save(context);
            if (itemData.TryGetValue("name", out var nameValue) && nameValue is string name)
            {
                itemData.Remove("name");

                // Check if we can use shorthand
                if (context.UseShorthand && Resource.ShorthandProperty is string shorthandProp)
                {
                    if (itemData.Count == 1 && itemData.ContainsKey(shorthandProp))
                    {
                        result[name] = itemData[shorthandProp];
                        continue;
                    }
                }
                result[name] = itemData;
            }
            else
            {
                // No name, can't use object format for this item
                throw new InvalidOperationException("Cannot save item in object format: missing 'name' property");
            }
        }
        return result;
    }


    /// <summary>
    /// Convert the AgentManifest instance to a YAML string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The YAML string representation of this instance.</returns>
    public string ToYaml(SaveContext? context = null)
    {
        context ??= new SaveContext();
        return context.ToYaml(Save(context));
    }

    /// <summary>
    /// Convert the AgentManifest instance to a JSON string.
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
    /// Load a AgentManifest instance from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded AgentManifest instance.</returns>
    public static AgentManifest FromJson(string json, LoadContext? context = null)
    {
        using var doc = JsonDocument.Parse(json);
        Dictionary<string, object?> dict;
        dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, JsonUtils.Options)
            ?? throw new ArgumentException("Failed to parse JSON as dictionary");

        return Load(dict, context);
    }

    /// <summary>
    /// Load a AgentManifest instance from a YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded AgentManifest instance.</returns>
    public static AgentManifest FromYaml(string yaml, LoadContext? context = null)
    {
        var dict = YamlUtils.Deserializer.Deserialize<Dictionary<string, object?>>(yaml)
            ?? throw new ArgumentException("Failed to parse YAML as dictionary");

        return Load(dict, context);
    }

    #endregion
}
