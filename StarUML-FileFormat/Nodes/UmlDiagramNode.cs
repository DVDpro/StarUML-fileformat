using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public abstract class UmlDiagramNode : Node
    {
        public bool DefaultDiagram { get; set; }

        private const string DefaultDiagramPropertyName = "defaultDiagram";

        protected UmlDiagramNode(string typeName, INode parent) : base(typeName, parent)
        {

        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            // TODO: Implement ownedViews
            if (element.TryGetProperty(DefaultDiagramPropertyName, out var defaultDiagramProperty))
            {
                DefaultDiagram = defaultDiagramProperty.GetBoolean();
            }           
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            // TODO: Implement ownedViews
            writer.WriteBoolean(DefaultDiagramPropertyName, DefaultDiagram);
        }
    }
}
