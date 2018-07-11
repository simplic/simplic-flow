using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Event
{
    public class EventCall
    {
        public EventDelegate Delegate { get; set; }
        public FlowEventArgs Args { get; set; }
    }
}
