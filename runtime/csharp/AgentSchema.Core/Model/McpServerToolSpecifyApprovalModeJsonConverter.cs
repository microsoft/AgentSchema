// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class McpServerToolSpecifyApprovalModeJsonConverter: JsonConverter<McpServerToolSpecifyApprovalMode>
{
    public override McpServerToolSpecifyApprovalMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to McpServerToolSpecifyApprovalMode.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing McpServerToolSpecifyApprovalMode: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new McpServerToolSpecifyApprovalMode();
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("alwaysRequireApprovalTools", out JsonElement alwaysRequireApprovalToolsValue))
            {
                instance.AlwaysRequireApprovalTools = [.. alwaysRequireApprovalToolsValue.EnumerateArray().Select(x => x.GetString() ?? throw new JsonException("Empty array elements for alwaysRequireApprovalTools are not supported"))];
            }
            
            if (rootElement.TryGetProperty("neverRequireApprovalTools", out JsonElement neverRequireApprovalToolsValue))
            {
                instance.NeverRequireApprovalTools = [.. neverRequireApprovalToolsValue.EnumerateArray().Select(x => x.GetString() ?? throw new JsonException("Empty array elements for neverRequireApprovalTools are not supported"))];
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, McpServerToolSpecifyApprovalMode value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("alwaysRequireApprovalTools");
        JsonSerializer.Serialize(writer, value.AlwaysRequireApprovalTools, options);
        
        writer.WritePropertyName("neverRequireApprovalTools");
        JsonSerializer.Serialize(writer, value.NeverRequireApprovalTools, options);
        
        writer.WriteEndObject();
    }
}