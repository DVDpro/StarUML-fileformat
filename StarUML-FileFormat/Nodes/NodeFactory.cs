using System;
using System.Collections.Generic;
using System.Text.Json;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    internal class NodeFactory
    {
        private static Dictionary<string, Type> _typeRegister;
        private static readonly object _typeLoadLckObj = new object();

        private static void LoadNodeTypes()
        {
            if (_typeRegister != null) return;

            lock (_typeLoadLckObj)
            {
                if (_typeRegister != null) return;

                var register = new Dictionary<string, Type>();
                var iNodeType = typeof(INode);
                foreach (var type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (iNodeType.IsAssignableFrom(type) && iNodeType != type && !type.IsAbstract && type.IsClass)
                    {
                        var typeNameAttr = type.GetCustomAttributes(typeof(NodeTypeAttribute), false);
                        if (typeNameAttr?.Length == 1)
                        {
                            register.Add(((NodeTypeAttribute)typeNameAttr[0]).TypeName, type);
                        }
                    }
                }
                _typeRegister = register;
            }
        }

        internal static INode Create(string typeName, INode parent)
        {
            LoadNodeTypes();
            if (!_typeRegister.TryGetValue(typeName, out var nodeType))
            {
                throw new NotSupportedException($"Unsupported node type {typeName}");
            }

            var node = (INode)Activator.CreateInstance(nodeType, parent);
            return node;
        }

        internal static INode CreateAndInitializeFromElement(string typeName, INode parent, JsonElement ownedElement)
        {
            var node = Create(typeName, parent);
            node.InitializeFromElement(ownedElement);
            return node;
        }
    }
}
