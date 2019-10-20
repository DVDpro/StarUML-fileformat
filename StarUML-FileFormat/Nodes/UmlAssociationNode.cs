using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlAssociationNode : UmlNode
    {
        private const string NodeTypeName = "UMLAssociation";
        
        private const string End1PropertyName = "end1";
        private const string End2PropertyName = "end2";

        public UmlAssociationEndNode End1 { get; set; }

        public UmlAssociationEndNode End2 { get; set; }

        public UmlAssociationNode(INode parent) : base(NodeTypeName, parent)
        {
        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(End1PropertyName, out var end1Property))
            {
                End1 = (UmlAssociationEndNode)NodeFactory.CreateAndInitializeFromElement(this, end1Property);
            }

            if (element.TryGetProperty(End2PropertyName, out var end2Property))
            {
                End2 = (UmlAssociationEndNode)NodeFactory.CreateAndInitializeFromElement(this, end2Property);
            }
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            
            if (End1 != null)
            {
                writer.WritePropertyName(End1PropertyName);
                writer.WriteStartObject();
                End1.Write(writer);
                writer.WriteEndObject();
            }

            if (End2 != null)
            {
                writer.WritePropertyName(End2PropertyName);
                writer.WriteStartObject();
                End2.Write(writer);
                writer.WriteEndObject();
            }
        }
    }
}
