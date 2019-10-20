using System.Threading.Tasks;
using Xunit;

namespace StarUML_FileFormat.Tests
{
    public class ProjectTest
    {
        [Fact]
        public async Task LoadTestProject()
        {
            var fileName = ".\\test-project.mdj";
            var project = await DVDpro.StarUML.FileFormat.Project.LoadAsync(fileName);
            Assert.Equal("AAAAAAFF+h6SjaM2Hec=", project.Node.Id);
            Assert.Equal("TestProject", project.Node.Name);
            Assert.Equal("its the project", project.Node.Documentation);
        }

        [Fact]
        public async Task WriteTestProject()
        {
            var project = new DVDpro.StarUML.FileFormat.Project();
            project.Node.Id = "AAAAAAFF+h6SjaM2Hec=";
            project.Node.Name = "TestProject";
            project.Node.Documentation = "its the project";
            project.Node.Author = "DVDpro";
            project.Node.Version = "1";

            var patternProject = await DVDpro.StarUML.FileFormat.Project.LoadAsync(".\\test-project.mdj");
            patternProject.Node.OwnedElements = null;
            var patternContent = patternProject.ToString();

            var newContent = project.ToString();
            Assert.Equal(patternContent, newContent);
        }
    }
}
