using Simplic.Flow.Event;
using System;

namespace Simplic.Flow
{
    public class EventCall
    {
        public Guid QueueId { get; set; }
        public EventDelegate Delegate { get; set; }
        public FlowEventArgs Args { get; set; }
    }
}
