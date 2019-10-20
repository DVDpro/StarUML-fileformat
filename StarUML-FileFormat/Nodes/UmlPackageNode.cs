using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public class UmlPackageNode : UmlNode
    {
        private const string NodeTypeName = "UMLPackage";

        public UmlPackageNode(INode parent) : base(NodeTypeName, parent)
        {

        }
    }
}
