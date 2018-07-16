using System;

namespace Simplic.Flow
{
    public interface INodeResolver
    {
        Node Create(Guid id, bool isStartNode);
    }
}
