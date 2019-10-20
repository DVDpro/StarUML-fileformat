using System;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlInterfaceRealizationNode : UmlNode
    {
        private const string NodeTypeName = "UMLInterfaceRealization";
        
        private const string TargetPropertyName = "target";
        
        private const string SourcePropertyName = "source";

        public NodeTypeReference Target { get; set; }

        public NodeTypeReference Source { get; set; }

        public UmlInterfaceRealizationNode(INode parent) : base(NodeTypeName, parent)
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
            Source?.Write(SourcePropertyName, writer);
            Target?.Write(TargetPropertyName, writer);
        }
    }

}