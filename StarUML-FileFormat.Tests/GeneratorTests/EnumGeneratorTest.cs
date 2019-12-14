using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StarUML_FileFormat.Tests.GeneratorTests
{
    public class EnumGeneratorTest
    {
        [Fact]
        public async Task ExecuteTest()
        {
            var fileName = ".\\test-project.mdj";
            var project = await DVDpro.StarUML.FileFormat.Project.LoadAsync(fileName);

            var tmpFile = System.IO.Path.GetTempFileName();
            try
            {
                var generator = new DVDpro.StarUML.FileFormat.Generators.EnumGenerator();
                using (var outStream = new DVDpro.StarUML.FileFormat.Generators.CSharpFileStream(tmpFile))
                {
                    foreach (var model in project.GetChildrenByType<DVDpro.StarUML.FileFormat.Nodes.UmlModelNode>())
                    {
                        var enumNode = model.GetChildrenByType<DVDpro.StarUML.FileFormat.Nodes.UmlEnumerationNode>().First(r => r.Name == "MasterEnum");
                        generator.Generate(outStream, enumNode);
                    }                            
                }
                var output = await System.IO.File.ReadAllTextAsync(tmpFile);
                Assert.Equal("/// <summary>\r\n/// Test enum comment\r\n/// multiline\r\n/// </summary>\r\npublic enum MasterEnum\r\n{\r\n    /// <summary>\r\n    /// test literal comment\r\n    /// </summary>\r\n    Literal1 = 0x1,\r\n    Literal2\r\n}\r\n", output);                    
            }
            finally
            {
                System.IO.File.Delete(tmpFile);
            }
        }
    }
}
