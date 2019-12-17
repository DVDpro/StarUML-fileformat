using DVDpro.StarUML.FileFormat.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Generators.CSharp
{
    public class PropertyGenerator
    {
        public void GenerateForInterface(CSharpWriter stream, Nodes.UmlAttributeNode attributeNode)
        {
            stream.WriteSummary(attributeNode.Documentation);
            if (attributeNode.IsReadOnly)
                stream.WriteCodeLine($"{CSharpHelper.ResolveType(attributeNode)} {attributeNode.Name} {{ get; }}");
            else
                stream.WriteCodeLine($"{CSharpHelper.ResolveType(attributeNode)} {attributeNode.Name} {{ get; set; }}");
        }

        internal void GenerateForClass(CSharpWriter stream, UmlAttributeNode attributeNode)
        {
            stream.WriteSummary(attributeNode.Documentation);
            var visibility = CSharpHelper.ConvertVisibility(attributeNode.Visibility, true, true);
            var attrType = CSharpHelper.ResolveType(attributeNode);
            if (attributeNode.IsReadOnly)
                stream.WriteCodeLine($"{visibility} {attrType} {attributeNode.Name} {{ get; private set; }}");
            else
                stream.WriteCodeLine($"{visibility} {attrType} {attributeNode.Name} {{ get; set; }}");
        }

        internal void GenerateForClass(CSharpWriter stream, UmlAssociationEndNode associationEndNode)
        {
            stream.WriteSummary(associationEndNode.Documentation);
            var visibility = CSharpHelper.ConvertVisibility(associationEndNode.Visibility, true, true);
            var opositeType = CSharpHelper.ResolveType(associationEndNode.OpositeEnd);

            if (associationEndNode.IsReadOnly)
                stream.WriteCodeLine($"{visibility} IEnumerable<{opositeType}> {associationEndNode.Name} {{ get; private set; }}");
            else
                stream.WriteCodeLine($"{visibility} IEnumerable<{opositeType}> {associationEndNode.Name} {{ get; set; }}");
        }
    }
}
