// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class AgentDefinitionJsonConverter: JsonConverter<AgentDefinition>
{
    public override AgentDefinition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to AgentDefinition.");
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing AgentDefinition: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;
            
            // load polymorphic AgentDefinition instance
            AgentDefinition instance;
            if (rootElement.TryGetProperty("kind", out JsonElement discriminatorValue))
            {
                var discriminator = discriminatorValue.GetString() 
                    ?? throw new JsonException("Empty discriminator value for AgentDefinition is not supported");
                instance = discriminator switch 
                {
                    "prompt" => JsonSerializer.Deserialize<PromptAgent> (rootElement, options)
                        ?? throw new JsonException("Empty PromptAgent instances are not supported"),
                    "workflow" => JsonSerializer.Deserialize<Workflow> (rootElement, options)
                        ?? throw new JsonException("Empty Workflow instances are not supported"),
                    "hosted" => JsonSerializer.Deserialize<ContainerAgent> (rootElement, options)
                        ?? throw new JsonException("Empty ContainerAgent instances are not supported"),
                    _ => throw new JsonException($"Unknown AgentDefinition discriminator value: {discriminator}"),
                };
            }
            else
            {
                throw new JsonException("Missing AgentDefinition discriminator property: 'kind'");
            }
            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }
            
            if (rootElement.TryGetProperty("name", out JsonElement nameValue))
            {
                instance.Name = nameValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: name");
            }
            
            if (rootElement.TryGetProperty("displayName", out JsonElement displayNameValue))
            {
                instance.DisplayName = displayNameValue.GetString();
            }
            
            if (rootElement.TryGetProperty("description", out JsonElement descriptionValue))
            {
                instance.Description = descriptionValue.GetString();
            }
            
            if (rootElement.TryGetProperty("metadata", out JsonElement metadataValue))
            {
                instance.Metadata = JsonSerializer.Deserialize<Dictionary<string, object>>(metadataValue.GetRawText(), options);
            }
            
            if (rootElement.TryGetProperty("inputSchema", out JsonElement inputSchemaValue))
            {
                instance.InputSchema = JsonSerializer.Deserialize<PropertySchema?>(inputSchemaValue.GetRawText(), options);
            }
            
            if (rootElement.TryGetProperty("outputSchema", out JsonElement outputSchemaValue))
            {
                instance.OutputSchema = JsonSerializer.Deserialize<PropertySchema?>(outputSchemaValue.GetRawText(), options);
            }
            
            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, AgentDefinition value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);
        
        writer.WritePropertyName("name");
        JsonSerializer.Serialize(writer, value.Name, options);
        
        if(value.DisplayName != null)
        {
            writer.WritePropertyName("displayName");
            JsonSerializer.Serialize(writer, value.DisplayName, options);
        }
        
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
        
        if(value.InputSchema != null)
        {
            writer.WritePropertyName("inputSchema");
            JsonSerializer.Serialize(writer, value.InputSchema, options);
        }
        
        if(value.OutputSchema != null)
        {
            writer.WritePropertyName("outputSchema");
            JsonSerializer.Serialize(writer, value.OutputSchema, options);
        }
        
        writer.WriteEndObject();
    }
}