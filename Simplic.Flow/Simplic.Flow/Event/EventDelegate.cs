using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Event
{
    public class EventDelegate
    {
        public string EventName { get; set; }
        public Guid FlowId { get; set; }
        public Guid EventNodeId { get; set; }
        public bool IsStartEvent;
    }
}
