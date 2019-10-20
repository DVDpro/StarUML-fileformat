using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlInterfaceNode : UmlNode
    {
        private const string NodeTypeName = "UMLInterface";

        public UmlInterfaceNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }
}
