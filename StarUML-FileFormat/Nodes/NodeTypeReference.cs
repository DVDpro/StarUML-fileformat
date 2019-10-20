using System;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public class NodeTypeReference
    {
        public string NodeId
        {
            get { return _nodeId; }
            set
            {
                _nodeId = value;
                _name = null;
            }
        }
        private string _nodeId;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _nodeId = null;
            }
        }
        private string _name;

        public INode OwnerNode { get; }
        
        private const string RefPropertyName = "$ref";

        public NodeTypeReference(INode ownerNode, JsonElement element)
        {
            OwnerNode = ownerNode;
            if (element.ValueKind == JsonValueKind.Object && element.TryGetProperty(RefPropertyName, out var refProperty))
            {
                _nodeId = refProperty.GetString();
            }
            else
            {
                _name = element.GetString();
            }
        }

        public NodeTypeReference(INode ownerNode, INode referencedNode)
        {
            OwnerNode = ownerNode;
            _nodeId = referencedNode.Id;
        }

        internal void Write(string propertyName, Utf8JsonWriter writer)
        {
            writer.WritePropertyName(propertyName);
            if (NodeId != null)
            {
                writer.WriteStartObject();
                writer.WriteString(RefPropertyName, NodeId);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteStringValue(Name);
            }
        }

        public override string ToString()
        {
            if (NodeId != null)
            {
                return NodeReference.Name;
            }
            else
            {
                return Name;
            }
        }

        public bool IsNodeReference
        {
            get { return NodeId != null; }
        }

        public INode NodeReference
        {
            get
            {
                return OwnerNode.Project.FindNodeById(NodeId);
            }
        }

    }
}