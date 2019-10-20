using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    
    [NodeType(NodeTypeName)] 
    public class UmlClassDiagramNode : UmlDiagramNode
    {
        private const string NodeTypeName = "UMLClassDiagram";

        public UmlClassDiagramNode(INode parent) : base(NodeTypeName, parent)
        {
        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            // TODO: Implement ownedViews
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            // TODO: Implement ownedViews
        }
    }
}
