// Copyright (c) Microsoft. All rights reserved.
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class PropertyJsonConverter : JsonConverter<Property>
{
    public override Property Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Cannot convert null value to Property.");
        }
        else if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False)
        {
            var boolValue = reader.GetBoolean();
            return new Property()
            {
                Kind = "boolean",
                Example = boolValue,
            };
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString() ?? throw new JsonException("Empty string shorthand values for Property are not supported");
            // load polymorphic Property instance
            Property instance;
            instance = stringValue.ToLowerInvariant() switch
            {
                "array" => new ArrayProperty(),
                "object" => new ObjectProperty(),
                _ => new Property(),
            };
            instance.Kind = "string";
            instance.Example = stringValue;
            return instance;

        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            byte[] span = reader.HasValueSequence ?
                        reader.ValueSequence.ToArray() :
                        reader.ValueSpan.ToArray();

            var numberString = System.Text.Encoding.UTF8.GetString(span);
            if (numberString.Contains('.') || numberString.Contains('e') || numberString.Contains('E'))
            {
                // try parse as float
                if (float.TryParse(numberString, out float floatValue))
                {
                    return new Property()
                    {
                        Kind = "float",
                        Example = floatValue,
                    };
                }
            }
            else
            {
                // try parse as int
                if (int.TryParse(numberString, out int intValue))
                {
                    return new Property()
                    {
                        Kind = "integer",
                        Example = intValue,
                    };
                }
            }
        }
        else if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Unexpected JSON token when parsing Property: {reader.TokenType}");
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;

            // load polymorphic Property instance
            Property instance;
            if (rootElement.TryGetProperty("kind", out JsonElement discriminatorValue))
            {
                var discriminator = discriminatorValue.GetString()
                    ?? throw new JsonException("Empty discriminator value for Property is not supported");
                instance = discriminator switch
                {
                    "array" => JsonSerializer.Deserialize<ArrayProperty>(rootElement, options)
                        ?? throw new JsonException("Empty ArrayProperty instances are not supported"),
                    "object" => JsonSerializer.Deserialize<ObjectProperty>(rootElement, options)
                        ?? throw new JsonException("Empty ObjectProperty instances are not supported"),
                    _ => new Property(),
                };
            }
            else
            {
                throw new JsonException("Missing Property discriminator property: 'kind'");
            }
            if (rootElement.TryGetProperty("name", out JsonElement nameValue))
            {
                instance.Name = nameValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: name");
            }

            if (rootElement.TryGetProperty("kind", out JsonElement kindValue))
            {
                instance.Kind = kindValue.GetString() ?? throw new ArgumentException("Properties must contain a property named: kind");
            }

            if (rootElement.TryGetProperty("description", out JsonElement descriptionValue))
            {
                instance.Description = descriptionValue.GetString();
            }

            if (rootElement.TryGetProperty("required", out JsonElement requiredValue))
            {
                instance.Required = requiredValue.GetBoolean();
            }

            if (rootElement.TryGetProperty("default", out JsonElement defaultValue))
            {
                instance.Default = defaultValue.GetScalarValue();
            }

            if (rootElement.TryGetProperty("example", out JsonElement exampleValue))
            {
                instance.Example = exampleValue.GetScalarValue();
            }

            if (rootElement.TryGetProperty("enumValues", out JsonElement enumValuesValue))
            {
                instance.EnumValues = [.. enumValuesValue.EnumerateArray().Select(x => x.GetScalarValue() ?? throw new JsonException("Empty array elements for enumValues are not supported"))];
            }

            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, Property value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("name");
        JsonSerializer.Serialize(writer, value.Name, options);

        writer.WritePropertyName("kind");
        JsonSerializer.Serialize(writer, value.Kind, options);

        if (value.Description != null)
        {
            writer.WritePropertyName("description");
            JsonSerializer.Serialize(writer, value.Description, options);
        }

        if (value.Required != null)
        {
            writer.WritePropertyName("required");
            JsonSerializer.Serialize(writer, value.Required, options);
        }

        if (value.Default != null)
        {
            writer.WritePropertyName("default");
            JsonSerializer.Serialize(writer, value.Default, options);
        }

        if (value.Example != null)
        {
            writer.WritePropertyName("example");
            JsonSerializer.Serialize(writer, value.Example, options);
        }

        if (value.EnumValues != null)
        {
            writer.WritePropertyName("enumValues");
            JsonSerializer.Serialize(writer, value.EnumValues, options);
        }

        writer.WriteEndObject();
    }
}