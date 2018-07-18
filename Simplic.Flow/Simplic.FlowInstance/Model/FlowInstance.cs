using Newtonsoft.Json;
using Simplic.Flow;
using System;
using System.Collections.Generic;

namespace Simplic.FlowInstance
{
    public class FlowInstance
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Flow.Flow Flow { get; set; }
        public IList<NodeScope<EventNode>> CurrentNodes { get; set; } = new List<NodeScope<EventNode>>();
        public DataPinScope DataScope { get; set; } = new DataPinScope();
        public bool IsAlive { get { return CurrentNodes.Count > 0; } }
        public bool IsFailed { get; set; }
    }
}
