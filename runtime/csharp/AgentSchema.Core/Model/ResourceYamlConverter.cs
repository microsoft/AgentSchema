// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ResourceYamlConverter : YamlConverter<Resource>
{
    /// <summary>
    /// Singleton instance of the Resource converter.
    /// </summary>
    public static readonly ResourceYamlConverter Instance = new ResourceYamlConverter();

    public override Resource Read(IParser parser, ObjectDeserializer rootDeserializer)
    {

        parser.Consume<MappingStart>();
        // load polymorphic Resource instance
        Resource instance;
        parser.TryFindMappingEntry((Scalar s) => s.Value == "kind", out var kindValue, out var kindParsingEvent);
        if (kindValue != null)
        {
            var discriminator = kindValue.Value
                ?? throw new YamlException("Empty discriminator value for Resource is not supported");
            instance = discriminator.ToLowerInvariant() switch
            {
                "model" => rootDeserializer(typeof(ModelResource)) as ModelResource ??
                    throw new YamlException("Empty ModelResource instances are not supported"),
                "tool" => rootDeserializer(typeof(ToolResource)) as ToolResource ??
                    throw new YamlException("Empty ToolResource instances are not supported"),
                _ => throw new YamlException($"Unknown Resource discriminator value: {discriminator}"),
            };
        }
        else
        {
            throw new YamlException("Missing Resource discriminator property: 'kind'");
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
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Resource.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Resource value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("name"));
        serializer(value.Name, typeof(string));

        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));

        emitter.Emit(new MappingEnd());
    }
}