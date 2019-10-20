using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlEnumerationLiteralNode : UmlNode
    {
        private const string NodeTypeName = "UMLEnumerationLiteral";

        public UmlEnumerationLiteralNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }
}
