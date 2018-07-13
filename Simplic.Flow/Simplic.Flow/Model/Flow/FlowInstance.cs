using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Simplic.Flow
{
    public class FlowInstance
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public Flow Flow { get; set; }

        public IList<NodeScope<EventNode>> CurrentNodes { get; set; } = new List<NodeScope<EventNode>>();

        public DataPinScope Scope = new DataPinScope();
        public bool IsAlive { get { return CurrentNodes.Count > 0; } }
    }
}
