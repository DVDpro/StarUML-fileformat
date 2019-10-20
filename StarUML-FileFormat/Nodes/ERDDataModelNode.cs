using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class ERDDataModelNode : Node
    {
        private const string NodeTypeName = "ERDDataModel";

        public ERDDataModelNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }
}
