using System;

namespace Simplic.Flow
{
    public abstract class BaseNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public abstract string FriendlyName { get; }
    }
}
