using System;

namespace Simplic.Flow.Node
{
    public class StartWithConditionNodeResolver : INodeResolver
    {
        public BaseNode Create(Guid id, bool isStartNode)
        {
            return new StartWithConditionNode { Id = id };
        }
    }
}