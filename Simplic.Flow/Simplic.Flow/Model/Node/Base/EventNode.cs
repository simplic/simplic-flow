using Simplic.Flow.Event;
using System;

namespace Simplic.Flow
{    
    public abstract class EventNode : ActionNode
    {
        public FlowEventArgs Arguments { get; set; }
        public string EventName { get; set; }
        public Guid FlowId { get; set; }                
        public bool IsStartEvent { get; set; }
        public virtual bool ShouldExecute(DataPinScope scope)
        {
            return true;
        }
    }
}
