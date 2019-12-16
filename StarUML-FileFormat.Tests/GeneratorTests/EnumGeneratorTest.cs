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
    public class EnumGeneratorTest
    {
        [Fact]
        public async Task ExecuteToFileTest()
        {
            var fileName = ".\\test-project.mdj";
            var project = await DVDpro.StarUML.FileFormat.Project.LoadAsync(fileName);

            var tmpFile = System.IO.Path.GetTempFileName();
            try
            {
                var generator = new EnumGenerator();
                using (var outStream = new CSharpWriter(tmpFile))
                {
                    foreach (var model in project.GetChildrenByType<DVDpro.StarUML.FileFormat.Nodes.UmlModelNode>())
                    {
                        var enumNode = model.GetChildrenByType<DVDpro.StarUML.FileFormat.Nodes.UmlEnumerationNode>().First(r => r.Name == "MasterEnum");
                        generator.Generate(outStream, enumNode);
                    }                            
                }
                var output = await System.IO.File.ReadAllTextAsync(tmpFile);
                Assert.Equal("/// <summary>\r\n/// Test enum comment\r\n/// multiline\r\n/// </summary>\r\npublic enum MasterEnum\r\n{\r\n    /// <summary>test literal comment</summary>\r\n    Literal1 = 0x1,\r\n\r\n    Literal2\r\n}\r\n", output);                    
            }
            finally
            {
                System.IO.File.Delete(tmpFile);
            }
        }

        [Fact]
        public async Task ExecuteInMemoryTest()
        {
            var fileName = ".\\test-project.mdj";
            var project = await DVDpro.StarUML.FileFormat.Project.LoadAsync(fileName);

            var generator = new EnumGenerator();
            using (var ms = new System.IO.MemoryStream())
            {
                using (var outStream = new CSharpWriter(ms))
                {
                    foreach (var model in project.GetChildrenByType<DVDpro.StarUML.FileFormat.Nodes.UmlModelNode>())
                    {
                        var enumNode = model.GetChildrenByType<DVDpro.StarUML.FileFormat.Nodes.UmlEnumerationNode>().First(r => r.Name == "MasterEnum");
                        generator.Generate(outStream, enumNode);
                    }

                    outStream.Flush();
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    using (var reader = new System.IO.StreamReader(ms))
                    {
                        var output = reader.ReadToEnd();
                        Assert.Equal("/// <summary>\r\n/// Test enum comment\r\n/// multiline\r\n/// </summary>\r\npublic enum MasterEnum\r\n{\r\n    /// <summary>test literal comment</summary>\r\n    Literal1 = 0x1,\r\n\r\n    Literal2\r\n}\r\n", output);
                    }
                }
                
            }
        }
    }
}
