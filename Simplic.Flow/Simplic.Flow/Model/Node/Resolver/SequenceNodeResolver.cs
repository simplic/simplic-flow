using System;

namespace Simplic.Flow
{
    public class SequenceNodeResolver : INodeResolver
    {
        public Node Create(Guid id, bool isStartNode)
        {
            return new SequenceNode { Id = id };
        }
    }
}