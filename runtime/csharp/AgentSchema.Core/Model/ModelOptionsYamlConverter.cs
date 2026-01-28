// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ModelOptionsYamlConverter: YamlConverter<ModelOptions>
{
    /// <summary>
    /// Singleton instance of the ModelOptions converter.
    /// </summary>
    public static readonly ModelOptionsYamlConverter Instance = new ModelOptionsYamlConverter();

    public override ModelOptions Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new ModelOptions();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "frequencyPenalty":
                    var frequencyPenaltyValue = parser.Consume<Scalar>();
                    if(float.TryParse(frequencyPenaltyValue.Value, out var frequencyPenaltyItem))
                    {
                        instance.FrequencyPenalty = frequencyPenaltyItem;
                    }
                    break;
                case "maxOutputTokens":
                    var maxOutputTokensValue = parser.Consume<Scalar>();
                    if(int.TryParse(maxOutputTokensValue.Value, out var maxOutputTokensItem))
                    {
                        instance.MaxOutputTokens = maxOutputTokensItem;
                    }
                    break;
                case "presencePenalty":
                    var presencePenaltyValue = parser.Consume<Scalar>();
                    if(float.TryParse(presencePenaltyValue.Value, out var presencePenaltyItem))
                    {
                        instance.PresencePenalty = presencePenaltyItem;
                    }
                    break;
                case "seed":
                    var seedValue = parser.Consume<Scalar>();
                    if(int.TryParse(seedValue.Value, out var seedItem))
                    {
                        instance.Seed = seedItem;
                    }
                    break;
                case "temperature":
                    var temperatureValue = parser.Consume<Scalar>();
                    if(float.TryParse(temperatureValue.Value, out var temperatureItem))
                    {
                        instance.Temperature = temperatureItem;
                    }
                    break;
                case "topK":
                    var topKValue = parser.Consume<Scalar>();
                    if(int.TryParse(topKValue.Value, out var topKItem))
                    {
                        instance.TopK = topKItem;
                    }
                    break;
                case "topP":
                    var topPValue = parser.Consume<Scalar>();
                    if(float.TryParse(topPValue.Value, out var topPItem))
                    {
                        instance.TopP = topPItem;
                    }
                    break;
                case "stopSequences":
                            /*
                    instance.StopSequences = [.. stopSequencesValue.EnumerateArray().Select(x => x.GetString() ?? throw new YamlException("Empty array elements for stopSequences are not supported"))];
                            */
                    break;
                case "allowMultipleToolCalls":
                    var allowMultipleToolCallsValue = parser.Consume<Scalar>();
                    if(bool.TryParse(allowMultipleToolCallsValue.Value, out var allowMultipleToolCallsItem))
                    {
                        instance.AllowMultipleToolCalls = allowMultipleToolCallsItem;
                    }
                    break;
                case "additionalProperties":
                    var additionalPropertiesValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                    instance.AdditionalProperties = additionalPropertiesValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in ModelOptions.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, ModelOptions value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        if(value.FrequencyPenalty != null)
        {
            emitter.Emit(new Scalar("frequencyPenalty"));
            serializer(value.FrequencyPenalty, typeof(float));
        }
        
        if(value.MaxOutputTokens != null)
        {
            emitter.Emit(new Scalar("maxOutputTokens"));
            serializer(value.MaxOutputTokens, typeof(int));
        }
        
        if(value.PresencePenalty != null)
        {
            emitter.Emit(new Scalar("presencePenalty"));
            serializer(value.PresencePenalty, typeof(float));
        }
        
        if(value.Seed != null)
        {
            emitter.Emit(new Scalar("seed"));
            serializer(value.Seed, typeof(int));
        }
        
        if(value.Temperature != null)
        {
            emitter.Emit(new Scalar("temperature"));
            serializer(value.Temperature, typeof(float));
        }
        
        if(value.TopK != null)
        {
            emitter.Emit(new Scalar("topK"));
            serializer(value.TopK, typeof(int));
        }
        
        if(value.TopP != null)
        {
            emitter.Emit(new Scalar("topP"));
            serializer(value.TopP, typeof(float));
        }
        
        if(value.StopSequences != null)
        {
            emitter.Emit(new Scalar("stopSequences"));
            serializer(value.StopSequences, typeof(IList<string>));
        }
        
        if(value.AllowMultipleToolCalls != null)
        {
            emitter.Emit(new Scalar("allowMultipleToolCalls"));
            serializer(value.AllowMultipleToolCalls, typeof(bool));
        }
        
        if(value.AdditionalProperties != null)
        {
            emitter.Emit(new Scalar("additionalProperties"));
            serializer(value.AdditionalProperties, typeof(IDictionary<string, object>));
        }
        
        emitter.Emit(new MappingEnd());
    }
}