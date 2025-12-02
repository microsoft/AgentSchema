// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class AgentDefinitionYamlConverter : YamlConverter<AgentDefinition>
{
    /// <summary>
    /// Singleton instance of the AgentDefinition converter.
    /// </summary>
    public static readonly AgentDefinitionYamlConverter Instance = new AgentDefinitionYamlConverter();

    public override AgentDefinition Read(IParser parser, ObjectDeserializer rootDeserializer)
    {

        parser.Consume<MappingStart>();
        // load polymorphic AgentDefinition instance
        AgentDefinition instance;
        parser.TryFindMappingEntry((Scalar s) => s.Value == "kind", out var kindValue, out var kindParsingEvent);
        if (kindValue != null)
        {
            var discriminator = kindValue.Value
                ?? throw new YamlException("Empty discriminator value for AgentDefinition is not supported");
            instance = discriminator.ToLowerInvariant() switch
            {
                "prompt" => rootDeserializer(typeof(PromptAgent)) as PromptAgent ??
                    throw new YamlException("Empty PromptAgent instances are not supported"),
                "workflow" => rootDeserializer(typeof(Workflow)) as Workflow ??
                    throw new YamlException("Empty Workflow instances are not supported"),
                "hosted" => rootDeserializer(typeof(ContainerAgent)) as ContainerAgent ??
                    throw new YamlException("Empty ContainerAgent instances are not supported"),
                _ => throw new YamlException($"Unknown AgentDefinition discriminator value: {discriminator}"),
            };
        }
        else
        {
            throw new YamlException("Missing AgentDefinition discriminator property: 'kind'");
        }

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    // discriminator property already processed
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "name":
                    var nameValue = parser.Consume<Scalar>();
                    instance.Name = nameValue.Value ?? throw new ArgumentException("Properties must contain a property named: name");
                    break;
                case "displayName":
                    var displayNameValue = parser.Consume<Scalar>();
                    instance.DisplayName = displayNameValue.Value;
                    break;
                case "description":
                    var descriptionValue = parser.Consume<Scalar>();
                    instance.Description = descriptionValue.Value;
                    break;
                case "metadata":
                    var metadataValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                    instance.Metadata = metadataValue;
                    break;
                case "inputSchema":
                    var inputSchemaValue = rootDeserializer(typeof(PropertySchema)) as PropertySchema;
                    instance.InputSchema = inputSchemaValue;
                    break;
                case "outputSchema":
                    var outputSchemaValue = rootDeserializer(typeof(PropertySchema)) as PropertySchema;
                    instance.OutputSchema = outputSchemaValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in AgentDefinition.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, AgentDefinition value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));

        emitter.Emit(new Scalar("name"));
        serializer(value.Name, typeof(string));

        if (value.DisplayName != null)
        {
            emitter.Emit(new Scalar("displayName"));
            serializer(value.DisplayName, typeof(string));
        }

        if (value.Description != null)
        {
            emitter.Emit(new Scalar("description"));
            serializer(value.Description, typeof(string));
        }

        if (value.Metadata != null)
        {
            emitter.Emit(new Scalar("metadata"));
            serializer(value.Metadata, typeof(IDictionary<string, object>));
        }

        if (value.InputSchema != null)
        {
            emitter.Emit(new Scalar("inputSchema"));
            serializer(value.InputSchema, typeof(PropertySchema));
        }

        if (value.OutputSchema != null)
        {
            emitter.Emit(new Scalar("outputSchema"));
            serializer(value.OutputSchema, typeof(PropertySchema));
        }

        emitter.Emit(new MappingEnd());
    }
}