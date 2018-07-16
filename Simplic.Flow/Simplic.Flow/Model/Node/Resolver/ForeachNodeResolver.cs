using System;

namespace Simplic.Flow
{
    public class ForeachNodeResolver : INodeResolver
    {        
        public Node Create(Guid id, bool isStartNode)
        {
            return new ForeachNode { Id = id };
        }
    }
}