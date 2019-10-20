using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public enum UmlAssociationEndAggregation
    {
        [Description("composite")]
        Composite,

        [Description("shared")]
        Aggregation,
    }
}
