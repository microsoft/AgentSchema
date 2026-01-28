// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class McpToolJsonConverter: JsonConverter<McpTool>
{
    public override McpTool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to McpTool.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing McpTool: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new McpTool();
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("connection", out JsonElement connectionValue))
            {
                instance.Connection = JsonSerializer.Deserialize<Connection>(connectionValue.GetRawText(), options) ?? throw new ArgumentException("Properties must contain a property named: connection");
            }
            
            if (rootElement.TryGetProperty("serverName", out JsonElement serverNameValue))
            {
                instance.ServerName = serverNameValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: serverName");
            }
            
            if (rootElement.TryGetProperty("serverDescription", out JsonElement serverDescriptionValue))
            {
                instance.ServerDescription = serverDescriptionValue.GetString();
            }
            
            if (rootElement.TryGetProperty("approvalMode", out JsonElement approvalModeValue))
            {
                instance.ApprovalMode = JsonSerializer.Deserialize<McpServerApprovalMode>(approvalModeValue.GetRawText(), options) ?? throw new ArgumentException("Properties must contain a property named: approvalMode");
            }
            
            if (rootElement.TryGetProperty("allowedTools", out JsonElement allowedToolsValue))
            {
                instance.AllowedTools = [.. allowedToolsValue.EnumerateArray().Select(x => x.GetString() ?? throw new JsonException("Empty array elements for allowedTools are not supported"))];
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, McpTool value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("connection");
        JsonSerializer.Serialize(writer, value.Connection, options);
        
        writer.WritePropertyName("serverName");
        JsonSerializer.Serialize(writer, value.ServerName, options);
        
        if(value.ServerDescription != null)
        {
            writer.WritePropertyName("serverDescription");
            JsonSerializer.Serialize(writer, value.ServerDescription, options);
        }
        
        writer.WritePropertyName("approvalMode");
        JsonSerializer.Serialize(writer, value.ApprovalMode, options);
        
        if(value.AllowedTools != null)
        {
            writer.WritePropertyName("allowedTools");
            JsonSerializer.Serialize(writer, value.AllowedTools, options);
        }
        
        writer.WriteEndObject();
    }
}