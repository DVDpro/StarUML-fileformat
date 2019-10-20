using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    
    [NodeType(NodeTypeName)] 
    public class UmlClassNode : UmlNode
    {
        private const string NodeTypeName = "UMLClass";

        public UmlClassNode(INode parent) : base(NodeTypeName, parent)
        {
        }
    }
}
