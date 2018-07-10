using System;
using System.Collections.Generic;

namespace Simplic.Flow
{
    public class Flow
    {
        public T CreateNode<T>() where T : Node, new()
        {
            var node = new T();
            Nodes.Add(node);
            return node;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Node> Nodes { get; set; } = new List<Node>();
    }
}
