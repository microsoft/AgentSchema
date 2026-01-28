// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class PromptAgentJsonConverter: JsonConverter<PromptAgent>
{
    public override PromptAgent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to PromptAgent.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing PromptAgent: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new PromptAgent();
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("model", out JsonElement modelValue))
            {
                instance.Model = JsonSerializer.Deserialize<Model>(modelValue.GetRawText(), options) ?? throw new ArgumentException("Properties must contain a property named: model");
            }
            
            if (rootElement.TryGetProperty("tools", out JsonElement toolsValue))
            {
                if (toolsValue.ValueKind == JsonValueKind.Array)
                {
                    instance.Tools = 
                        [.. toolsValue.EnumerateArray()
                            .Select(x => JsonSerializer.Deserialize<Tool> (x.GetRawText(), options)
                                ?? throw new JsonException("Empty array elements for Tools are not supported"))];
                }
                else if (toolsValue.ValueKind == JsonValueKind.Object)
                {
                    instance.Tools = 
                        [.. toolsValue.EnumerateObject()
                            .Select(property =>
                            {
                                var item = JsonSerializer.Deserialize<Tool>(property.Value.GetRawText(), options)
                                    ?? throw new JsonException("Empty array elements for Tools are not supported");
                                item.Name = property.Name;
                                return item;
                            })];
                }
                
                else
                {
                    throw new JsonException("Invalid JSON token for tools");
                }
            }
            
            if (rootElement.TryGetProperty("template", out JsonElement templateValue))
            {
                instance.Template = JsonSerializer.Deserialize<Template?>(templateValue.GetRawText(), options);
            }
            
            if (rootElement.TryGetProperty("instructions", out JsonElement instructionsValue))
            {
                instance.Instructions = instructionsValue.GetString();
            }
            
            if (rootElement.TryGetProperty("additionalInstructions", out JsonElement additionalInstructionsValue))
            {
                instance.AdditionalInstructions = additionalInstructionsValue.GetString();
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, PromptAgent value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("model");
        JsonSerializer.Serialize(writer, value.Model, options);
        
        if(value.Tools != null)
        {
            writer.WritePropertyName("tools");
            JsonSerializer.Serialize(writer, value.Tools, options);
        }
        
        if(value.Template != null)
        {
            writer.WritePropertyName("template");
            JsonSerializer.Serialize(writer, value.Template, options);
        }
        
        if(value.Instructions != null)
        {
            writer.WritePropertyName("instructions");
            JsonSerializer.Serialize(writer, value.Instructions, options);
        }
        
        if(value.AdditionalInstructions != null)
        {
            writer.WritePropertyName("additionalInstructions");
            JsonSerializer.Serialize(writer, value.AdditionalInstructions, options);
        }
        
        writer.WriteEndObject();
    }
}