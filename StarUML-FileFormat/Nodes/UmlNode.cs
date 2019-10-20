using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public abstract class UmlNode : Node
    {
        public string Stereotype { get; set; }

        public UmlNodeVisibility? Visibility { get; set; }

        private const string StereotypePropertyName = "stereotype";
        private const string VisibilityPropertyName = "visibility";

        protected UmlNode(string typeName) : base(typeName)
        {

        }

        internal override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(StereotypePropertyName, out var stereotypeProperty))
            {
                Stereotype = stereotypeProperty.GetString();
            }
            if (element.TryGetProperty(VisibilityPropertyName, out var visibilityProperty))
            {
                if (Enum.TryParse<UmlNodeVisibility>(visibilityProperty.GetString(), true, out var visibilityResolved))
                {
                    Visibility = visibilityResolved;
                }
                else
                {
                    Visibility = null;
                }
            }
        }

        internal virtual void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            if (!string.IsNullOrEmpty(Stereotype))
            {
                writer.WritePropertyName(StereotypePropertyName);
                writer.WriteStringValue(Stereotype);
            }
            if (Visibility != null)
            {
                writer.WritePropertyName(VisibilityPropertyName);
                writer.WriteStringValue(Visibility.ToString().ToLowerInvariant());
            }            
        }
    }
}
