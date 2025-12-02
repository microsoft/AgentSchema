// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class McpServerApprovalModeYamlConverter : YamlConverter<McpServerApprovalMode>
{
    /// <summary>
    /// Singleton instance of the McpServerApprovalMode converter.
    /// </summary>
    public static readonly McpServerApprovalModeYamlConverter Instance = new McpServerApprovalModeYamlConverter();

    public override McpServerApprovalMode Read(IParser parser, ObjectDeserializer rootDeserializer)
    {

        parser.Consume<MappingStart>();
        // load polymorphic McpServerApprovalMode instance
        McpServerApprovalMode instance;
        parser.TryFindMappingEntry((Scalar s) => s.Value == "kind", out var kindValue, out var kindParsingEvent);
        if (kindValue != null)
        {
            var discriminator = kindValue.Value
                ?? throw new YamlException("Empty discriminator value for McpServerApprovalMode is not supported");
            instance = discriminator.ToLowerInvariant() switch
            {
                "always" => rootDeserializer(typeof(McpServerToolAlwaysRequireApprovalMode)) as McpServerToolAlwaysRequireApprovalMode ??
                    throw new YamlException("Empty McpServerToolAlwaysRequireApprovalMode instances are not supported"),
                "never" => rootDeserializer(typeof(McpServerToolNeverRequireApprovalMode)) as McpServerToolNeverRequireApprovalMode ??
                    throw new YamlException("Empty McpServerToolNeverRequireApprovalMode instances are not supported"),
                "specify" => rootDeserializer(typeof(McpServerToolSpecifyApprovalMode)) as McpServerToolSpecifyApprovalMode ??
                    throw new YamlException("Empty McpServerToolSpecifyApprovalMode instances are not supported"),
                _ => throw new YamlException($"Unknown McpServerApprovalMode discriminator value: {discriminator}"),
            };
        }
        else
        {
            throw new YamlException("Missing McpServerApprovalMode discriminator property: 'kind'");
        }

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    // discriminator property already processed
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in McpServerApprovalMode.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, McpServerApprovalMode value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));

        emitter.Emit(new MappingEnd());
    }
}