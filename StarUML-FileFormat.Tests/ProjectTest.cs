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
            Assert.Equal("AAAAAAFF+h6SjaM2Hec=", project.Id);
            Assert.Equal("TestProject", project.Name);
            Assert.Equal("its the project", project.Documentation);
            Assert.Null(project.ParentNode);
        }

        [Fact]
        public async Task WriteTestProject()
        {
            var project = new DVDpro.StarUML.FileFormat.Project();
            project.Id = "AAAAAAFF+h6SjaM2Hec=";
            project.Name = "TestProject";
            project.Documentation = "its the project";
            project.Author = "DVDpro";
            project.Version = "1";

            var patternProject = await DVDpro.StarUML.FileFormat.Project.LoadAsync(".\\test-project.mdj");
            var patternContent = patternProject.ToString();

            var newContent = project.ToString();
            Assert.Equal(patternContent, newContent);
        }
    }
}
