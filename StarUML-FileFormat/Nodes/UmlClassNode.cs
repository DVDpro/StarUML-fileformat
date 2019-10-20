using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    
    [NodeType(NodeTypeName)] 
    public class UmlClassNode : UmlNode
    {
        private const string NodeTypeName = "UMLClass";

        private const string AttributesPropertyName = "attributes";
        
        public List<UmlAttributeNode> Attributes { get; set; }

        public UmlClassNode(INode parent) : base(NodeTypeName, parent)
        {
        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(AttributesPropertyName, out var attributes))
            {
                Attributes = new List<UmlAttributeNode>();
                foreach (var attrElement in attributes.EnumerateArray())
                {
                    var attr = (UmlAttributeNode)NodeFactory.CreateAndInitializeFromElement(this, attrElement);
                    Attributes.Add(attr);
                }
            }
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);

            if (OwnedElements != null)
            {
                writer.WritePropertyName(AttributesPropertyName);
                writer.WriteStartArray();
                foreach (var node in Attributes)
                {
                    writer.WriteStartObject();
                    node.Write(writer);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
        }

        public override IEnumerable<INode> Children
        {
            get
            {
                if (Attributes == null)
                    return base.Children;
                return base.Children.Union(Attributes);
            }
        }
    }
}
