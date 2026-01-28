// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ObjectPropertyYamlConverter: YamlConverter<ObjectProperty>
{
    /// <summary>
    /// Singleton instance of the ObjectProperty converter.
    /// </summary>
    public static readonly ObjectPropertyYamlConverter Instance = new ObjectPropertyYamlConverter();

    public override ObjectProperty Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new ObjectProperty();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "properties":
                            /*
                    if (propertiesValue.ValueKind == JsonValueKind.Array)
                    {
                            
                        instance.Properties = 
                            [.. propertiesValue.EnumerateArray()
                                .Select(x => JsonSerializer.Deserialize<Property> (x.GetRawText(), options)
                                    ?? throw new YamlException("Empty array elements for Properties are not supported"))];
                    }
                            */
                            /*
                    else if (propertiesValue.ValueKind == JsonValueKind.Object)
                    {
                        instance.Properties = 
                            [.. propertiesValue.EnumerateObject()
                                .Select(property =>
                                {
                                    var item = JsonSerializer.Deserialize<Property>(property.Value.GetRawText(), options)
                                        ?? throw new YamlException("Empty array elements for Properties are not supported");
                                    item.Name = property.Name;
                                    return item;
                                })];
                    }
                            */
                    
                            /*
                    else
                    {
                        throw new YamlException("Invalid JSON token for properties");
                    }
                            */
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in ObjectProperty.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, ObjectProperty value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("properties"));
        serializer(value.Properties, typeof(IList<Property>));
        
        emitter.Emit(new MappingEnd());
    }
}