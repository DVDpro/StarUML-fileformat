using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public abstract class UmlNode : Node
    {
        public string Stereotype { get; set; }

        public bool IsReadOnly { get; set; }

        public UmlNodeVisibility? Visibility { get; set; }

        private const string StereotypePropertyName = "stereotype";
        private const string VisibilityPropertyName = "visibility";
        private const string ReadOnlyPropertyName = "isReadOnly";

        protected UmlNode(string typeName, INode parent) : base(typeName, parent)
        {

        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(StereotypePropertyName, out var stereotypeProperty))
            {
                Stereotype = stereotypeProperty.GetString();
            }
            if (element.TryGetProperty(VisibilityPropertyName, out var visibilityProperty))
            {
                if (EnumHelper<UmlNodeVisibility>.TryParse(visibilityProperty.GetString(), out var visibilityResolved))
                {
                    Visibility = visibilityResolved;
                }
                else
                {
                    Visibility = null;
                }
            }
            if (element.TryGetProperty(ReadOnlyPropertyName, out var readOnlyProperty))
            {
                IsReadOnly = readOnlyProperty.GetBoolean();
            }
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            if (!string.IsNullOrEmpty(Stereotype))
            {
                writer.WriteString(StereotypePropertyName, Stereotype);
            }
            if (Visibility != null)
            {
                writer.WriteString(VisibilityPropertyName, EnumHelper<UmlNodeVisibility>.ToString(Visibility.Value));
            }
            if (IsReadOnly)
            {
                writer.WriteBoolean(ReadOnlyPropertyName, IsReadOnly);
            }
        }

        

        public IEnumerable<Nodes.UmlInterfaceNode> GetInterfaceRealizationNodes()
        {
            foreach (var realizationLink in this.GetChildrenByType<Nodes.UmlInterfaceRealizationNode>())
            {
                yield return (Nodes.UmlInterfaceNode)realizationLink.Target.NodeReference;
            }
        }

        public IEnumerable<Nodes.INode> GetGeneralBaseNodes()
        {
            foreach (var generalLink in this.GetChildrenByType<Nodes.UmlGeneralizationNode>())
            {
                yield return generalLink.Target.NodeReference;
            }
        }
    }
}
