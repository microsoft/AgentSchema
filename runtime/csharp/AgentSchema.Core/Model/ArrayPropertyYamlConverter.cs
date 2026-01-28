// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ArrayPropertyYamlConverter: YamlConverter<ArrayProperty>
{
    /// <summary>
    /// Singleton instance of the ArrayProperty converter.
    /// </summary>
    public static readonly ArrayPropertyYamlConverter Instance = new ArrayPropertyYamlConverter();

    public override ArrayProperty Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new ArrayProperty();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "items":
                    var itemsValue = rootDeserializer(typeof(Property)) as Property ?? throw new ArgumentException("Properties must contain a property named: items");
                    instance.Items = itemsValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in ArrayProperty.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, ArrayProperty value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("items"));
        serializer(value.Items, typeof(Property));
        
        emitter.Emit(new MappingEnd());
    }
}