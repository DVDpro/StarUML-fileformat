using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlGeneralizationNode : UmlNode
    {
        private const string NodeTypeName = "UMLGeneralization";

        private const string TargetPropertyName = "target";
        private const string SourcePropertyName = "source";

        public NodeTypeReference Target { get; set; }

        public NodeTypeReference Source { get; set; }

        public UmlGeneralizationNode(INode parent) : base(NodeTypeName, parent)
        {
        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(TargetPropertyName, out var targetProperty))
            {
                Target = new NodeTypeReference(this, targetProperty);
            }

            if (element.TryGetProperty(SourcePropertyName, out var sourceProperty))
            {
                Source = new NodeTypeReference(this, sourceProperty);
            }
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            if (Source != null)
            {
                writer.WritePropertyName(SourcePropertyName);
                writer.WriteStartObject();
                Source.Write(writer);
                writer.WriteEndObject();
            }

            if (Target != null)
            {
                writer.WritePropertyName(TargetPropertyName);
                writer.WriteStartObject();
                Target.Write(writer);
                writer.WriteEndObject();
            }
        }
    }
}
