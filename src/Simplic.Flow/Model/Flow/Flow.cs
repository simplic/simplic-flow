using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.Flow
{
    public class Flow
    {
        public T CreateNode<T>() where T : BaseNode, new()
        {
            var node = new T();
            Nodes.Add(node);
            return node;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<BaseNode> Nodes { get; set; } = new List<BaseNode>();
        public IList<FlowVariable> Variables { get; set; } = new List<FlowVariable>();
    }
}
