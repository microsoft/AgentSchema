// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ApiKeyConnectionJsonConverter: JsonConverter<ApiKeyConnection>
{
    public override ApiKeyConnection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to ApiKeyConnection.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing ApiKeyConnection: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new ApiKeyConnection();
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("endpoint", out JsonElement endpointValue))
            {
                instance.Endpoint = endpointValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: endpoint");
            }
            
            if (rootElement.TryGetProperty("apiKey", out JsonElement apiKeyValue))
            {
                instance.ApiKey = apiKeyValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: apiKey");
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, ApiKeyConnection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("endpoint");
        JsonSerializer.Serialize(writer, value.Endpoint, options);
        
        writer.WritePropertyName("apiKey");
        JsonSerializer.Serialize(writer, value.ApiKey, options);
        
        writer.WriteEndObject();
    }
}