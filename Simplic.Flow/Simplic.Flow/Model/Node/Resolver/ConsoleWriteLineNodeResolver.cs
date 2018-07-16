using System;

namespace Simplic.Flow
{
    public class ConsoleWriteLineNodeResolver : INodeResolver
    {
        public Node Create(Guid id, bool isStartNode)
        {
            return new ConsoleWriteLineNode { Id = id };
        }
    }
}
