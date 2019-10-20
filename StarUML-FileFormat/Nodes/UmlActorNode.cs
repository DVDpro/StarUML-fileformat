using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlActorNode : UmlNode
    {
        private const string NodeTypeName = "UMLActor";

        public UmlActorNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }
}
