using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlPrimitiveTypeNode : UmlNode
    {
        private const string NodeTypeName = "UMLPrimitiveType";

        public UmlPrimitiveTypeNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }
}
