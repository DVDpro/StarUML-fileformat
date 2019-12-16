using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DVDpro.StarUML.FileFormat.Nodes;

namespace DVDpro.StarUML.FileFormat.Generators.CSharp
{
    public class ClassGenerator
    {
        public void Generate(CSharpWriter stream, Nodes.UmlClassNode classNode, bool asPartial = true)
        {
            stream.WriteSummary(classNode.Documentation);
            var baseClass = classNode.BaseClass;
            var implInterfaces = classNode.GetInterfaceRealizationNodes().ToList();

            var baseNames = new List<string>();
            if (baseClass != null) baseNames.Add(baseClass.Name);
            baseNames.AddRange(implInterfaces.Select(r => r.Name));
            
            var baseDeclaration = baseNames.Count == 0 ? string.Empty : $" : {string.Join(", ", baseNames)}";

            var partialDefinition = asPartial ? " partial" : string.Empty;

            stream.WriteCodeLine($"{CSharpHelper.ConvertVisibility(classNode.Visibility)}{partialDefinition} class {classNode.Name}{baseDeclaration}");
            using (var interScope = stream.CreateIndentScope())
            {
                foreach (var attr in classNode.Attributes)
                {
                    WriteAttributeAsProperty(stream, attr);
                }
            }
        }

        private void WriteAttributeAsProperty(CSharpWriter stream, UmlAttributeNode attr)
        {
            var propertyGenerator = new PropertyGenerator();
            propertyGenerator.GenerateForClass(stream, attr);
        }
    }
}
