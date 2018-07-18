using System;

namespace Simplic.Flow.Node
{
    public class ForeachNodeResolver : INodeResolver
    {        
        public BaseNode Create(Guid id, bool isStartNode)
        {
            return new ForeachNode { Id = id };
        }
    }
}