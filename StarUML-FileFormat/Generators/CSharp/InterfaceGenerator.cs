using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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
                foreach (var child in interfaceNode.Children)
                {
                    
                }
            }
        }
    }
}
