using System;

namespace Simplic.Flow
{
    public abstract class EventNode : ActionNode
    {
        public abstract string EventName { get; set; }
        public Guid FlowId { get; set; }                
        public abstract bool IsStartEvent { get; set; }
        public virtual bool ShouldExecute(DataPinScope scope)
        {
            return true;
        }
    }
}
