using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DVDpro.StarUML.FileFormat
{
    public class UmlClass
    {
        public UmlClass(Nodes.UmlClassNode classNode)
        {
            ClassNode = classNode;

            var generalizationNode = classNode.GetChildrenByType<Nodes.UmlGeneralizationNode>().SingleOrDefault();
            BaseClass = new UmlClass((Nodes.UmlClassNode)generalizationNode.Target.NodeReference);
        }

        public Nodes.UmlClassNode ClassNode { get; }

        public UmlClass BaseClass { get; }
    }
}
