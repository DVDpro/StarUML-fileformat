using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)] 
    public class ERDDiagramNode : UmlDiagramNode
    {
        private const string NodeTypeName = "ERDDiagram";

        public ERDDiagramNode(INode parent) : base(NodeTypeName, parent)
        {
        }
    }
}
