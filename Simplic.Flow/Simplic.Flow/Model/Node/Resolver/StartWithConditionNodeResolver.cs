using System;

namespace Simplic.Flow
{
    public class StartWithConditionNodeResolver : INodeResolver
    {
        public Node Create(Guid id, bool isStartNode)
        {
            return new StartWithConditionNode { Id = id };
        }
    }
}