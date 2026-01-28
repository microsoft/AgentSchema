// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class McpServerToolAlwaysRequireApprovalModeYamlConverter: YamlConverter<McpServerToolAlwaysRequireApprovalMode>
{
    /// <summary>
    /// Singleton instance of the McpServerToolAlwaysRequireApprovalMode converter.
    /// </summary>
    public static readonly McpServerToolAlwaysRequireApprovalModeYamlConverter Instance = new McpServerToolAlwaysRequireApprovalModeYamlConverter();

    public override McpServerToolAlwaysRequireApprovalMode Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new McpServerToolAlwaysRequireApprovalMode();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in McpServerToolAlwaysRequireApprovalMode.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, McpServerToolAlwaysRequireApprovalMode value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new MappingEnd());
    }
}