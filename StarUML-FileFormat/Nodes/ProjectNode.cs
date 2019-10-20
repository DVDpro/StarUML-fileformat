using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace DVDpro.StarUML.FileFormat.Nodes
{
    public class ProjectNode : Node
    {
        private const string NodeTypeName = "Project";

        public string Author { get; set; }

        public string Version { get; set; }

        private const string AuthorPropertyName = "author";
        private const string VersionPropertyName = "version";

        public ProjectNode() : base(NodeTypeName)
        {
        }

        internal override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(AuthorPropertyName, out var authorProp))
            {
                Author = authorProp.GetString();
            }

            if (element.TryGetProperty(VersionPropertyName, out var versionProp))
            {
                Version = versionProp.GetString();
            }
        }

        internal override void Write(Utf8JsonWriter writer)
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
