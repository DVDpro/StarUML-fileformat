using DVDpro.StarUML.FileFormat.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Generators
{
    public static class CSharpHelper
    {
        public static List<string> GetNamespaceForNode(Nodes.INode node)
        {
            var nsParts = new List<string>();
            var current = node;
            do
            {
                switch (current)
                {
                    case Nodes.UmlModelNode modul:
                    case Nodes.UmlPackageNode package:
                    case Nodes.UmlSubsystemNode subsystem:
                        nsParts.Insert(0, current.Name);
                        break;
                }

                current = node.Parent;
            }
            while (current != null);
            return nsParts;
        }

        internal static string ResolveType(UmlAttributeNode attributeNode)
        {
            if (attributeNode.Type.IsNodeReference)
            {
                return attributeNode.Type.NodeReference.Name;
            }
            else
            {
                return attributeNode.Type.Name;
            }
        }

        public static string ConvertVisibility(Nodes.UmlNodeVisibility? visibility, bool canBePrivate = false, bool canBeProtected = false)
        {
            if (visibility == null) return "public";

            switch (visibility.Value)
            {
                case Nodes.UmlNodeVisibility.Public:
                    return "public";
                case Nodes.UmlNodeVisibility.Protected:
                    if (canBeProtected)
                        return "protected";
                    else
                        return "public";
                case Nodes.UmlNodeVisibility.Package:
                    return "internal";
                case Nodes.UmlNodeVisibility.Private:
                    if (canBePrivate)
                        return "private";
                    else
                        return "internal";
                default:
                    return "public";
            }
        }
    }
}
