// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class WebSearchToolYamlConverter: YamlConverter<WebSearchTool>
{
    /// <summary>
    /// Singleton instance of the WebSearchTool converter.
    /// </summary>
    public static readonly WebSearchToolYamlConverter Instance = new WebSearchToolYamlConverter();

    public override WebSearchTool Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new WebSearchTool();

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
                case "options":
                    var optionsValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                    instance.Options = optionsValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in WebSearchTool.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, WebSearchTool value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("connection"));
        serializer(value.Connection, typeof(Connection));
        
        if(value.Options != null)
        {
            emitter.Emit(new Scalar("options"));
            serializer(value.Options, typeof(IDictionary<string, object>));
        }
        
        emitter.Emit(new MappingEnd());
    }
}