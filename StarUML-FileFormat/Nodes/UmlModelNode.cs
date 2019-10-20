using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlModelNode : UmlNode
    {
        private const string NodeTypeName = "UMLModel";
        
        public UmlModelNode(INode parent) : base(NodeTypeName, parent)
        {
        }
    }
}
