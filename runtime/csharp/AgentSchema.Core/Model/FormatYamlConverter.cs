// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class FormatYamlConverter: YamlConverter<Format>
{
    /// <summary>
    /// Singleton instance of the Format converter.
    /// </summary>
    public static readonly FormatYamlConverter Instance = new FormatYamlConverter();

    public override Format Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new Format();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "strict":
                    var strictValue = parser.Consume<Scalar>();
                    if(bool.TryParse(strictValue.Value, out var strictItem))
                    {
                        instance.Strict = strictItem;
                    }
                    break;
                case "options":
                    var optionsValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                    instance.Options = optionsValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Format.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Format value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        if(value.Strict != null)
        {
            emitter.Emit(new Scalar("strict"));
            serializer(value.Strict, typeof(bool));
        }
        
        if(value.Options != null)
        {
            emitter.Emit(new Scalar("options"));
            serializer(value.Options, typeof(IDictionary<string, object>));
        }
        
        emitter.Emit(new MappingEnd());
    }
}