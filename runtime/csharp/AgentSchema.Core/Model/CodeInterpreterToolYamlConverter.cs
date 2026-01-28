// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class CodeInterpreterToolYamlConverter: YamlConverter<CodeInterpreterTool>
{
    /// <summary>
    /// Singleton instance of the CodeInterpreterTool converter.
    /// </summary>
    public static readonly CodeInterpreterToolYamlConverter Instance = new CodeInterpreterToolYamlConverter();

    public override CodeInterpreterTool Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new CodeInterpreterTool();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "fileIds":
                            /*
                    instance.FileIds = [.. fileIdsValue.EnumerateArray().Select(x => x.GetString() ?? throw new YamlException("Empty array elements for fileIds are not supported"))];
                            */
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in CodeInterpreterTool.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, CodeInterpreterTool value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("fileIds"));
        serializer(value.FileIds, typeof(IList<string>));
        
        emitter.Emit(new MappingEnd());
    }
}