// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130

/// <summary>
/// 
/// </summary>
public class ProtocolVersionRecord
{
    /// <summary>
    /// The shorthand property name for this type, if any.
    /// </summary>
    public static string? ShorthandProperty => null;

    /// <summary>
    /// Initializes a new instance of <see cref="ProtocolVersionRecord"/>.
    /// </summary>
#pragma warning disable CS8618
    public ProtocolVersionRecord()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// The protocol type.
    /// </summary>
    public string Protocol { get; set; } = string.Empty;

    /// <summary>
    /// The version string for the protocol, e.g. 'v0.1.1'.
    /// </summary>
    public string Version { get; set; } = string.Empty;


    #region Load Methods

    /// <summary>
    /// Load a ProtocolVersionRecord instance from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the data.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ProtocolVersionRecord instance.</returns>
    public static ProtocolVersionRecord Load(Dictionary<string, object?> data, LoadContext? context = null)
    {
        if (context is not null)
        {
            data = context.ProcessInput(data);
        }


        // Create new instance
        var instance = new ProtocolVersionRecord();


        if (data.TryGetValue("protocol", out var protocolValue) && protocolValue is not null)
        {
            instance.Protocol = protocolValue?.ToString()!;
        }

        if (data.TryGetValue("version", out var versionValue) && versionValue is not null)
        {
            instance.Version = versionValue?.ToString()!;
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
    /// Save the ProtocolVersionRecord instance to a dictionary.
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


        if (obj.Protocol is not null)
        {
            result["protocol"] = obj.Protocol;
        }

        if (obj.Version is not null)
        {
            result["version"] = obj.Version;
        }


        if (context is not null)
        {
            result = context.ProcessDict(result);
        }

        return result;
    }


    /// <summary>
    /// Convert the ProtocolVersionRecord instance to a YAML string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The YAML string representation of this instance.</returns>
    public string ToYaml(SaveContext? context = null)
    {
        context ??= new SaveContext();
        return context.ToYaml(Save(context));
    }

    /// <summary>
    /// Convert the ProtocolVersionRecord instance to a JSON string.
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
    /// Load a ProtocolVersionRecord instance from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ProtocolVersionRecord instance.</returns>
    public static ProtocolVersionRecord FromJson(string json, LoadContext? context = null)
    {
        using var doc = JsonDocument.Parse(json);
        Dictionary<string, object?> dict;
        dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, JsonUtils.Options)
            ?? throw new ArgumentException("Failed to parse JSON as dictionary");

        return Load(dict, context);
    }

    /// <summary>
    /// Load a ProtocolVersionRecord instance from a YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded ProtocolVersionRecord instance.</returns>
    public static ProtocolVersionRecord FromYaml(string yaml, LoadContext? context = null)
    {
        var dict = YamlUtils.Deserializer.Deserialize<Dictionary<string, object?>>(yaml)
            ?? throw new ArgumentException("Failed to parse YAML as dictionary");

        return Load(dict, context);
    }

    #endregion
}
