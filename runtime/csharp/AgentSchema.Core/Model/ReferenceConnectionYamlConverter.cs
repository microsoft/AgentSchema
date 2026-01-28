// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ReferenceConnectionYamlConverter: YamlConverter<ReferenceConnection>
{
    /// <summary>
    /// Singleton instance of the ReferenceConnection converter.
    /// </summary>
    public static readonly ReferenceConnectionYamlConverter Instance = new ReferenceConnectionYamlConverter();

    public override ReferenceConnection Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new ReferenceConnection();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "name":
                    var nameValue = parser.Consume<Scalar>();
                    instance.Name = nameValue.Value ?? throw new ArgumentException("Properties must contain a property named: name");
                    break;
                case "target":
                    var targetValue = parser.Consume<Scalar>();
                    instance.Target = targetValue.Value;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in ReferenceConnection.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, ReferenceConnection value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("name"));
        serializer(value.Name, typeof(string));
        
        if(value.Target != null)
        {
            emitter.Emit(new Scalar("target"));
            serializer(value.Target, typeof(string));
        }
        
        emitter.Emit(new MappingEnd());
    }
}