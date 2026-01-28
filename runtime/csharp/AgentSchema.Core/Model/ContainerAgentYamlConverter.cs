// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ContainerAgentYamlConverter: YamlConverter<ContainerAgent>
{
    /// <summary>
    /// Singleton instance of the ContainerAgent converter.
    /// </summary>
    public static readonly ContainerAgentYamlConverter Instance = new ContainerAgentYamlConverter();

    public override ContainerAgent Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new ContainerAgent();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "protocols":
                            /*
                    if (protocolsValue.ValueKind == JsonValueKind.Array)
                    {
                            
                        instance.Protocols = 
                            [.. protocolsValue.EnumerateArray()
                                .Select(x => JsonSerializer.Deserialize<ProtocolVersionRecord> (x.GetRawText(), options)
                                    ?? throw new YamlException("Empty array elements for Protocols are not supported"))];
                    }
                            */
                            /*
                    else
                    {
                        throw new YamlException("Invalid JSON token for protocols");
                    }
                            */
                    break;
                case "environmentVariables":
                            /*
                    if (environmentVariablesValue.ValueKind == JsonValueKind.Array)
                    {
                            
                        instance.EnvironmentVariables = 
                            [.. environmentVariablesValue.EnumerateArray()
                                .Select(x => JsonSerializer.Deserialize<EnvironmentVariable> (x.GetRawText(), options)
                                    ?? throw new YamlException("Empty array elements for EnvironmentVariables are not supported"))];
                    }
                            */
                            /*
                    else if (environmentVariablesValue.ValueKind == JsonValueKind.Object)
                    {
                        instance.EnvironmentVariables = 
                            [.. environmentVariablesValue.EnumerateObject()
                                .Select(property =>
                                {
                                    var item = JsonSerializer.Deserialize<EnvironmentVariable>(property.Value.GetRawText(), options)
                                        ?? throw new YamlException("Empty array elements for EnvironmentVariables are not supported");
                                    item.Name = property.Name;
                                    return item;
                                })];
                    }
                            */
                    
                            /*
                    else
                    {
                        throw new YamlException("Invalid JSON token for environmentVariables");
                    }
                            */
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in ContainerAgent.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, ContainerAgent value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("protocols"));
        serializer(value.Protocols, typeof(IList<ProtocolVersionRecord>));
        
        if(value.EnvironmentVariables != null)
        {
            emitter.Emit(new Scalar("environmentVariables"));
            serializer(value.EnvironmentVariables, typeof(IList<EnvironmentVariable>));
        }
        
        emitter.Emit(new MappingEnd());
    }
}