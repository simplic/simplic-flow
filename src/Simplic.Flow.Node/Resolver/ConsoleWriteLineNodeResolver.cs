using System;

namespace Simplic.Flow.Node
{
    public class ConsoleWriteLineNodeResolver : INodeResolver
    {
        public BaseNode Create(Guid id, bool isStartNode)
        {
            return new ConsoleWriteLineNode { Id = id };
        }
    }
}