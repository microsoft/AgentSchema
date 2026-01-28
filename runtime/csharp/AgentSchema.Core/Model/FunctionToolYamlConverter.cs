// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class FunctionToolYamlConverter: YamlConverter<FunctionTool>
{
    /// <summary>
    /// Singleton instance of the FunctionTool converter.
    /// </summary>
    public static readonly FunctionToolYamlConverter Instance = new FunctionToolYamlConverter();

    public override FunctionTool Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new FunctionTool();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "parameters":
                    var parametersValue = rootDeserializer(typeof(PropertySchema)) as PropertySchema ?? throw new ArgumentException("Properties must contain a property named: parameters");
                    instance.Parameters = parametersValue;
                    break;
                case "strict":
                    var strictValue = parser.Consume<Scalar>();
                    if(bool.TryParse(strictValue.Value, out var strictItem))
                    {
                        instance.Strict = strictItem;
                    }
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in FunctionTool.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, FunctionTool value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("parameters"));
        serializer(value.Parameters, typeof(PropertySchema));
        
        if(value.Strict != null)
        {
            emitter.Emit(new Scalar("strict"));
            serializer(value.Strict, typeof(bool));
        }
        
        emitter.Emit(new MappingEnd());
    }
}