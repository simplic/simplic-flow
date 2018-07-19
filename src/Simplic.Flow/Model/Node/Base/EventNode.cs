using System;

namespace Simplic.Flow
{
    public abstract class EventNode : ActionNode
    {
        public virtual string EventName { get { return GetType().Name; } }
        public Guid FlowId { get; set; }                
        public bool IsStartEvent { get; set; }
        public virtual bool ShouldExecute(DataPinScope scope)
        {
            return true;
        }
    }
}
