// Copyright (c) Microsoft. All rights reserved.
using System.Text.Json;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema;
#pragma warning restore IDE0130

/// <summary>
/// A single safety or governance policy attached to an agent.
/// The `type` field discriminates the policy kind; the remaining fields are
/// interpreted based on `type`. For `rai_policy`, `raiPolicyName` associates the
/// agent with a Responsible AI policy that governs content filtering and safety
/// guardrails for prompts and completions.
/// </summary>
public class Policy
{
    /// <summary>
    /// The shorthand property name for this type, if any.
    /// </summary>
    public static string? ShorthandProperty => null;

    /// <summary>
    /// Initializes a new instance of <see cref="Policy"/>.
    /// </summary>
#pragma warning disable CS8618
    public Policy()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// The policy type. 'rai_policy' attaches a Responsible AI (content safety) guardrail.
    /// </summary>
    public string Type { get; set; } = "rai_policy";

    /// <summary>
    /// For 'rai_policy', the full ARM resource ID of the Responsible AI policy on the Cognitive Services account (e.g., '/subscriptions/<sub>/resourceGroups/<rg>/providers/Microsoft.CognitiveServices/accounts/<account>/raiPolicies/<policyName>').
    /// </summary>
    public string? RaiPolicyName { get; set; }


    #region Load Methods

    /// <summary>
    /// Load a Policy instance from a dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the data.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded Policy instance.</returns>
    public static Policy Load(Dictionary<string, object?> data, LoadContext? context = null)
    {
        if (context is not null)
        {
            data = context.ProcessInput(data);
        }


        // Create new instance
        var instance = new Policy();


        if (data.TryGetValue("type", out var typeValue) && typeValue is not null)
        {
            instance.Type = typeValue?.ToString()!;
        }

        if (data.TryGetValue("raiPolicyName", out var raiPolicyNameValue) && raiPolicyNameValue is not null)
        {
            instance.RaiPolicyName = raiPolicyNameValue?.ToString()!;
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
    /// Save the Policy instance to a dictionary.
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


        if (obj.Type is not null)
        {
            result["type"] = obj.Type;
        }

        if (obj.RaiPolicyName is not null)
        {
            result["raiPolicyName"] = obj.RaiPolicyName;
        }


        if (context is not null)
        {
            result = context.ProcessDict(result);
        }

        return result;
    }


    /// <summary>
    /// Convert the Policy instance to a YAML string.
    /// </summary>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The YAML string representation of this instance.</returns>
    public string ToYaml(SaveContext? context = null)
    {
        context ??= new SaveContext();
        return context.ToYaml(Save(context));
    }

    /// <summary>
    /// Convert the Policy instance to a JSON string.
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
    /// Load a Policy instance from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded Policy instance.</returns>
    public static Policy FromJson(string json, LoadContext? context = null)
    {
        using var doc = JsonDocument.Parse(json);
        Dictionary<string, object?> dict;
        dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, JsonUtils.Options)
            ?? throw new ArgumentException("Failed to parse JSON as dictionary");

        return Load(dict, context);
    }

    /// <summary>
    /// Load a Policy instance from a YAML string.
    /// </summary>
    /// <param name="yaml">The YAML string to parse.</param>
    /// <param name="context">Optional context with pre/post processing callbacks.</param>
    /// <returns>The loaded Policy instance.</returns>
    public static Policy FromYaml(string yaml, LoadContext? context = null)
    {
        var dict = YamlUtils.Deserializer.Deserialize<Dictionary<string, object?>>(yaml)
            ?? throw new ArgumentException("Failed to parse YAML as dictionary");

        return Load(dict, context);
    }

    #endregion
}
