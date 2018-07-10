using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Event
{
    public class WorkflowEventArgs
    {
        public string EventName { get; set; }
        public object ObjectId { get; set; }
        public object Object { get; set; }
    }
}
