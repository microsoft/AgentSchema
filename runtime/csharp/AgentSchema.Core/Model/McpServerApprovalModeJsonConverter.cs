// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class McpServerApprovalModeJsonConverter : JsonConverter<McpServerApprovalMode>
{
    public override McpServerApprovalMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to McpServerApprovalMode.");
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString() ?? throw new JsonException("Empty string shorthand values for McpServerApprovalMode are not supported");
            // load polymorphic McpServerApprovalMode instance
            McpServerApprovalMode instance;
            instance = stringValue.ToLowerInvariant() switch
            {
                "always" => new McpServerToolAlwaysRequireApprovalMode(),
                "never" => new McpServerToolNeverRequireApprovalMode(),
                "specify" => new McpServerToolSpecifyApprovalMode(),
                _ => throw new JsonException($"Unknown McpServerApprovalMode discriminator value: {stringValue}"),
            };
            instance.Kind = stringValue;
            return instance;

        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing McpServerApprovalMode: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;

            // load polymorphic McpServerApprovalMode instance
            McpServerApprovalMode instance;
            if (rootElement.TryGetProperty("kind", out JsonElement discriminatorValue))
            {
                var discriminator = discriminatorValue.GetString()
                    ?? throw new JsonException("Empty discriminator value for McpServerApprovalMode is not supported");
                instance = discriminator switch
                {
                    "always" => JsonSerializer.Deserialize<McpServerToolAlwaysRequireApprovalMode>(rootElement, options)
                        ?? throw new JsonException("Empty McpServerToolAlwaysRequireApprovalMode instances are not supported"),
                    "never" => JsonSerializer.Deserialize<McpServerToolNeverRequireApprovalMode>(rootElement, options)
                        ?? throw new JsonException("Empty McpServerToolNeverRequireApprovalMode instances are not supported"),
                    "specify" => JsonSerializer.Deserialize<McpServerToolSpecifyApprovalMode>(rootElement, options)
                        ?? throw new JsonException("Empty McpServerToolSpecifyApprovalMode instances are not supported"),
                    _ => throw new JsonException($"Unknown McpServerApprovalMode discriminator value: {discriminator}"),
                };
            }
            else
            {
                throw new JsonException("Missing McpServerApprovalMode discriminator property: 'kind'");
            }
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }

            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, McpServerApprovalMode value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);

        writer.WriteEndObject();
    }
}