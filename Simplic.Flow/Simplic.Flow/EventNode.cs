using Simplic.Flow.Event;
using System;

namespace Simplic.Flow
{    
    public abstract class EventNode : ActionNode
    {
        public FlowEventArgs Arguments { get; set; }
        public string EventName { get; set; }
        public Guid FlowId { get; set; }        
        public abstract bool NeedsState { get; set; }
    }
}
