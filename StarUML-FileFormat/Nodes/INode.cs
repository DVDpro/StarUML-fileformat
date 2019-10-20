using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public interface INode
    {
        string Id { get; set; }

        string Name { get; set; }

        string Documentation { get; set; }

        INode Parent { get; }

        ProjectNode Project { get; }

        string TypeName { get; }

        List<INode> OwnedElements { get; set; }

        void InitializeFromElement(JsonElement element);

        void Write(Utf8JsonWriter writer);

        INode FindNodeById(string nodeId);

        IEnumerable<INode> Children { get; }
    }
}
