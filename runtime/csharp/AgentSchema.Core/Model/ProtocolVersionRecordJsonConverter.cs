// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ProtocolVersionRecordJsonConverter: JsonConverter<ProtocolVersionRecord>
{
    public override ProtocolVersionRecord Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to ProtocolVersionRecord.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing ProtocolVersionRecord: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new ProtocolVersionRecord();
            if (rootElement.TryGetProperty("protocol", out JsonElement protocolValue))
            {
                instance.Protocol = protocolValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: protocol");
            }
            
            if (rootElement.TryGetProperty("version", out JsonElement versionValue))
            {
                instance.Version = versionValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: version");
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, ProtocolVersionRecord value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("protocol");
        JsonSerializer.Serialize(writer, value.Protocol, options);
        
        writer.WritePropertyName("version");
        JsonSerializer.Serialize(writer, value.Version, options);
        
        writer.WriteEndObject();
    }
}