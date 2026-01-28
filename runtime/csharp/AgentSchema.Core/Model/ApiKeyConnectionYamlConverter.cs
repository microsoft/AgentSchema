// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ApiKeyConnectionYamlConverter: YamlConverter<ApiKeyConnection>
{
    /// <summary>
    /// Singleton instance of the ApiKeyConnection converter.
    /// </summary>
    public static readonly ApiKeyConnectionYamlConverter Instance = new ApiKeyConnectionYamlConverter();

    public override ApiKeyConnection Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new ApiKeyConnection();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "endpoint":
                    var endpointValue = parser.Consume<Scalar>();
                    instance.Endpoint = endpointValue.Value ?? throw new ArgumentException("Properties must contain a property named: endpoint");
                    break;
                case "apiKey":
                    var apiKeyValue = parser.Consume<Scalar>();
                    instance.ApiKey = apiKeyValue.Value ?? throw new ArgumentException("Properties must contain a property named: apiKey");
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in ApiKeyConnection.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, ApiKeyConnection value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("endpoint"));
        serializer(value.Endpoint, typeof(string));
        
        emitter.Emit(new Scalar("apiKey"));
        serializer(value.ApiKey, typeof(string));
        
        emitter.Emit(new MappingEnd());
    }
}