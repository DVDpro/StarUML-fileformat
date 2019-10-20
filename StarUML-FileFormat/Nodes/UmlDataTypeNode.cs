using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlDataTypeNode : UmlNode
    {
        private const string NodeTypeName = "UMLDataType";

        public UmlDataTypeNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }    
}
