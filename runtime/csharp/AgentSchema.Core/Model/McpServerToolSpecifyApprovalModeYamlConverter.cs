// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class McpServerToolSpecifyApprovalModeYamlConverter: YamlConverter<McpServerToolSpecifyApprovalMode>
{
    /// <summary>
    /// Singleton instance of the McpServerToolSpecifyApprovalMode converter.
    /// </summary>
    public static readonly McpServerToolSpecifyApprovalModeYamlConverter Instance = new McpServerToolSpecifyApprovalModeYamlConverter();

    public override McpServerToolSpecifyApprovalMode Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new McpServerToolSpecifyApprovalMode();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "alwaysRequireApprovalTools":
                            /*
                    instance.AlwaysRequireApprovalTools = [.. alwaysRequireApprovalToolsValue.EnumerateArray().Select(x => x.GetString() ?? throw new YamlException("Empty array elements for alwaysRequireApprovalTools are not supported"))];
                            */
                    break;
                case "neverRequireApprovalTools":
                            /*
                    instance.NeverRequireApprovalTools = [.. neverRequireApprovalToolsValue.EnumerateArray().Select(x => x.GetString() ?? throw new YamlException("Empty array elements for neverRequireApprovalTools are not supported"))];
                            */
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in McpServerToolSpecifyApprovalMode.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, McpServerToolSpecifyApprovalMode value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("alwaysRequireApprovalTools"));
        serializer(value.AlwaysRequireApprovalTools, typeof(IList<string>));
        
        emitter.Emit(new Scalar("neverRequireApprovalTools"));
        serializer(value.NeverRequireApprovalTools, typeof(IList<string>));
        
        emitter.Emit(new MappingEnd());
    }
}