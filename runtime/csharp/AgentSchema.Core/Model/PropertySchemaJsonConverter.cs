// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class PropertySchemaJsonConverter: JsonConverter<PropertySchema>
{
    public override PropertySchema Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to PropertySchema.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing PropertySchema: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new PropertySchema();
            if (rootElement.TryGetProperty("examples", out JsonElement examplesValue))
            {
                instance.Examples = JsonSerializer.Deserialize<Dictionary<string, object>[]>(examplesValue.GetRawText(), options);
            }
            
            if (rootElement.TryGetProperty("strict", out JsonElement strictValue))
            {
                instance.Strict = strictValue.GetBoolean();
            }
            
            if (rootElement.TryGetProperty("properties", out JsonElement propertiesValue))
            {
                if (propertiesValue.ValueKind == JsonValueKind.Array)
                {
                    instance.Properties = 
                        [.. propertiesValue.EnumerateArray()
                            .Select(x => JsonSerializer.Deserialize<Property> (x.GetRawText(), options)
                                ?? throw new JsonException("Empty array elements for Properties are not supported"))];
                }
                else if (propertiesValue.ValueKind == JsonValueKind.Object)
                {
                    instance.Properties = 
                        [.. propertiesValue.EnumerateObject()
                            .Select(property =>
                            {
                                var item = JsonSerializer.Deserialize<Property>(property.Value.GetRawText(), options)
                                    ?? throw new JsonException("Empty array elements for Properties are not supported");
                                item.Name = property.Name;
                                return item;
                            })];
                }
                
                else
                {
                    throw new JsonException("Invalid JSON token for properties");
                }
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, PropertySchema value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        if(value.Examples != null)
        {
            writer.WritePropertyName("examples");
            JsonSerializer.Serialize(writer, value.Examples, options);
        }
        
        if(value.Strict != null)
        {
            writer.WritePropertyName("strict");
            JsonSerializer.Serialize(writer, value.Strict, options);
        }
        
        writer.WritePropertyName("properties");
        JsonSerializer.Serialize(writer, value.Properties, options);
        
        writer.WriteEndObject();
    }
}