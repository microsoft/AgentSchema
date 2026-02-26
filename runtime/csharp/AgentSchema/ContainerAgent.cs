// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130

/// <summary>
/// This represents a container based agent hosted by the provider/publisher.
/// The intent is to represent a container application that the user wants to run
/// in a hosted environment that the provider manages.
/// </summary>
public class ContainerAgent : AgentDefinition
{
    /// <summary>
    /// The shorthand property name for this type, if any.
    /// </summary>
    public new static string? ShorthandProperty => null;

    /// <summary>
    /// Initializes a new instance of <see cref="ContainerAgent"/>.
    /// </summary>
#pragma warning disable CS8618
    public ContainerAgent()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Type of agent, e.g., 'hosted'
    /// </summary>
    public override string Kind { get; set; } = "hosted";

    /// <summary>
    /// Protocol used by the containerized agent
    /// </summary>
    public IList<ProtocolVersionRecord> Protocols { get; set; } = [];

    /// <summary>
    /// Container image path (e.g., '<acr-endpoint>/<container-image-name>')
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Resource allocation for the container
    /// </summary>
    public ContainerResources Resources { get; set; }

    /// <summary>
    /// Environment variables to set in the container
    /// </summary>
    public IList<EnvironmentVariable>? EnvironmentVariables { get; set; }


    #region Load Methods

    /// <summary>
    /// Load a ContainerAgent instance from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the data.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ContainerAgent instance.</returns>
    public new static ContainerAgent Load(Dictionary<string, object?> data, LoadContext? context = null)
    {
        if (context is not null)
        {
            data = context.ProcessInput(data);
        }


        // Create new instance
        var instance = new ContainerAgent();


        if (data.TryGetValue("kind", out var kindValue) && kindValue is not null)
        {
            instance.Kind = kindValue?.ToString()!;
        }

        if (data.TryGetValue("protocols", out var protocolsValue) && protocolsValue is not null)
        {
            instance.Protocols = LoadProtocols(protocolsValue, context);
        }

        if (data.TryGetValue("image", out var imageValue) && imageValue is not null)
        {
            instance.Image = imageValue?.ToString()!;
        }

        if (data.TryGetValue("resources", out var resourcesValue) && resourcesValue is not null)
        {
            instance.Resources = ContainerResources.Load(resourcesValue.GetDictionary(), context);
        }

        if (data.TryGetValue("environmentVariables", out var environmentVariablesValue) && environmentVariablesValue is not null)
        {
            instance.EnvironmentVariables = LoadEnvironmentVariables(environmentVariablesValue, context);
        }

        if (context is not null)
        {
            instance = context.ProcessOutput(instance);
        }
        return instance;
    }


    /// <summary>
    /// Load a list of ProtocolVersionRecord from a dictionary or list.
    /// </summary>
    public static IList<ProtocolVersionRecord> LoadProtocols(object data, LoadContext? context)
    {
        var result = new List<ProtocolVersionRecord>();

        if (data is Dictionary<string, object?> dict)
        {
            // Convert named dictionary to list
            foreach (var kvp in dict)
            {
                if (kvp.Value is Dictionary<string, object?> itemDict)
                {
                    // Value is an object, add name to it
                    itemDict["name"] = kvp.Key;
                    result.Add(ProtocolVersionRecord.Load(itemDict, context));
                }
                else
                {
                    // Value is a scalar, use it as the primary property
                    var newDict = new Dictionary<string, object?>
                    {
                        ["name"] = kvp.Key,
                        [""] = kvp.Value
                    };
                    result.Add(ProtocolVersionRecord.Load(newDict, context));
                }
            }
        }
        else if (data is IEnumerable<object> list)
        {
            foreach (var item in list)
            {
                if (item is Dictionary<string, object?> itemDict)
                {
                    result.Add(ProtocolVersionRecord.Load(itemDict, context));
                }
            }
        }

        return result;
    }


    /// <summary>
    /// Load a list of EnvironmentVariable from a dictionary or list.
    /// </summary>
    public static IList<EnvironmentVariable> LoadEnvironmentVariables(object data, LoadContext? context)
    {
        var result = new List<EnvironmentVariable>();

        if (data is Dictionary<string, object?> dict)
        {
            // Convert named dictionary to list
            foreach (var kvp in dict)
            {
                if (kvp.Value is Dictionary<string, object?> itemDict)
                {
                    // Value is an object, add name to it
                    itemDict["name"] = kvp.Key;
                    result.Add(EnvironmentVariable.Load(itemDict, context));
                }
                else
                {
                    // Value is a scalar, use it as the primary property
                    var newDict = new Dictionary<string, object?>
                    {
                        ["name"] = kvp.Key,
                        [""] = kvp.Value
                    };
                    result.Add(EnvironmentVariable.Load(newDict, context));
                }
            }
        }
        else if (data is IEnumerable<object> list)
        {
            foreach (var item in list)
            {
                if (item is Dictionary<string, object?> itemDict)
                {
                    result.Add(EnvironmentVariable.Load(itemDict, context));
                }
            }
        }

        return result;
    }



    #endregion

    #region Save Methods

    /// <summary>
    /// Save the ContainerAgent instance to a dictionary.
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

        if (obj.Protocols is not null)
        {
            result["protocols"] = SaveProtocols(obj.Protocols, context);
        }

        if (obj.Image is not null)
        {
            result["image"] = obj.Image;
        }

        if (obj.Resources is not null)
        {
            result["resources"] = obj.Resources?.Save(context);
        }

        if (obj.EnvironmentVariables is not null)
        {
            result["environmentVariables"] = SaveEnvironmentVariables(obj.EnvironmentVariables, context);
        }


        return result;
    }


    /// <summary>
    /// Save a list of ProtocolVersionRecord to object or array format.
    /// </summary>
    public static object SaveProtocols(IList<ProtocolVersionRecord> items, SaveContext? context)
    {
        context ??= new SaveContext();

        // This collection type does not have a 'name' property, only array format is supported
        return items.Select(item => item.Save(context)).ToList();

    }


    /// <summary>
    /// Save a list of EnvironmentVariable to object or array format.
    /// </summary>
    public static object SaveEnvironmentVariables(IList<EnvironmentVariable> items, SaveContext? context)
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
                if (context.UseShorthand && EnvironmentVariable.ShorthandProperty is string shorthandProp)
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
    /// Convert the ContainerAgent instance to a YAML string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The YAML string representation of this instance.</returns>
    public new string ToYaml(SaveContext? context = null)
    {
        context ??= new SaveContext();
        return context.ToYaml(Save(context));
    }

    /// <summary>
    /// Convert the ContainerAgent instance to a JSON string.
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
    /// Load a ContainerAgent instance from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ContainerAgent instance.</returns>
    public new static ContainerAgent FromJson(string json, LoadContext? context = null)
    {
        using var doc = JsonDocument.Parse(json);
        Dictionary<string, object?> dict;
        dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, JsonUtils.Options)
            ?? throw new ArgumentException("Failed to parse JSON as dictionary");

        return Load(dict, context);
    }

    /// <summary>
    /// Load a ContainerAgent instance from a YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ContainerAgent instance.</returns>
    public new static ContainerAgent FromYaml(string yaml, LoadContext? context = null)
    {
        var dict = YamlUtils.Deserializer.Deserialize<Dictionary<string, object?>>(yaml)
            ?? throw new ArgumentException("Failed to parse YAML as dictionary");

        return Load(dict, context);
    }

    #endregion
}
