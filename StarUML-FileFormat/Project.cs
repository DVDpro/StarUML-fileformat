using DVDpro.StarUML.FileFormat.Nodes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DVDpro.StarUML.FileFormat
{
    public class Project
    {
        public ProjectNode Node { get; private set; }

        public Project()
        {
            Node = new ProjectNode();
        }
                
        public static async Task<Project> LoadAsync(string fileName, CancellationToken cancellationToken = default)
        {
            using (var fs = System.IO.File.Open(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                return await LoadAsync(fs, cancellationToken);
            }
        }

        public static async Task<Project> LoadAsync(System.IO.Stream stream, CancellationToken cancellationToken = default)
        {
            var options = new JsonDocumentOptions
            {
            };

            var node = new ProjectNode();
            using (JsonDocument document = await JsonDocument.ParseAsync(stream, options, cancellationToken))
            {
                node.InitializeFromElement(document.RootElement);
            }
            var proj = new Project
            {
                Node = node
            };
            return proj;
        }

        public void Save(string fileName)
        {
            if (Node == null) throw new InvalidOperationException($"Can't save when {nameof(ProjectNode)} is null.");

            using (var fs = System.IO.File.Open(fileName, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                Save(fs);
            }
        }

        public void Save(System.IO.Stream stream)
        {
            if (Node == null) throw new InvalidOperationException($"Can't save when {nameof(ProjectNode)} is null.");

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var writer = new Utf8JsonWriter(stream, options))
            {
                writer.WriteStartObject();
                Node.Write(writer);
                writer.WriteEndObject();
            }
        }

        public override string ToString()
        {
            using (var memStream = new System.IO.MemoryStream())
            {
                Save(memStream);
                memStream.Flush();
                memStream.Seek(0, System.IO.SeekOrigin.Begin);
                using (var reader = new System.IO.StreamReader(memStream, Encoding.UTF8, true))
                {
                    return reader.ReadToEnd();
                }
            }
            
        }
    }
}
