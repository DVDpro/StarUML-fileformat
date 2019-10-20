using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{

    [NodeType(NodeTypeName)]
    public class UmlAssociationEndNode : UmlNode
    {
        private const string NodeTypeName = "UMLAssociationEnd";

        public UmlAssociationEndAggregation Aggregation { get; set; }
        
        public NodeTypeReference Reference { get; set; }

        private const string AggregationPropertyName = "aggregation";

        private const string ReferencePropertyName = "reference";

        public UmlAssociationNode ParentAssociation { get { return (UmlAssociationNode)Parent; } }

        public UmlAssociationEndNode(INode parent) : base(NodeTypeName, parent)
        {
        }

        public override void InitializeFromElement(JsonElement element)
        {
            base.InitializeFromElement(element);
            if (element.TryGetProperty(AggregationPropertyName, out var aggregationProperty))
            {
                if (EnumHelper<UmlAssociationEndAggregation>.TryParse(aggregationProperty.GetString(), out var aggregationResolved))
                {
                    Aggregation = aggregationResolved;
                }
                else
                {
                    throw new NotSupportedException($"Unsupported association end aggregation: {aggregationProperty.GetString()}");
                }
            }
            if (element.TryGetProperty(ReferencePropertyName, out var referenceProperty))
            {
                Reference = new NodeTypeReference(this, referenceProperty);
            }
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            writer.WriteString(AggregationPropertyName, EnumHelper<UmlAssociationEndAggregation>.ToString(Aggregation));
            if (Reference != null)
            {
                Reference.Write(ReferencePropertyName, writer);
            }
        }

        public UmlAssociationEndNode OpositeEnd
        {
            get
            {
                var assoc = (UmlAssociationNode)Parent;
                if (assoc.End1 == this)
                    return assoc.End2;
                else
                    return assoc.End1;
            }
        }
    }
}
