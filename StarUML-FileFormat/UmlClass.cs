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
            if (generalizationNode != null)
                BaseClass = new UmlClass((Nodes.UmlClassNode)generalizationNode.Target.NodeReference);
        }

        public Nodes.UmlClassNode ClassNode { get; }

        public UmlClass BaseClass { get; }
        
        public IEnumerable<Nodes.UmlAssociationEndNode> GetAssociationEnds(bool end1, bool end2)
        {
            foreach (Nodes.UmlAssociationEndNode assocEnd in ClassNode.TopParent.GetAllNodes().Where(r=>r.Value is Nodes.UmlAssociationEndNode).Select(r=>r.Value))
            {
                if (assocEnd.Reference?.NodeId != ClassNode.Id) continue;

                if (!end1 && assocEnd == assocEnd.ParentAssociation.End1) continue;
                if (!end2 && assocEnd == assocEnd.ParentAssociation.End2) continue;

                yield return assocEnd;
            }
        }
    }
}
