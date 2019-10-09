using System.IO;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat
{
    public class Node
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Documentation { get; set; }

        public Node ParentNode { get; set; }

        private string TypeName { get; }

        private const string TypePropertyName = "_type";
        private const string IdPropertyName = "_id";
        private const string NamePropertyName = "name";
        private const string DocumentationPropertyName = "documentation";

        protected Node(string typeName)
        {
            TypeName = typeName;
        }

        protected void InitializeFromElement(System.Text.Json.JsonElement json)
        {
            Id = json.GetProperty(IdPropertyName).GetString();
            if (json.TryGetProperty(NamePropertyName, out var nameProperty))
            {
                Name = nameProperty.GetString();
            }
            if (json.TryGetProperty(DocumentationPropertyName, out var docProperty))
            {
                Documentation = docProperty.GetString();
            }
        }

        protected virtual void Write(Utf8JsonWriter writer)
        {
            writer.WritePropertyName(TypePropertyName);
            writer.WriteStringValue(TypeName);
            writer.WritePropertyName(IdPropertyName);
            writer.WriteStringValue(Id);
            if (Name != null)
            {
                writer.WritePropertyName(NamePropertyName);
                writer.WriteStringValue(Name);
            }

            if (Documentation != null)
            {
                writer.WritePropertyName(DocumentationPropertyName);
                writer.WriteStringValue(Documentation);
            }
        }

        public override string ToString()
        {
            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    this.Write(writer);
                    writer.WriteEndObject();
                }
                string json = Encoding.UTF8.GetString(stream.ToArray());
                return json;
            }
        }
    }
}
