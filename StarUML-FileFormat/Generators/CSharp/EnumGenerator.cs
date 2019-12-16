using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Generators.CSharp
{
    /// <summary>
    /// Generate Csharp enum code.
    /// </summary>
    public class EnumGenerator
    {       

        public void Generate(CSharpWriter stream, Nodes.UmlEnumerationNode enumNode)
        {
            stream.WriteSummary(enumNode.Documentation);
            if (enumNode.Tags.Any(r => r.Name == "Flags"))
            {
                stream.WriteCodeLine("[Flags]");
            }
            stream.WriteCodeLine($"{CSharpHelper.ConvertVisibility(enumNode.Visibility)} enum {enumNode.Name}");
            using (var enumScope = stream.CreateIndentScope())
            {
                var litCount = enumNode.Literals.Count;
                for (var i=0; i<litCount; i++)
                {
                    var lit = enumNode.Literals[i];
                    var litDelimiter = litCount > i + 1 ? "," : string.Empty;

                    stream.WriteSummary(lit.Documentation);
                    var valueTag = lit.Tags.FirstOrDefault(r => r.Name == "Value");
                    if (valueTag != null)
                    {                        
                        stream.WriteCodeLine($"{lit.Name} = {valueTag.Value}{litDelimiter}");
                        stream.WriteLine(); // This is only for beauty output
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
