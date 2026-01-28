// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class EnvironmentVariableYamlConverter: YamlConverter<EnvironmentVariable>
{
    /// <summary>
    /// Singleton instance of the EnvironmentVariable converter.
    /// </summary>
    public static readonly EnvironmentVariableYamlConverter Instance = new EnvironmentVariableYamlConverter();

    public override EnvironmentVariable Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new EnvironmentVariable();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "name":
                    var nameValue = parser.Consume<Scalar>();
                    instance.Name = nameValue.Value ?? throw new ArgumentException("Properties must contain a property named: name");
                    break;
                case "value":
                    var valueValue = parser.Consume<Scalar>();
                    instance.Value = valueValue.Value ?? throw new ArgumentException("Properties must contain a property named: value");
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in EnvironmentVariable.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, EnvironmentVariable value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("name"));
        serializer(value.Name, typeof(string));
        
        emitter.Emit(new Scalar("value"));
        serializer(value.Value, typeof(string));
        
        emitter.Emit(new MappingEnd());
    }
}