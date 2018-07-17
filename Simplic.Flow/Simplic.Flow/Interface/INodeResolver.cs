using System;

namespace Simplic.Flow
{
    public interface INodeResolver
    {
        BaseNode Create(Guid id, bool isStartNode);
    }
}
