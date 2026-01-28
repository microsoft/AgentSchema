// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ProtocolVersionRecordYamlConverter: YamlConverter<ProtocolVersionRecord>
{
    /// <summary>
    /// Singleton instance of the ProtocolVersionRecord converter.
    /// </summary>
    public static readonly ProtocolVersionRecordYamlConverter Instance = new ProtocolVersionRecordYamlConverter();

    public override ProtocolVersionRecord Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new ProtocolVersionRecord();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "protocol":
                    var protocolValue = parser.Consume<Scalar>();
                    instance.Protocol = protocolValue.Value ?? throw new ArgumentException("Properties must contain a property named: protocol");
                    break;
                case "version":
                    var versionValue = parser.Consume<Scalar>();
                    instance.Version = versionValue.Value ?? throw new ArgumentException("Properties must contain a property named: version");
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in ProtocolVersionRecord.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, ProtocolVersionRecord value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("protocol"));
        serializer(value.Protocol, typeof(string));
        
        emitter.Emit(new Scalar("version"));
        serializer(value.Version, typeof(string));
        
        emitter.Emit(new MappingEnd());
    }
}