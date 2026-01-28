// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ModelOptionsJsonConverter: JsonConverter<ModelOptions>
{
    public override ModelOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to ModelOptions.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing ModelOptions: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new ModelOptions();
            if (rootElement.TryGetProperty("frequencyPenalty", out JsonElement frequencyPenaltyValue))
            {
                instance.FrequencyPenalty = frequencyPenaltyValue.GetSingle();
            }
            
            if (rootElement.TryGetProperty("maxOutputTokens", out JsonElement maxOutputTokensValue))
            {
                instance.MaxOutputTokens = maxOutputTokensValue.GetInt32();
            }
            
            if (rootElement.TryGetProperty("presencePenalty", out JsonElement presencePenaltyValue))
            {
                instance.PresencePenalty = presencePenaltyValue.GetSingle();
            }
            
            if (rootElement.TryGetProperty("seed", out JsonElement seedValue))
            {
                instance.Seed = seedValue.GetInt32();
            }
            
            if (rootElement.TryGetProperty("temperature", out JsonElement temperatureValue))
            {
                instance.Temperature = temperatureValue.GetSingle();
            }
            
            if (rootElement.TryGetProperty("topK", out JsonElement topKValue))
            {
                instance.TopK = topKValue.GetInt32();
            }
            
            if (rootElement.TryGetProperty("topP", out JsonElement topPValue))
            {
                instance.TopP = topPValue.GetSingle();
            }
            
            if (rootElement.TryGetProperty("stopSequences", out JsonElement stopSequencesValue))
            {
                instance.StopSequences = [.. stopSequencesValue.EnumerateArray().Select(x => x.GetString() ?? throw new JsonException("Empty array elements for stopSequences are not supported"))];
            }
            
            if (rootElement.TryGetProperty("allowMultipleToolCalls", out JsonElement allowMultipleToolCallsValue))
            {
                instance.AllowMultipleToolCalls = allowMultipleToolCallsValue.GetBoolean();
            }
            
            if (rootElement.TryGetProperty("additionalProperties", out JsonElement additionalPropertiesValue))
            {
                instance.AdditionalProperties = JsonSerializer.Deserialize<Dictionary<string, object>>(additionalPropertiesValue.GetRawText(), options);
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, ModelOptions value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        if(value.FrequencyPenalty != null)
        {
            writer.WritePropertyName("frequencyPenalty");
            JsonSerializer.Serialize(writer, value.FrequencyPenalty, options);
        }
        
        if(value.MaxOutputTokens != null)
        {
            writer.WritePropertyName("maxOutputTokens");
            JsonSerializer.Serialize(writer, value.MaxOutputTokens, options);
        }
        
        if(value.PresencePenalty != null)
        {
            writer.WritePropertyName("presencePenalty");
            JsonSerializer.Serialize(writer, value.PresencePenalty, options);
        }
        
        if(value.Seed != null)
        {
            writer.WritePropertyName("seed");
            JsonSerializer.Serialize(writer, value.Seed, options);
        }
        
        if(value.Temperature != null)
        {
            writer.WritePropertyName("temperature");
            JsonSerializer.Serialize(writer, value.Temperature, options);
        }
        
        if(value.TopK != null)
        {
            writer.WritePropertyName("topK");
            JsonSerializer.Serialize(writer, value.TopK, options);
        }
        
        if(value.TopP != null)
        {
            writer.WritePropertyName("topP");
            JsonSerializer.Serialize(writer, value.TopP, options);
        }
        
        if(value.StopSequences != null)
        {
            writer.WritePropertyName("stopSequences");
            JsonSerializer.Serialize(writer, value.StopSequences, options);
        }
        
        if(value.AllowMultipleToolCalls != null)
        {
            writer.WritePropertyName("allowMultipleToolCalls");
            JsonSerializer.Serialize(writer, value.AllowMultipleToolCalls, options);
        }
        
        if(value.AdditionalProperties != null)
        {
            writer.WritePropertyName("additionalProperties");
            JsonSerializer.Serialize(writer, value.AdditionalProperties, options);
        }
        
        writer.WriteEndObject();
    }
}