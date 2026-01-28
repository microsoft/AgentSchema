// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class AgentManifestYamlConverter: YamlConverter<AgentManifest>
{
    /// <summary>
    /// Singleton instance of the AgentManifest converter.
    /// </summary>
    public static readonly AgentManifestYamlConverter Instance = new AgentManifestYamlConverter();

    public override AgentManifest Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new AgentManifest();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "name":
                    var nameValue = parser.Consume<Scalar>();
                    instance.Name = nameValue.Value ?? throw new ArgumentException("Properties must contain a property named: name");
                    break;
                case "displayName":
                    var displayNameValue = parser.Consume<Scalar>();
                    instance.DisplayName = displayNameValue.Value ?? throw new ArgumentException("Properties must contain a property named: displayName");
                    break;
                case "description":
                    var descriptionValue = parser.Consume<Scalar>();
                    instance.Description = descriptionValue.Value;
                    break;
                case "metadata":
                    var metadataValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                    instance.Metadata = metadataValue;
                    break;
                case "template":
                    var templateValue = rootDeserializer(typeof(AgentDefinition)) as AgentDefinition ?? throw new ArgumentException("Properties must contain a property named: template");
                    instance.Template = templateValue;
                    break;
                case "parameters":
                    var parametersValue = rootDeserializer(typeof(PropertySchema)) as PropertySchema ?? throw new ArgumentException("Properties must contain a property named: parameters");
                    instance.Parameters = parametersValue;
                    break;
                case "resources":
                            /*
                    if (resourcesValue.ValueKind == JsonValueKind.Array)
                    {
                            
                        instance.Resources = 
                            [.. resourcesValue.EnumerateArray()
                                .Select(x => JsonSerializer.Deserialize<Resource> (x.GetRawText(), options)
                                    ?? throw new YamlException("Empty array elements for Resources are not supported"))];
                    }
                            */
                            /*
                    else if (resourcesValue.ValueKind == JsonValueKind.Object)
                    {
                        instance.Resources = 
                            [.. resourcesValue.EnumerateObject()
                                .Select(property =>
                                {
                                    var item = JsonSerializer.Deserialize<Resource>(property.Value.GetRawText(), options)
                                        ?? throw new YamlException("Empty array elements for Resources are not supported");
                                    item.Name = property.Name;
                                    return item;
                                })];
                    }
                            */
                    
                            /*
                    else
                    {
                        throw new YamlException("Invalid JSON token for resources");
                    }
                            */
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in AgentManifest.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, AgentManifest value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("name"));
        serializer(value.Name, typeof(string));
        
        emitter.Emit(new Scalar("displayName"));
        serializer(value.DisplayName, typeof(string));
        
        if(value.Description != null)
        {
            emitter.Emit(new Scalar("description"));
            serializer(value.Description, typeof(string));
        }
        
        if(value.Metadata != null)
        {
            emitter.Emit(new Scalar("metadata"));
            serializer(value.Metadata, typeof(IDictionary<string, object>));
        }
        
        emitter.Emit(new Scalar("template"));
        serializer(value.Template, typeof(AgentDefinition));
        
        emitter.Emit(new Scalar("parameters"));
        serializer(value.Parameters, typeof(PropertySchema));
        
        emitter.Emit(new Scalar("resources"));
        serializer(value.Resources, typeof(IList<Resource>));
        
        emitter.Emit(new MappingEnd());
    }
}