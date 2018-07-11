using System;
using System.Collections.Generic;

namespace Simplic.Flow
{
    public abstract class Node
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public abstract string FriendlyName { get; }
        public T GetValue<T>(DataPin inPin) { return default(T); }
        public IList<T> GetListValue<T>(DataPin inPin) { return null; }
        public void SetValue(DataPin pin, object value) { }
        // public void Notify(Node node) { }
        public void EnqueueNode(Node node, params PinScope[] scope) { }
    }
}
