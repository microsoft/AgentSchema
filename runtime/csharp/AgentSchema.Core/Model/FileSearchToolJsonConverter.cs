// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class FileSearchToolJsonConverter: JsonConverter<FileSearchTool>
{
    public override FileSearchTool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to FileSearchTool.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing FileSearchTool: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new FileSearchTool();
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("connection", out JsonElement connectionValue))
            {
                instance.Connection = JsonSerializer.Deserialize<Connection>(connectionValue.GetRawText(), options) ?? throw new ArgumentException("Properties must contain a property named: connection");
            }
            
            if (rootElement.TryGetProperty("vectorStoreIds", out JsonElement vectorStoreIdsValue))
            {
                instance.VectorStoreIds = [.. vectorStoreIdsValue.EnumerateArray().Select(x => x.GetString() ?? throw new JsonException("Empty array elements for vectorStoreIds are not supported"))];
            }
            
            if (rootElement.TryGetProperty("maximumResultCount", out JsonElement maximumResultCountValue))
            {
                instance.MaximumResultCount = maximumResultCountValue.GetInt32();
            }
            
            if (rootElement.TryGetProperty("ranker", out JsonElement rankerValue))
            {
                instance.Ranker = rankerValue.GetString();
            }
            
            if (rootElement.TryGetProperty("scoreThreshold", out JsonElement scoreThresholdValue))
            {
                instance.ScoreThreshold = scoreThresholdValue.GetSingle();
            }
            
            if (rootElement.TryGetProperty("filters", out JsonElement filtersValue))
            {
                instance.Filters = JsonSerializer.Deserialize<Dictionary<string, object>>(filtersValue.GetRawText(), options);
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, FileSearchTool value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("connection");
        JsonSerializer.Serialize(writer, value.Connection, options);
        
        writer.WritePropertyName("vectorStoreIds");
        JsonSerializer.Serialize(writer, value.VectorStoreIds, options);
        
        if(value.MaximumResultCount != null)
        {
            writer.WritePropertyName("maximumResultCount");
            JsonSerializer.Serialize(writer, value.MaximumResultCount, options);
        }
        
        if(value.Ranker != null)
        {
            writer.WritePropertyName("ranker");
            JsonSerializer.Serialize(writer, value.Ranker, options);
        }
        
        if(value.ScoreThreshold != null)
        {
            writer.WritePropertyName("scoreThreshold");
            JsonSerializer.Serialize(writer, value.ScoreThreshold, options);
        }
        
        if(value.Filters != null)
        {
            writer.WritePropertyName("filters");
            JsonSerializer.Serialize(writer, value.Filters, options);
        }
        
        writer.WriteEndObject();
    }
}