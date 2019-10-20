using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlAttributeNode : UmlNode
    {
        private const string NodeTypeName = "UMLAttribute";

        public NodeTypeReference Type { get; set; }

        private const string TypePropertyName = "type";
        public UmlAttributeNode(INode parent) : base(NodeTypeName, parent)
        {
        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(TypePropertyName, out var typeProperty))
            {
                Type = new NodeTypeReference(this, typeProperty);
            }
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            if (Type != null)
            {
                writer.WritePropertyName(TypePropertyName);
                Type.Write(writer);
            }
        }
    }
}
