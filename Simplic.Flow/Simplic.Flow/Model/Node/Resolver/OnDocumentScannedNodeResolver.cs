using System;

namespace Simplic.Flow
{
    public class OnDocumentScannedNodeResolver : INodeResolver
    {
        public Node Create(Guid id, bool isStartNode)
        {
            return new OnDocumentScannedNode { Id = id, IsStartEvent = isStartNode };
        }
    }
}
