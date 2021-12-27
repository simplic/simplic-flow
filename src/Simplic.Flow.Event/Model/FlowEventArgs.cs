using System;

namespace Simplic.Flow.Event
{
    public class FlowEventArgs
    {
        /// <summary>
        /// Gets or sets the id of the event. For repeating events this should be "equal" to prevent
        /// from unnecessary events.
        /// </summary>
        public string Id { get; set; }
        public string EventName { get; set; }
        public object ObjectId { get; set; }
        public object Object { get; set; }
        public int UserId { get; set; }
    }
}