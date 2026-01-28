// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class WorkflowJsonConverter: JsonConverter<Workflow>
{
    public override Workflow Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to Workflow.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing Workflow: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new Workflow();
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("trigger", out JsonElement triggerValue))
            {
                instance.Trigger = JsonSerializer.Deserialize<Dictionary<string, object>>(triggerValue.GetRawText(), options);
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, Workflow value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        if(value.Trigger != null)
        {
            writer.WritePropertyName("trigger");
            JsonSerializer.Serialize(writer, value.Trigger, options);
        }
        
        writer.WriteEndObject();
    }
}