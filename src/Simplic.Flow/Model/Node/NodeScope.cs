using Newtonsoft.Json;
using Simplic.Flow.Event;
using System;

namespace Simplic.Flow
{
    public class NodeScope<T> where T : BaseNode
    {
        [JsonIgnore]
        public T Node { get; set; }
        public Guid NodeId { get; set; }
        public DataPinScope Scope { get; set; }
    }
}
