using System;

namespace Simplic.Flow.EventQueue
{
    public class EventQueueModel
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public byte[] Args { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int CreateUserId { get; set; }
        public bool Handled { get; set; }
    }
}