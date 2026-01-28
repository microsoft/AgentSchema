// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class AgentManifestJsonConverter: JsonConverter<AgentManifest>
{
    public override AgentManifest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to AgentManifest.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing AgentManifest: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // create new instance
            var instance = new AgentManifest();
            if (rootElement.TryGetProperty("name", out JsonElement nameValue))
            {
                instance.Name = nameValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: name");
            }
            
            if (rootElement.TryGetProperty("displayName", out JsonElement displayNameValue))
            {
                instance.DisplayName = displayNameValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: displayName");
            }
            
            if (rootElement.TryGetProperty("description", out JsonElement descriptionValue))
            {
                instance.Description = descriptionValue.GetString();
            }
            
            if (rootElement.TryGetProperty("metadata", out JsonElement metadataValue))
            {
                instance.Metadata = JsonSerializer.Deserialize<Dictionary<string, object>>(metadataValue.GetRawText(), options);
            }
            
            if (rootElement.TryGetProperty("template", out JsonElement templateValue))
            {
                instance.Template = JsonSerializer.Deserialize<AgentDefinition>(templateValue.GetRawText(), options) ?? throw new ArgumentException("Properties must contain a property named: template");
            }
            
            if (rootElement.TryGetProperty("parameters", out JsonElement parametersValue))
            {
                instance.Parameters = JsonSerializer.Deserialize<PropertySchema>(parametersValue.GetRawText(), options) ?? throw new ArgumentException("Properties must contain a property named: parameters");
            }
            
            if (rootElement.TryGetProperty("resources", out JsonElement resourcesValue))
            {
                if (resourcesValue.ValueKind == JsonValueKind.Array)
                {
                    instance.Resources = 
                        [.. resourcesValue.EnumerateArray()
                            .Select(x => JsonSerializer.Deserialize<Resource> (x.GetRawText(), options)
                                ?? throw new JsonException("Empty array elements for Resources are not supported"))];
                }
                else if (resourcesValue.ValueKind == JsonValueKind.Object)
                {
                    instance.Resources = 
                        [.. resourcesValue.EnumerateObject()
                            .Select(property =>
                            {
                                var item = JsonSerializer.Deserialize<Resource>(property.Value.GetRawText(), options)
                                    ?? throw new JsonException("Empty array elements for Resources are not supported");
                                item.Name = property.Name;
                                return item;
                            })];
                }
                
                else
                {
                    throw new JsonException("Invalid JSON token for resources");
                }
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, AgentManifest value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("name");
        JsonSerializer.Serialize(writer, value.Name, options);
        
        writer.WritePropertyName("displayName");
        JsonSerializer.Serialize(writer, value.DisplayName, options);
        
        if(value.Description != null)
        {
            writer.WritePropertyName("description");
            JsonSerializer.Serialize(writer, value.Description, options);
        }
        
        if(value.Metadata != null)
        {
            writer.WritePropertyName("metadata");
            JsonSerializer.Serialize(writer, value.Metadata, options);
        }
        
        writer.WritePropertyName("template");
        JsonSerializer.Serialize(writer, value.Template, options);
        
        writer.WritePropertyName("parameters");
        JsonSerializer.Serialize(writer, value.Parameters, options);
        
        writer.WritePropertyName("resources");
        JsonSerializer.Serialize(writer, value.Resources, options);
        
        writer.WriteEndObject();
    }
}