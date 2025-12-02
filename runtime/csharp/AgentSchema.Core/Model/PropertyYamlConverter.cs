// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class PropertyYamlConverter : YamlConverter<Property>
{
    /// <summary>
    /// Singleton instance of the Property converter.
    /// </summary>
    public static readonly PropertyYamlConverter Instance = new PropertyYamlConverter();

    public override Property Read(IParser parser, ObjectDeserializer rootDeserializer)
    {

        parser.Consume<MappingStart>();
        // load polymorphic Property instance
        Property instance;
        parser.TryFindMappingEntry((Scalar s) => s.Value == "kind", out var kindValue, out var kindParsingEvent);
        if (kindValue != null)
        {
            var discriminator = kindValue.Value
                ?? throw new YamlException("Empty discriminator value for Property is not supported");
            instance = discriminator.ToLowerInvariant() switch
            {
                "array" => rootDeserializer(typeof(ArrayProperty)) as ArrayProperty ??
                    throw new YamlException("Empty ArrayProperty instances are not supported"),
                "object" => rootDeserializer(typeof(ObjectProperty)) as ObjectProperty ??
                    throw new YamlException("Empty ObjectProperty instances are not supported"),
                _ => new Property(),
            };
        }
        else
        {
            throw new YamlException("Missing Property discriminator property: 'kind'");
        }

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "name":
                    var nameValue = parser.Consume<Scalar>();
                    instance.Name = nameValue.Value ?? throw new ArgumentException("Properties must contain a property named: name");
                    break;
                case "kind":
                    // discriminator property already processed
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "description":
                    var descriptionValue = parser.Consume<Scalar>();
                    instance.Description = descriptionValue.Value;
                    break;
                case "required":
                    var requiredValue = parser.Consume<Scalar>();
                    if (bool.TryParse(requiredValue.Value, out var requiredItem))
                    {
                        instance.Required = requiredItem;
                    }
                    break;
                case "default":
                    instance.Default = rootDeserializer(typeof(object));
                    break;
                case "example":
                    instance.Example = rootDeserializer(typeof(object));
                    break;
                case "enumValues":
                    /*
            instance.EnumValues = [.. enumValuesValue.EnumerateArray().Select(x => x.GetScalarValue() ?? throw new YamlException("Empty array elements for enumValues are not supported"))];
                    */
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Property.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Property value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("name"));
        serializer(value.Name, typeof(string));

        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));

        if (value.Description != null)
        {
            emitter.Emit(new Scalar("description"));
            serializer(value.Description, typeof(string));
        }

        if (value.Required != null)
        {
            emitter.Emit(new Scalar("required"));
            serializer(value.Required, typeof(bool));
        }

        if (value.Default != null)
        {
            emitter.Emit(new Scalar("default"));
            serializer(value.Default, typeof(object));
        }

        if (value.Example != null)
        {
            emitter.Emit(new Scalar("example"));
            serializer(value.Example, typeof(object));
        }

        if (value.EnumValues != null)
        {
            emitter.Emit(new Scalar("enumValues"));
            serializer(value.EnumValues, typeof(IList<object>));
        }

        emitter.Emit(new MappingEnd());
    }
}