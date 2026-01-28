// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class FileSearchToolYamlConverter: YamlConverter<FileSearchTool>
{
    /// <summary>
    /// Singleton instance of the FileSearchTool converter.
    /// </summary>
    public static readonly FileSearchToolYamlConverter Instance = new FileSearchToolYamlConverter();

    public override FileSearchTool Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new FileSearchTool();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "connection":
                    var connectionValue = rootDeserializer(typeof(Connection)) as Connection ?? throw new ArgumentException("Properties must contain a property named: connection");
                    instance.Connection = connectionValue;
                    break;
                case "vectorStoreIds":
                            /*
                    instance.VectorStoreIds = [.. vectorStoreIdsValue.EnumerateArray().Select(x => x.GetString() ?? throw new YamlException("Empty array elements for vectorStoreIds are not supported"))];
                            */
                    break;
                case "maximumResultCount":
                    var maximumResultCountValue = parser.Consume<Scalar>();
                    if(int.TryParse(maximumResultCountValue.Value, out var maximumResultCountItem))
                    {
                        instance.MaximumResultCount = maximumResultCountItem;
                    }
                    break;
                case "ranker":
                    var rankerValue = parser.Consume<Scalar>();
                    instance.Ranker = rankerValue.Value;
                    break;
                case "scoreThreshold":
                    var scoreThresholdValue = parser.Consume<Scalar>();
                    if(float.TryParse(scoreThresholdValue.Value, out var scoreThresholdItem))
                    {
                        instance.ScoreThreshold = scoreThresholdItem;
                    }
                    break;
                case "filters":
                    var filtersValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                    instance.Filters = filtersValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in FileSearchTool.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, FileSearchTool value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("connection"));
        serializer(value.Connection, typeof(Connection));
        
        emitter.Emit(new Scalar("vectorStoreIds"));
        serializer(value.VectorStoreIds, typeof(IList<string>));
        
        if(value.MaximumResultCount != null)
        {
            emitter.Emit(new Scalar("maximumResultCount"));
            serializer(value.MaximumResultCount, typeof(int));
        }
        
        if(value.Ranker != null)
        {
            emitter.Emit(new Scalar("ranker"));
            serializer(value.Ranker, typeof(string));
        }
        
        if(value.ScoreThreshold != null)
        {
            emitter.Emit(new Scalar("scoreThreshold"));
            serializer(value.ScoreThreshold, typeof(float));
        }
        
        if(value.Filters != null)
        {
            emitter.Emit(new Scalar("filters"));
            serializer(value.Filters, typeof(IDictionary<string, object>));
        }
        
        emitter.Emit(new MappingEnd());
    }
}