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
        
        public List<INode> Attributes { get; set; }

        public UmlClassNode(INode parent) : base(NodeTypeName, parent)
        {
        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(AttributesPropertyName, out var attributes))
            {
                Attributes = new List<INode>();
                foreach (var ownedElement in attributes.EnumerateArray())
                {
                    var ownedNode = NodeFactory.CreateAndInitializeFromElement(this, ownedElement);
                    Attributes.Add(ownedNode);
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
                return base.Children.Union(Attributes);
            }
        }
    }
}
