// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ConnectionYamlConverter : YamlConverter<Connection>
{
    /// <summary>
    /// Singleton instance of the Connection converter.
    /// </summary>
    public static readonly ConnectionYamlConverter Instance = new ConnectionYamlConverter();

    public override Connection Read(IParser parser, ObjectDeserializer rootDeserializer)
    {

        parser.Consume<MappingStart>();
        // load polymorphic Connection instance
        Connection instance;
        parser.TryFindMappingEntry((Scalar s) => s.Value == "kind", out var kindValue, out var kindParsingEvent);
        if (kindValue != null)
        {
            var discriminator = kindValue.Value
                ?? throw new YamlException("Empty discriminator value for Connection is not supported");
            instance = discriminator.ToLowerInvariant() switch
            {
                "reference" => rootDeserializer(typeof(ReferenceConnection)) as ReferenceConnection ??
                    throw new YamlException("Empty ReferenceConnection instances are not supported"),
                "remote" => rootDeserializer(typeof(RemoteConnection)) as RemoteConnection ??
                    throw new YamlException("Empty RemoteConnection instances are not supported"),
                "key" => rootDeserializer(typeof(ApiKeyConnection)) as ApiKeyConnection ??
                    throw new YamlException("Empty ApiKeyConnection instances are not supported"),
                "anonymous" => rootDeserializer(typeof(AnonymousConnection)) as AnonymousConnection ??
                    throw new YamlException("Empty AnonymousConnection instances are not supported"),
                _ => throw new YamlException($"Unknown Connection discriminator value: {discriminator}"),
            };
        }
        else
        {
            throw new YamlException("Missing Connection discriminator property: 'kind'");
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
                case "authenticationMode":
                    var authenticationModeValue = parser.Consume<Scalar>();
                    instance.AuthenticationMode = authenticationModeValue.Value ?? throw new ArgumentException("Properties must contain a property named: authenticationMode");
                    break;
                case "usageDescription":
                    var usageDescriptionValue = parser.Consume<Scalar>();
                    instance.UsageDescription = usageDescriptionValue.Value;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Connection.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Connection value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));

        emitter.Emit(new Scalar("authenticationMode"));
        serializer(value.AuthenticationMode, typeof(string));

        if (value.UsageDescription != null)
        {
            emitter.Emit(new Scalar("usageDescription"));
            serializer(value.UsageDescription, typeof(string));
        }

        emitter.Emit(new MappingEnd());
    }
}