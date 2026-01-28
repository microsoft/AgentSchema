// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ConnectionJsonConverter: JsonConverter<Connection>
{
    public override Connection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to Connection.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing Connection: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // load polymorphic Connection instance
            Connection instance;
            if (rootElement.TryGetProperty("kind", out JsonElement discriminatorValue))
            {
                var discriminator = discriminatorValue.GetString() 
                    ?? throw new JsonException("Empty discriminator value for Connection is not supported");
                instance = discriminator switch 
                {
                    "reference" => JsonSerializer.Deserialize<ReferenceConnection> (rootElement, options)
                        ?? throw new JsonException("Empty ReferenceConnection instances are not supported"),
                    "remote" => JsonSerializer.Deserialize<RemoteConnection> (rootElement, options)
                        ?? throw new JsonException("Empty RemoteConnection instances are not supported"),
                    "key" => JsonSerializer.Deserialize<ApiKeyConnection> (rootElement, options)
                        ?? throw new JsonException("Empty ApiKeyConnection instances are not supported"),
                    "anonymous" => JsonSerializer.Deserialize<AnonymousConnection> (rootElement, options)
                        ?? throw new JsonException("Empty AnonymousConnection instances are not supported"),
                    _ => throw new JsonException($"Unknown Connection discriminator value: {discriminator}"),
                };
            }
            else
            {
                throw new JsonException("Missing Connection discriminator property: 'kind'");
            }
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("authenticationMode", out JsonElement authenticationModeValue))
            {
                instance.AuthenticationMode = authenticationModeValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: authenticationMode");
            }
            
            if (rootElement.TryGetProperty("usageDescription", out JsonElement usageDescriptionValue))
            {
                instance.UsageDescription = usageDescriptionValue.GetString();
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, Connection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("authenticationMode");
        JsonSerializer.Serialize(writer, value.AuthenticationMode, options);
        
        if(value.UsageDescription != null)
        {
            writer.WritePropertyName("usageDescription");
            JsonSerializer.Serialize(writer, value.UsageDescription, options);
        }
        
        writer.WriteEndObject();
    }
}