// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class McpServerToolNeverRequireApprovalModeYamlConverter: YamlConverter<McpServerToolNeverRequireApprovalMode>
{
    /// <summary>
    /// Singleton instance of the McpServerToolNeverRequireApprovalMode converter.
    /// </summary>
    public static readonly McpServerToolNeverRequireApprovalModeYamlConverter Instance = new McpServerToolNeverRequireApprovalModeYamlConverter();

    public override McpServerToolNeverRequireApprovalMode Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new McpServerToolNeverRequireApprovalMode();

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
                    throw new YamlException($"Unknown property '{propertyName}' in McpServerToolNeverRequireApprovalMode.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, McpServerToolNeverRequireApprovalMode value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new MappingEnd());
    }
}