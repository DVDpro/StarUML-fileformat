using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Generators
{
    public class EnumGenerator
    {
        private string ConvertVisibility(Nodes.UmlNodeVisibility? visibility)
        {
            if (visibility == null) return "public";

            switch (visibility.Value)
            {
                case Nodes.UmlNodeVisibility.Public:
                case Nodes.UmlNodeVisibility.Protected:
                    return "public";
                case Nodes.UmlNodeVisibility.Package:
                case Nodes.UmlNodeVisibility.Private:
                    return "internal";
                default:
                    return "public";
            }
        }

        public void Generate(CSharpFileStream stream, Nodes.UmlEnumerationNode enumNode)
        {
            stream.WriteSummary(enumNode.Documentation);
            if (enumNode.Tags.Any(r => r.Name == "Flags"))
            {
                stream.WriteCodeLine("[Flags]");
            }
            stream.WriteCodeLine($"{ConvertVisibility(enumNode.Visibility)} enum {enumNode.Name}");
            using (var enumScope = stream.CreateIndentScope())
            {
                var litCount = enumNode.Literals.Count;
                for (var i=0; i<litCount; i++)
                {
                    var lit = enumNode.Literals[i];
                    var litDelimiter = litCount > i + 1 ? "," : string.Empty;

                    stream.WriteLine(); // This is only for beauty output
                    stream.WriteSummary(lit.Documentation);
                    var valueTag = lit.Tags.FirstOrDefault(r => r.Name == "Value");
                    if (valueTag != null)
                    {                        
                        stream.WriteCodeLine($"{lit.Name} = {valueTag.Value}{litDelimiter}");
                    }
                    else
                    {
                        stream.WriteCodeLine($"{lit.Name}{litDelimiter}");
                    }
                }
            }
        }
    }
}
