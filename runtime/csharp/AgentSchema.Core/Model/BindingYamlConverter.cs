// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class BindingYamlConverter: YamlConverter<Binding>
{
    /// <summary>
    /// Singleton instance of the Binding converter.
    /// </summary>
    public static readonly BindingYamlConverter Instance = new BindingYamlConverter();

    public override Binding Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new Binding();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "name":
                    var nameValue = parser.Consume<Scalar>();
                    instance.Name = nameValue.Value ?? throw new ArgumentException("Properties must contain a property named: name");
                    break;
                case "input":
                    var inputValue = parser.Consume<Scalar>();
                    instance.Input = inputValue.Value ?? throw new ArgumentException("Properties must contain a property named: input");
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Binding.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Binding value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("name"));
        serializer(value.Name, typeof(string));
        
        emitter.Emit(new Scalar("input"));
        serializer(value.Input, typeof(string));
        
        emitter.Emit(new MappingEnd());
    }
}