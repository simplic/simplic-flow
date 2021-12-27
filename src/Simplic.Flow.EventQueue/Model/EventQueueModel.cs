using System;

namespace Simplic.Flow.EventQueue
{
    public class EventQueueModel
    {
        /// <summary>
        /// Gets or sets the id of the event. For repeating events this should be "equal" to prevent
        /// from unnecessary events.
        /// </summary>
        public string Id { get; set; }

        public string EventName { get; set; }
        public byte[] Args { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int CreateUserId { get; set; }
        public bool Handled { get; set; }
    }
}