using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DVDpro.StarUML.FileFormat.Nodes;

namespace StarUML_FileFormat.Tests.NodeTests
{
    public class UmlClassNodeTest
    {
        [Fact]
        public async Task GetAssociationTest()
        {
            var fileName = ".\\test-project.mdj";
            var project = await DVDpro.StarUML.FileFormat.Project.LoadAsync(fileName);

            var model = project.GetChildrenByType<UmlModelNode>().Single();
            var node = model.GetChildrenByType<UmlClassNode>().First(r => r.Name == "Car");

            var assocEnd1 = node.GetAssociationEnds(true, false).ToList();
            var assocEnd2 = node.GetAssociationEnds(false, true).ToList();
            Assert.Empty(assocEnd1);
            Assert.Equal(3, assocEnd2.Count);
        }
    }
}
