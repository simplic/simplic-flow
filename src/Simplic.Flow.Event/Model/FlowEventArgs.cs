using System;

namespace Simplic.Flow.Event
{
    public class FlowEventArgs
    {
        public Guid QueueId { get; set; }
        public string EventName { get; set; }
        public object ObjectId { get; set; }
        public object Object { get; set; }
        public int UserId { get; set; }
    }
}