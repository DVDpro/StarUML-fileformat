using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public abstract class Node : INode
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Documentation { get; set; }

        public INode Parent { get; set; }

        public string TypeName { get; }

        private const string TypePropertyName = "_type";
        private const string IdPropertyName = "_id";
        private const string NamePropertyName = "name";
        private const string DocumentationPropertyName = "documentation";

        protected Node(string typeName)
        {
            TypeName = typeName;
        }

        internal virtual void InitializeFromElement(JsonElement element)
        {
            var typeName = element.GetProperty(TypePropertyName).GetString();
            if (typeName != TypeName)
            {
                throw new InvalidOperationException($"Invalid node. Expected {TypeName} but actual is {typeName}.");
            }

            Id = element.GetProperty(IdPropertyName).GetString();
            if (element.TryGetProperty(NamePropertyName, out var nameProperty))
            {
                Name = nameProperty.GetString();
            }
            if (element.TryGetProperty(DocumentationPropertyName, out var docProperty))
            {
                Documentation = docProperty.GetString();
            }
        }

        internal virtual void Write(Utf8JsonWriter writer)
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
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is INode node)
            {
                return node.Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Node a, Node b)
        {
            return a.Id == b.Id;
        }

        public static bool operator !=(Node a, Node b)
        {
            return a.Id != b.Id;
        }
    }
}
