using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public enum UmlNodeVisibility
    {
        [Description("public")]
        Public = 0x1,
        [Description("private")]
        Private = 0x2,
        [Description("protected")]
        Protected = 0x4,
        [Description("package")]
        Package = 0x8
    }
}
