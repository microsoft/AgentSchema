// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ToolResourceYamlConverter: YamlConverter<ToolResource>
{
    /// <summary>
    /// Singleton instance of the ToolResource converter.
    /// </summary>
    public static readonly ToolResourceYamlConverter Instance = new ToolResourceYamlConverter();

    public override ToolResource Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new ToolResource();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "id":
                    var idValue = parser.Consume<Scalar>();
                    instance.Id = idValue.Value ?? throw new ArgumentException("Properties must contain a property named: id");
                    break;
                case "options":
                    var optionsValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object> ?? throw new ArgumentException("Properties must contain a property named: options");
                    instance.Options = optionsValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in ToolResource.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, ToolResource value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("id"));
        serializer(value.Id, typeof(string));
        
        emitter.Emit(new Scalar("options"));
        serializer(value.Options, typeof(IDictionary<string, object>));
        
        emitter.Emit(new MappingEnd());
    }
}