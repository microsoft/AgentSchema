// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class AnonymousConnectionYamlConverter: YamlConverter<AnonymousConnection>
{
    /// <summary>
    /// Singleton instance of the AnonymousConnection converter.
    /// </summary>
    public static readonly AnonymousConnectionYamlConverter Instance = new AnonymousConnectionYamlConverter();

    public override AnonymousConnection Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new AnonymousConnection();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "endpoint":
                    var endpointValue = parser.Consume<Scalar>();
                    instance.Endpoint = endpointValue.Value ?? throw new ArgumentException("Properties must contain a property named: endpoint");
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in AnonymousConnection.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, AnonymousConnection value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("endpoint"));
        serializer(value.Endpoint, typeof(string));
        
        emitter.Emit(new MappingEnd());
    }
}