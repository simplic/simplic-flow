using System;

namespace Simplic.Flow
{
    public abstract class BaseNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public abstract string Name { get; }
        public abstract string FriendlyName { get; }
    }
}
