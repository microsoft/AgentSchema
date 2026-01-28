// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ContainerAgentJsonConverter: JsonConverter<ContainerAgent>
{
    public override ContainerAgent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to ContainerAgent.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing ContainerAgent: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new ContainerAgent();
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("protocols", out JsonElement protocolsValue))
            {
                if (protocolsValue.ValueKind == JsonValueKind.Array)
                {
                    instance.Protocols = 
                        [.. protocolsValue.EnumerateArray()
                            .Select(x => JsonSerializer.Deserialize<ProtocolVersionRecord> (x.GetRawText(), options)
                                ?? throw new JsonException("Empty array elements for Protocols are not supported"))];
                }
                else
                {
                    throw new JsonException("Invalid JSON token for protocols");
                }
            }
            
            if (rootElement.TryGetProperty("environmentVariables", out JsonElement environmentVariablesValue))
            {
                if (environmentVariablesValue.ValueKind == JsonValueKind.Array)
                {
                    instance.EnvironmentVariables = 
                        [.. environmentVariablesValue.EnumerateArray()
                            .Select(x => JsonSerializer.Deserialize<EnvironmentVariable> (x.GetRawText(), options)
                                ?? throw new JsonException("Empty array elements for EnvironmentVariables are not supported"))];
                }
                else if (environmentVariablesValue.ValueKind == JsonValueKind.Object)
                {
                    instance.EnvironmentVariables = 
                        [.. environmentVariablesValue.EnumerateObject()
                            .Select(property =>
                            {
                                var item = JsonSerializer.Deserialize<EnvironmentVariable>(property.Value.GetRawText(), options)
                                    ?? throw new JsonException("Empty array elements for EnvironmentVariables are not supported");
                                item.Name = property.Name;
                                return item;
                            })];
                }
                
                else
                {
                    throw new JsonException("Invalid JSON token for environmentVariables");
                }
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, ContainerAgent value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("protocols");
        JsonSerializer.Serialize(writer, value.Protocols, options);
        
        if(value.EnvironmentVariables != null)
        {
            writer.WritePropertyName("environmentVariables");
            JsonSerializer.Serialize(writer, value.EnvironmentVariables, options);
        }
        
        writer.WriteEndObject();
    }
}