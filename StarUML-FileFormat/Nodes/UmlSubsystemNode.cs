using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{

    [NodeType(NodeTypeName)]
    public class UmlSubsystemNode : UmlNode
    {
        private const string NodeTypeName = "UMLSubsystem";

        public UmlSubsystemNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }
    
}
