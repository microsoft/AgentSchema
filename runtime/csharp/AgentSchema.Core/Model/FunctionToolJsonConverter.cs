// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class FunctionToolJsonConverter: JsonConverter<FunctionTool>
{
    public override FunctionTool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to FunctionTool.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing FunctionTool: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new FunctionTool();
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("parameters", out JsonElement parametersValue))
            {
                instance.Parameters = JsonSerializer.Deserialize<PropertySchema>(parametersValue.GetRawText(), options) ?? throw new ArgumentException("Properties must contain a property named: parameters");
            }
            
            if (rootElement.TryGetProperty("strict", out JsonElement strictValue))
            {
                instance.Strict = strictValue.GetBoolean();
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, FunctionTool value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("parameters");
        JsonSerializer.Serialize(writer, value.Parameters, options);
        
        if(value.Strict != null)
        {
            writer.WritePropertyName("strict");
            JsonSerializer.Serialize(writer, value.Strict, options);
        }
        
        writer.WriteEndObject();
    }
}