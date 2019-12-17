using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DVDpro.StarUML.FileFormat.Nodes;

namespace DVDpro.StarUML.FileFormat.Generators.CSharp
{
    public class InterfaceGenerator
    {
        public void Generate(CSharpWriter stream, Nodes.UmlInterfaceNode interfaceNode)
        {
            stream.WriteSummary(interfaceNode.Documentation);
            var baseNames = interfaceNode.GetGeneralBaseNodes().Select(r=>r.Name).ToArray();
            var baseDeclaration = baseNames.Length == 0 ? string.Empty : $" : {string.Join(", ", baseNames)}";
            stream.WriteCodeLine($"{CSharpHelper.ConvertVisibility(interfaceNode.Visibility)} interface {interfaceNode.Name}{baseDeclaration}");
            using (var interScope = stream.CreateIndentScope())
            {
                if (interfaceNode.Attributes != null)
                {
                    foreach (var attr in interfaceNode.Attributes)
                    {
                        WriteAttributeAsProperty(stream, attr);
                    }
                }
            }
        }

        private void WriteAttributeAsProperty(CSharpWriter stream, UmlAttributeNode attr)
        {
            var propertyGenerator = new PropertyGenerator();
            propertyGenerator.GenerateForInterface(stream, attr);
        }
    }
}
