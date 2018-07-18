using System;

namespace Simplic.Flow
{
    public class OnDocumentScannedNodeResolver : INodeResolver
    {
        public BaseNode Create(Guid id, bool isStartNode)
        {
            return new OnDocumentScannedNode { Id = id, IsStartEvent = isStartNode };
        }
    }
}
