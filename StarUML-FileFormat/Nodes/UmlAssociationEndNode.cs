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

        private const string AggregationPropertyName = "aggregation";

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
        }

        public override void Write(Utf8JsonWriter writer)
        {
            base.Write(writer);
            writer.WriteString(AggregationPropertyName, EnumHelper<UmlAssociationEndAggregation>.ToString(Aggregation));
        }
    }
}
