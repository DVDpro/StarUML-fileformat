using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DVDpro.StarUML.FileFormat.Generators.CSharp;
using DVDpro.StarUML.FileFormat.Generators;

namespace StarUML_FileFormat.Tests.GeneratorTests
{
    public class InterfaceGeneratorTest
    {
        [Fact]
        public async Task ExecuteToFileTest()
        {
            var fileName = ".\\test-project.mdj";
            var project = await DVDpro.StarUML.FileFormat.Project.LoadAsync(fileName);

            var tmpFile = System.IO.Path.GetTempFileName();
            try
            {
                var generator = new InterfaceGenerator();
                using (var outStream = new CSharpWriter(tmpFile))
                {
                    foreach (var model in project.GetChildrenByType<DVDpro.StarUML.FileFormat.Nodes.UmlModelNode>())
                    {
                        var node = model.GetChildrenByType<DVDpro.StarUML.FileFormat.Nodes.UmlInterfaceNode>().First(r => r.Name == "Interface1");
                        generator.Generate(outStream, node);
                    }                            
                }
                var output = await System.IO.File.ReadAllTextAsync(tmpFile);
                Assert.Equal("public interface Interface1 : Interface1Base\r\n{\r\n}\r\n", output);                    
            }
            finally
            {
                System.IO.File.Delete(tmpFile);
            }
        }
    }
}
