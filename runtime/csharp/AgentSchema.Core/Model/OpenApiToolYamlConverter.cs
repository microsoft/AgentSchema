// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class OpenApiToolYamlConverter: YamlConverter<OpenApiTool>
{
    /// <summary>
    /// Singleton instance of the OpenApiTool converter.
    /// </summary>
    public static readonly OpenApiToolYamlConverter Instance = new OpenApiToolYamlConverter();

    public override OpenApiTool Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new OpenApiTool();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "connection":
                    var connectionValue = rootDeserializer(typeof(Connection)) as Connection ?? throw new ArgumentException("Properties must contain a property named: connection");
                    instance.Connection = connectionValue;
                    break;
                case "specification":
                    var specificationValue = parser.Consume<Scalar>();
                    instance.Specification = specificationValue.Value ?? throw new ArgumentException("Properties must contain a property named: specification");
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in OpenApiTool.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, OpenApiTool value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("connection"));
        serializer(value.Connection, typeof(Connection));
        
        emitter.Emit(new Scalar("specification"));
        serializer(value.Specification, typeof(string));
        
        emitter.Emit(new MappingEnd());
    }
}