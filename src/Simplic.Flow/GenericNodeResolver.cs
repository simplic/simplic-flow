using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow
{
    public class GenericNodeResolver<T> : INodeResolver where T : ActionNode, new()
    {
        public BaseNode Create(Guid id, bool isStartNode)
        {
            var node = new T();
            node.Id = id;

            if (node is EventNode)
                (node as EventNode).IsStartEvent = isStartNode;

            return node;
        }
    }
}
