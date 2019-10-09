using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DVDpro.StarUML.FileFormat
{
    public class Project : Node
    {
        public string Author { get; set; }

        public string Version { get; set; }

        private const string AuthorPropertyName = "author";
        private const string VersionPropertyName = "version";

        public Project() : base("Project")
        {

        }

        public static async Task<Project> LoadAsync(string fileName, CancellationToken cancellationToken = default)
        {
            var options = new JsonDocumentOptions
            {
            };

            var node = new Project();
            using (var fs = System.IO.File.Open(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            using (JsonDocument document = await JsonDocument.ParseAsync(fs, options, cancellationToken))
            {
                node.InitializeFromElement(document.RootElement);

                if (document.RootElement.TryGetProperty(AuthorPropertyName, out var authorProp))
                {
                    node.Author = authorProp.GetString();
                }

                if (document.RootElement.TryGetProperty(VersionPropertyName, out var versionProp))
                {
                    node.Version = versionProp.GetString();
                }
            }
            return node;
        }

        protected override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            if (Author != null)
            {
                writer.WritePropertyName(AuthorPropertyName);
                writer.WriteStringValue(Author);
            }            

            if (Version != null)
            {
                writer.WritePropertyName(VersionPropertyName);
                writer.WriteStringValue(Version);
            }            
        }
    }
}
