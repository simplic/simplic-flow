using System;

namespace Simplic.Flow
{
    public abstract class BaseNode
    {
        public Guid Id { get; set; }
        public abstract string Name { get; }
        public abstract string FriendlyName { get; }
    }
}
