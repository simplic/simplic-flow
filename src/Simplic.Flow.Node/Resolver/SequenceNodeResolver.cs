using System;

namespace Simplic.Flow.Node
{
    public class SequenceNodeResolver : INodeResolver
    {
        public BaseNode Create(Guid id, bool isStartNode)
        {
            return new SequenceNode { Id = id };
        }
    }
}