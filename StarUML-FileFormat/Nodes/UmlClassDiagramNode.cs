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
    }
}
