using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public interface INode
    {
        string Id { get; set; }

        string Name { get; set; }

        string Documentation { get; set; }

        INode Parent { get; set; }

        string TypeName { get; }
    }
}
