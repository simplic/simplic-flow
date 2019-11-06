using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.EventQueue.Data.DB
{
    public class TargetEventQueueModel : EventQueueModel
    {
        public string MachineName { get; set; }
        public string ServiceName { get; set; }
    }
}
