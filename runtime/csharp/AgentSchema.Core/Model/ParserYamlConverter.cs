// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ParserYamlConverter: YamlConverter<Parser>
{
    /// <summary>
    /// Singleton instance of the Parser converter.
    /// </summary>
    public static readonly ParserYamlConverter Instance = new ParserYamlConverter();

    public override Parser Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new Parser();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "options":
                    var optionsValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                    instance.Options = optionsValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Parser.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Parser value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        if(value.Options != null)
        {
            emitter.Emit(new Scalar("options"));
            serializer(value.Options, typeof(IDictionary<string, object>));
        }
        
        emitter.Emit(new MappingEnd());
    }
}