using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [NodeType(NodeTypeName)]
    public sealed class TagNode : Node
    {
        private const string NodeTypeName = "Tag";

        public string Value { get; set; }

        public TagNode(INode parent) : base(NodeTypeName, parent)
        {

        }

        private const string ValuePropertyName = "value";

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(ValuePropertyName, out var valueProperty))
            {
                Value = valueProperty.GetString();
            }
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            if (Value != null)
            {
                writer.WriteString(ValuePropertyName, Value);
            }
        }
    }
}
