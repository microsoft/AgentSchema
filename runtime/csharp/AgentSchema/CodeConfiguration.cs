// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130

/// <summary>
/// Configuration for code-based (ZIP upload) deployment of a hosted agent.
/// When present, the agent source code is uploaded directly instead of building a container image.
/// </summary>
public class CodeConfiguration
{
    /// <summary>
    /// The shorthand property name for this type, if any.
    /// </summary>
    public static string? ShorthandProperty => null;

    /// <summary>
    /// Initializes a new instance of <see cref="CodeConfiguration"/>.
    /// </summary>
#pragma warning disable CS8618
    public CodeConfiguration()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Runtime identifier for code execution (e.g., 'python_3_11', 'dotnet_8').
    /// </summary>
    public string Runtime { get; set; } = string.Empty;

    /// <summary>
    /// The entry point file for the agent (e.g., 'main.py' for Python, 'HelloWorld.dll' for .NET).
    /// </summary>
    public string EntryPoint { get; set; } = string.Empty;

    /// <summary>
    /// How package dependencies are resolved at deployment time.
    /// </summary>
    public string? DependencyResolution { get; set; }


    #region Load Methods

    /// <summary>
    /// Load a CodeConfiguration instance from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the data.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded CodeConfiguration instance.</returns>
    public static CodeConfiguration Load(Dictionary<string, object?> data, LoadContext? context = null)
    {
        if (context is not null)
        {
            data = context.ProcessInput(data);
        }


        // Create new instance
        var instance = new CodeConfiguration();


        if (data.TryGetValue("runtime", out var runtimeValue) && runtimeValue is not null)
        {
            instance.Runtime = runtimeValue?.ToString()!;
        }

        if (data.TryGetValue("entryPoint", out var entryPointValue) && entryPointValue is not null)
        {
            instance.EntryPoint = entryPointValue?.ToString()!;
        }

        if (data.TryGetValue("dependencyResolution", out var dependencyResolutionValue) && dependencyResolutionValue is not null)
        {
            instance.DependencyResolution = dependencyResolutionValue?.ToString()!;
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
    /// Save the CodeConfiguration instance to a dictionary.
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


        if (obj.Runtime is not null)
        {
            result["runtime"] = obj.Runtime;
        }

        if (obj.EntryPoint is not null)
        {
            result["entryPoint"] = obj.EntryPoint;
        }

        if (obj.DependencyResolution is not null)
        {
            result["dependencyResolution"] = obj.DependencyResolution;
        }


        if (context is not null)
        {
            result = context.ProcessDict(result);
        }

        return result;
    }


    /// <summary>
    /// Convert the CodeConfiguration instance to a YAML string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The YAML string representation of this instance.</returns>
    public string ToYaml(SaveContext? context = null)
    {
        context ??= new SaveContext();
        return context.ToYaml(Save(context));
    }

    /// <summary>
    /// Convert the CodeConfiguration instance to a JSON string.
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
    /// Load a CodeConfiguration instance from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded CodeConfiguration instance.</returns>
    public static CodeConfiguration FromJson(string json, LoadContext? context = null)
    {
        using var doc = JsonDocument.Parse(json);
        Dictionary<string, object?> dict;
        dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, JsonUtils.Options)
            ?? throw new ArgumentException("Failed to parse JSON as dictionary");

        return Load(dict, context);
    }

    /// <summary>
    /// Load a CodeConfiguration instance from a YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded CodeConfiguration instance.</returns>
    public static CodeConfiguration FromYaml(string yaml, LoadContext? context = null)
    {
        var dict = YamlUtils.Deserializer.Deserialize<Dictionary<string, object?>>(yaml)
            ?? throw new ArgumentException("Failed to parse YAML as dictionary");

        return Load(dict, context);
    }

    #endregion
}
