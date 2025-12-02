// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class ToolYamlConverter : YamlConverter<Tool>
{
    /// <summary>
    /// Singleton instance of the Tool converter.
    /// </summary>
    public static readonly ToolYamlConverter Instance = new ToolYamlConverter();

    public override Tool Read(IParser parser, ObjectDeserializer rootDeserializer)
    {

        parser.Consume<MappingStart>();
        // load polymorphic Tool instance
        Tool instance;
        parser.TryFindMappingEntry((Scalar s) => s.Value == "kind", out var kindValue, out var kindParsingEvent);
        if (kindValue != null)
        {
            var discriminator = kindValue.Value
                ?? throw new YamlException("Empty discriminator value for Tool is not supported");
            instance = discriminator.ToLowerInvariant() switch
            {
                "function" => rootDeserializer(typeof(FunctionTool)) as FunctionTool ??
                    throw new YamlException("Empty FunctionTool instances are not supported"),
                "bing_search" => rootDeserializer(typeof(WebSearchTool)) as WebSearchTool ??
                    throw new YamlException("Empty WebSearchTool instances are not supported"),
                "file_search" => rootDeserializer(typeof(FileSearchTool)) as FileSearchTool ??
                    throw new YamlException("Empty FileSearchTool instances are not supported"),
                "mcp" => rootDeserializer(typeof(McpTool)) as McpTool ??
                    throw new YamlException("Empty McpTool instances are not supported"),
                "openapi" => rootDeserializer(typeof(OpenApiTool)) as OpenApiTool ??
                    throw new YamlException("Empty OpenApiTool instances are not supported"),
                "code_interpreter" => rootDeserializer(typeof(CodeInterpreterTool)) as CodeInterpreterTool ??
                    throw new YamlException("Empty CodeInterpreterTool instances are not supported"),
                _ => new CustomTool(),
            };
        }
        else
        {
            throw new YamlException("Missing Tool discriminator property: 'kind'");
        }

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "name":
                    var nameValue = parser.Consume<Scalar>();
                    instance.Name = nameValue.Value ?? throw new ArgumentException("Properties must contain a property named: name");
                    break;
                case "kind":
                    // discriminator property already processed
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "description":
                    var descriptionValue = parser.Consume<Scalar>();
                    instance.Description = descriptionValue.Value;
                    break;
                case "bindings":
                    /*
            if (bindingsValue.ValueKind == JsonValueKind.Array)
            {

                instance.Bindings = 
                    [.. bindingsValue.EnumerateArray()
                        .Select(x => JsonSerializer.Deserialize<Binding> (x.GetRawText(), options)
                            ?? throw new YamlException("Empty array elements for Bindings are not supported"))];
            }
                    */
                    /*
            else if (bindingsValue.ValueKind == JsonValueKind.Object)
            {
                instance.Bindings = 
                    [.. bindingsValue.EnumerateObject()
                        .Select(property =>
                        {
                            var item = JsonSerializer.Deserialize<Binding>(property.Value.GetRawText(), options)
                                ?? throw new YamlException("Empty array elements for Bindings are not supported");
                            item.Name = property.Name;
                            return item;
                        })];
            }
                    */

                    /*
            else
            {
                throw new YamlException("Invalid JSON token for bindings");
            }
                    */
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Tool.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Tool value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("name"));
        serializer(value.Name, typeof(string));

        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));

        if (value.Description != null)
        {
            emitter.Emit(new Scalar("description"));
            serializer(value.Description, typeof(string));
        }

        if (value.Bindings != null)
        {
            emitter.Emit(new Scalar("bindings"));
            serializer(value.Bindings, typeof(IList<Binding>));
        }

        emitter.Emit(new MappingEnd());
    }
}