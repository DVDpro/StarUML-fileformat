using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class ERDEntityNode : Node
    {
        private const string NodeTypeName = "ERDEntity";

        public ERDEntityNode(INode parent) : base(NodeTypeName, parent)
        {
        }
    }
}
