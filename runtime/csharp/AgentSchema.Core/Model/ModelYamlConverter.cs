// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ModelYamlConverter: YamlConverter<Model>
{
    /// <summary>
    /// Singleton instance of the Model converter.
    /// </summary>
    public static readonly ModelYamlConverter Instance = new ModelYamlConverter();

    public override Model Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new Model();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "id":
                    var idValue = parser.Consume<Scalar>();
                    instance.Id = idValue.Value ?? throw new ArgumentException("Properties must contain a property named: id");
                    break;
                case "provider":
                    var providerValue = parser.Consume<Scalar>();
                    instance.Provider = providerValue.Value;
                    break;
                case "apiType":
                    var apiTypeValue = parser.Consume<Scalar>();
                    instance.ApiType = apiTypeValue.Value;
                    break;
                case "connection":
                    var connectionValue = rootDeserializer(typeof(Connection)) as Connection;
                    instance.Connection = connectionValue;
                    break;
                case "options":
                    var optionsValue = rootDeserializer(typeof(ModelOptions)) as ModelOptions;
                    instance.Options = optionsValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Model.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Model value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("id"));
        serializer(value.Id, typeof(string));
        
        if(value.Provider != null)
        {
            emitter.Emit(new Scalar("provider"));
            serializer(value.Provider, typeof(string));
        }
        
        if(value.ApiType != null)
        {
            emitter.Emit(new Scalar("apiType"));
            serializer(value.ApiType, typeof(string));
        }
        
        if(value.Connection != null)
        {
            emitter.Emit(new Scalar("connection"));
            serializer(value.Connection, typeof(Connection));
        }
        
        if(value.Options != null)
        {
            emitter.Emit(new Scalar("options"));
            serializer(value.Options, typeof(ModelOptions));
        }
        
        emitter.Emit(new MappingEnd());
    }
}