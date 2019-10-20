using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlEnumerationNode : UmlNode
    {
        private const string NodeTypeName = "UMLEnumeration";

        private const string LiteralsPropertyName = "literals";

        public List<UmlEnumerationLiteralNode> Literals { get; set; }
        
        public UmlEnumerationNode(INode parent) : base(NodeTypeName, parent)
        {

        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(LiteralsPropertyName, out var literalsElement))
            {
                Literals = new List<UmlEnumerationLiteralNode>();
                foreach (var litElement in literalsElement.EnumerateArray())
                {
                    var lit = (UmlEnumerationLiteralNode)NodeFactory.CreateAndInitializeFromElement(this, litElement);
                    Literals.Add(lit);
                }
            }
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            if (Literals != null)
            {
                writer.WritePropertyName(LiteralsPropertyName);
                writer.WriteStartArray();
                foreach (var lit in Literals)
                {
                    lit.Write(writer);
                }
                writer.WriteEndArray();
            }
        }

        public override IEnumerable<INode> Children
        {
            get
            {
                return base.Children.Union(Literals);
            }
        }
    }
}
