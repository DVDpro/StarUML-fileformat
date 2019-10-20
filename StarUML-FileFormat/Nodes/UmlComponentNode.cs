using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlComponentNode : UmlNode
    {
        private const string NodeTypeName = "UMLComponent";

        public UmlComponentNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }
}
