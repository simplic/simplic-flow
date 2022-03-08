using System;

namespace Simplic.Flow.EventQueue
{
    public class EventQueueModel
    {
        /// <summary>
        /// Gets or sets the id of the event. In general this will be the event finger print (event name and object id)
        /// </summary>
        public string Id { get; set; } 

        public string EventName { get; set; }
        public byte[] Args { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int CreateUserId { get; set; }
        public bool Handled { get; set; }
    }
}