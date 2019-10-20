using System;
using System.Collections.Generic;
using System.Text;

namespace DVDpro.StarUML.FileFormat.Nodes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class NodeTypeAttribute : Attribute
    {
        public string TypeName { get; }

        public NodeTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}
