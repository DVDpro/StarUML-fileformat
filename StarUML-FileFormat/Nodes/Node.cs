using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    public abstract class Node : INode
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Documentation { get; set; }

        public INode Parent { get; }

        public string TypeName { get; }

        public List<INode> OwnedElements { get; set; }

        public List<TagNode> Tags { get; set; }

        public ProjectNode Project { get; }

        private const string IdPropertyName = "_id";
        private const string ParentPropertyName = "_parent";
        private const string RefPropertyName = "$ref";
        private const string NamePropertyName = "name";
        private const string DocumentationPropertyName = "documentation";
        private const string OwnedElementsPropertyName = "ownedElements";
        private const string TagsPropertyName = "tags";

        protected Node(string typeName, INode parent)
        {
            TypeName = typeName;
            Parent = parent;
            Project = TopParent as ProjectNode;
        }

        public virtual void InitializeFromElement(JsonElement element)
        {
            var typeName = NodeFactory.GetElementType(element);
            if (typeName != TypeName)
            {
                throw new InvalidOperationException($"Invalid node. Expected {TypeName} but actual is {typeName}.");
            }

            Id = element.GetProperty(IdPropertyName).GetString();
            if (element.TryGetProperty(NamePropertyName, out var nameProperty))
            {
                Name = nameProperty.GetString();
            }
            if (element.TryGetProperty(DocumentationPropertyName, out var docProperty))
            {
                Documentation = docProperty.GetString();
            }

            if (element.TryGetProperty(OwnedElementsPropertyName, out var ownedElements))
            {
                OwnedElements = new List<INode>();
                foreach (var ownedElement in ownedElements.EnumerateArray())
                {
                    var ownedNode = NodeFactory.CreateAndInitializeFromElement(this, ownedElement);
                    OwnedElements.Add(ownedNode);
                }
            }

            Tags = new List<TagNode>();
            if (element.TryGetProperty(TagsPropertyName, out var tagElements))
            {
                foreach (var tagElement in tagElements.EnumerateArray())
                {
                    var tagNode = (TagNode)NodeFactory.CreateAndInitializeFromElement(this, tagElement);
                    Tags.Add(tagNode);
                }
            }
        }

        public virtual void Write(Utf8JsonWriter writer)
        {
            writer.WriteString(NodeFactory.TypePropertyName, TypeName);
            writer.WriteString(IdPropertyName, Id);
            if (Name != null)
            {
                writer.WriteString(NamePropertyName, Name);
            }
            if (Parent != null)
            {
                writer.WritePropertyName(ParentPropertyName);
                writer.WriteStartObject();
                writer.WriteString(RefPropertyName, Parent.Id);
                writer.WriteEndObject();
            }
            if (OwnedElements != null)
            {
                writer.WritePropertyName(OwnedElementsPropertyName);
                writer.WriteStartArray();
                foreach (var node in OwnedElements)
                {
                    writer.WriteStartObject();
                    node.Write(writer);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            if (Documentation != null)
            {
                writer.WriteString(DocumentationPropertyName, Documentation);
            }
        }

        public override string ToString()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is INode node)
            {
                return node.Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Node a, Node b)
        {
            return a?.Id == b?.Id;
        }

        public static bool operator !=(Node a, Node b)
        {
            return a?.Id != b?.Id;
        }

        public virtual IEnumerable<INode> Children
        {
            get
            {
                if (OwnedElements == null) return new INode[0];
                return OwnedElements;
            }
        }

        public INode FindNodeById(string nodeId)
        {
            INode result = null;
            if (Children == null) return result;

            foreach (var childNode in Children)
            {
                if (childNode.Id == nodeId)
                {
                    result = childNode;
                    break;
                }

                result = childNode.FindNodeById(nodeId);
                if (result != null)
                    break;
            }
            return result;
        }
                
        public IEnumerable<TNode> GetChildrenByType<TNode>()
            where TNode : INode
        {
            if (Children == null) return new TNode[0];
            var typeNode = typeof(TNode);            
            return Children.Where(r => r is TNode).Cast<TNode>();
        }

        public INode TopParent
        {
            get
            {
                INode result = null;
                if (Parent != null)
                {
                    INode cNode = this;
                    do
                    {
                        if (cNode.Parent == null)
                        {
                            result = cNode;
                            break;
                        }
                        cNode = cNode.Parent;
                    }
                    while (cNode != null);
                }
                return result;
            }
        }

        public Dictionary<string, INode> GetAllNodes()
        {
            var result = new Dictionary<string, INode>
            {
                { this.Id, this }
            };
            foreach (var child in Children)
            {
                result.Add(child.Id, child);
                AddAllChildNodes(child, result);
            }
            return result;
        }

        private void AddAllChildNodes(INode child, Dictionary<string, INode> result)
        {
            foreach (var subChild in child.Children)
            {
                result.Add(subChild.Id, subChild);
                AddAllChildNodes(subChild, result);
            }
        }
    }
}
