// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class PromptAgentYamlConverter: YamlConverter<PromptAgent>
{
    /// <summary>
    /// Singleton instance of the PromptAgent converter.
    /// </summary>
    public static readonly PromptAgentYamlConverter Instance = new PromptAgentYamlConverter();

    public override PromptAgent Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new PromptAgent();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "model":
                    var modelValue = rootDeserializer(typeof(Model)) as Model ?? throw new ArgumentException("Properties must contain a property named: model");
                    instance.Model = modelValue;
                    break;
                case "tools":
                            /*
                    if (toolsValue.ValueKind == JsonValueKind.Array)
                    {
                            
                        instance.Tools = 
                            [.. toolsValue.EnumerateArray()
                                .Select(x => JsonSerializer.Deserialize<Tool> (x.GetRawText(), options)
                                    ?? throw new YamlException("Empty array elements for Tools are not supported"))];
                    }
                            */
                            /*
                    else if (toolsValue.ValueKind == JsonValueKind.Object)
                    {
                        instance.Tools = 
                            [.. toolsValue.EnumerateObject()
                                .Select(property =>
                                {
                                    var item = JsonSerializer.Deserialize<Tool>(property.Value.GetRawText(), options)
                                        ?? throw new YamlException("Empty array elements for Tools are not supported");
                                    item.Name = property.Name;
                                    return item;
                                })];
                    }
                            */
                    
                            /*
                    else
                    {
                        throw new YamlException("Invalid JSON token for tools");
                    }
                            */
                    break;
                case "template":
                    var templateValue = rootDeserializer(typeof(Template)) as Template;
                    instance.Template = templateValue;
                    break;
                case "instructions":
                    var instructionsValue = parser.Consume<Scalar>();
                    instance.Instructions = instructionsValue.Value;
                    break;
                case "additionalInstructions":
                    var additionalInstructionsValue = parser.Consume<Scalar>();
                    instance.AdditionalInstructions = additionalInstructionsValue.Value;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in PromptAgent.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, PromptAgent value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("model"));
        serializer(value.Model, typeof(Model));
        
        if(value.Tools != null)
        {
            emitter.Emit(new Scalar("tools"));
            serializer(value.Tools, typeof(IList<Tool>));
        }
        
        if(value.Template != null)
        {
            emitter.Emit(new Scalar("template"));
            serializer(value.Template, typeof(Template));
        }
        
        if(value.Instructions != null)
        {
            emitter.Emit(new Scalar("instructions"));
            serializer(value.Instructions, typeof(string));
        }
        
        if(value.AdditionalInstructions != null)
        {
            emitter.Emit(new Scalar("additionalInstructions"));
            serializer(value.AdditionalInstructions, typeof(string));
        }
        
        emitter.Emit(new MappingEnd());
    }
}