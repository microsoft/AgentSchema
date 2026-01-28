// Copyright (c) Microsoft. All rights reserved.
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

#pragma warning disable IDE0130
namespace AgentSchema.Core;
#pragma warning restore IDE0130

public class WorkflowYamlConverter: YamlConverter<Workflow>
{
    /// <summary>
    /// Singleton instance of the Workflow converter.
    /// </summary>
    public static readonly WorkflowYamlConverter Instance = new WorkflowYamlConverter();

    public override Workflow Read(IParser parser, ObjectDeserializer rootDeserializer)
    {
        
        parser.Consume<MappingStart>();
        // create new instance
        var instance = new Workflow();

        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var propertyName = parser.Consume<Scalar>().Value;
            switch (propertyName)
            {
                case "kind":
                    var kindValue = parser.Consume<Scalar>();
                    instance.Kind = kindValue.Value ?? throw new ArgumentException("Properties must contain a property named: kind");
                    break;
                case "trigger":
                    var triggerValue = rootDeserializer(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
                    instance.Trigger = triggerValue;
                    break;
                default:
                    throw new YamlException($"Unknown property '{propertyName}' in Workflow.");
            }
        }

        return instance;
    }

    public override void Write(IEmitter emitter, Workflow value, ObjectSerializer serializer)
    {
        emitter.Emit(new MappingStart());
        emitter.Emit(new Scalar("kind"));
        serializer(value.Kind, typeof(string));
        
        if(value.Trigger != null)
        {
            emitter.Emit(new Scalar("trigger"));
            serializer(value.Trigger, typeof(IDictionary<string, object>));
        }
        
        emitter.Emit(new MappingEnd());
    }
}