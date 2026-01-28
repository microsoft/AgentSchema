// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class PropertySchemaYamlConverter: YamlConverter<PropertySchema>
{
    /// <summary>
    /// Singleton instance of the PropertySchema converter.
    /// </summary>
    public static readonly PropertySchemaYamlConverter Instance = new PropertySchemaYamlConverter();

    public override PropertySchema Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new PropertySchema();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "examples":
                    var examplesValue = rootDeserializer(typeof(Dictionary<string, object>[])) as Dictionary<string, object>[];
                    instance.Examples = examplesValue;
                    break;
                case "strict":
                    var strictValue = parser.Consume<Scalar>();
                    if(bool.TryParse(strictValue.Value, out var strictItem))
                    {
                        instance.Strict = strictItem;
                    }
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
                    throw new YamlException($"Unknown property '{propertyName}' in PropertySchema.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, PropertySchema value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        if(value.Examples != null)
        {
            emitter.Emit(new Scalar("examples"));
            serializer(value.Examples, typeof(IList<IDictionary<string, object>>));
        }
        
        if(value.Strict != null)
        {
            emitter.Emit(new Scalar("strict"));
            serializer(value.Strict, typeof(bool));
        }
        
        emitter.Emit(new Scalar("properties"));
        serializer(value.Properties, typeof(IList<Property>));
        
        emitter.Emit(new MappingEnd());
    }
}