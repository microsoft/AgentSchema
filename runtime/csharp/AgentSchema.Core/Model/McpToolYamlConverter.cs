// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class McpToolYamlConverter: YamlConverter<McpTool>
{
    /// <summary>
    /// Singleton instance of the McpTool converter.
    /// </summary>
    public static readonly McpToolYamlConverter Instance = new McpToolYamlConverter();

    public override McpTool Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new McpTool();

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
                case "serverName":
                    var serverNameValue = parser.Consume<Scalar>();
                    instance.ServerName = serverNameValue.Value ?? throw new ArgumentException("Properties must contain a property named: serverName");
                    break;
                case "serverDescription":
                    var serverDescriptionValue = parser.Consume<Scalar>();
                    instance.ServerDescription = serverDescriptionValue.Value;
                    break;
                case "approvalMode":
                    var approvalModeValue = rootDeserializer(typeof(McpServerApprovalMode)) as McpServerApprovalMode ?? throw new ArgumentException("Properties must contain a property named: approvalMode");
                    instance.ApprovalMode = approvalModeValue;
                    break;
                case "allowedTools":
                            /*
                    instance.AllowedTools = [.. allowedToolsValue.EnumerateArray().Select(x => x.GetString() ?? throw new YamlException("Empty array elements for allowedTools are not supported"))];
                            */
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in McpTool.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, McpTool value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        emitter.Emit(new Scalar("connection"));
        serializer(value.Connection, typeof(Connection));
        
        emitter.Emit(new Scalar("serverName"));
        serializer(value.ServerName, typeof(string));
        
        if(value.ServerDescription != null)
        {
            emitter.Emit(new Scalar("serverDescription"));
            serializer(value.ServerDescription, typeof(string));
        }
        
        emitter.Emit(new Scalar("approvalMode"));
        serializer(value.ApprovalMode, typeof(McpServerApprovalMode));
        
        if(value.AllowedTools != null)
        {
            emitter.Emit(new Scalar("allowedTools"));
            serializer(value.AllowedTools, typeof(IList<string>));
        }
        
        emitter.Emit(new MappingEnd());
    }
}