using System;

namespace Simplic.Flow.Event
{
    public class FlowEventArgs
    {
        /// <summary>
        /// Gets or sets the id of the event. In general this will be the event finger print (event name and object id)
        /// </summary>
        public string Id { get; set; }
        public string EventName { get; set; }
        public object ObjectId { get; set; }
        public object Object { get; set; }
        public int UserId { get; set; }
    }
}