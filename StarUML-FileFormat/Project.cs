using DVDpro.StarUML.FileFormat.Nodes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DVDpro.StarUML.FileFormat
{
    public class Project : ProjectNode
    {

        public Project() : base()
        {
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

            var proj = new Project();
            using (JsonDocument document = await JsonDocument.ParseAsync(stream, options, cancellationToken))
            {
                proj.InitializeFromElement(document.RootElement);
            }            
            return proj;
        }

        public void Save(string fileName)
        {
            using (var fs = System.IO.File.Open(fileName, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                Save(fs);
            }
        }

        public void Save(System.IO.Stream stream)
        {
            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var writer = new Utf8JsonWriter(stream, options))
            {
                writer.WriteStartObject();
                Write(writer);
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
