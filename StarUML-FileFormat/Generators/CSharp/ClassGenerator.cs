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
                var autoInitializeConstructorNodes = new List<INode>();

                if (classNode.Attributes != null)
                {
                    for (var attrIndex = 0; attrIndex < classNode.Attributes.Count; attrIndex++)
                    {
                        if (attrIndex > 0) stream.WriteLine();
                        var attr = classNode.Attributes[attrIndex];
                        WriteAttributeAsProperty(stream, attr);
                        if (attr.IsReadOnly)
                        {
                            autoInitializeConstructorNodes.Add(attr);
                        }
                    }

                    foreach (var assocEndNode in classNode.GetAssociationEnds(false, true))
                    {
                        stream.WriteLine();
                        WriteAssociationEndProperty(stream, assocEndNode);
                        if (assocEndNode.IsReadOnly)
                        {
                            autoInitializeConstructorNodes.Add(assocEndNode);
                        }
                    }
                }
                if (autoInitializeConstructorNodes.Any())
                {
                    stream.WriteLine();
                    stream.WriteCodeLine($"public {classNode.Name}()");
                    using (var ctorScope = stream.CreateIndentScope())
                    {
                        foreach (var autoNode in autoInitializeConstructorNodes)
                        {
                            stream.WriteCodeLine($"{autoNode.Name} = default;");
                        }
                    }
                }
                
            }
        }

        private void WriteAssociationEndProperty(CSharpWriter stream, UmlAssociationEndNode assocEndNode)
        {
            var propertyGenerator = new PropertyGenerator();
            propertyGenerator.GenerateForClass(stream, assocEndNode);
        }

        private void WriteAttributeAsProperty(CSharpWriter stream, UmlAttributeNode attr)
        {
            var propertyGenerator = new PropertyGenerator();
            propertyGenerator.GenerateForClass(stream, attr);
        }
    }
}
