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
    }
}
