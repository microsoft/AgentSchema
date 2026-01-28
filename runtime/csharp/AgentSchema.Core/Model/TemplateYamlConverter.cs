// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class TemplateYamlConverter: YamlConverter<Template>
{
    /// <summary>
    /// Singleton instance of the Template converter.
    /// </summary>
    public static readonly TemplateYamlConverter Instance = new TemplateYamlConverter();

    public override Template Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new Template();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "format":
                    var formatValue = rootDeserializer(typeof(Format)) as Format ?? throw new ArgumentException("Properties must contain a property named: format");
                    instance.Format = formatValue;
                    break;
                case "parser":
                    var parserValue = rootDeserializer(typeof(Parser)) as Parser ?? throw new ArgumentException("Properties must contain a property named: parser");
                    instance.Parser = parserValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Template.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Template value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("format"));
        serializer(value.Format, typeof(Format));
        
        emitter.Emit(new Scalar("parser"));
        serializer(value.Parser, typeof(Parser));
        
        emitter.Emit(new MappingEnd());
    }
}