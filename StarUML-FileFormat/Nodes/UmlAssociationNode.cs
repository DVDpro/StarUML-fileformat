using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlAssociationNode : UmlNode
    {
        private const string NodeTypeName = "UMLAssociation";

        public UmlAssociationNode(INode parent) : base(NodeTypeName, parent)
        {
        }
    }
}
