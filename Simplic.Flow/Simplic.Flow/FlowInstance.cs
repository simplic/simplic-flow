using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow
{
    public class FlowInstance
    {
        public Guid Id { get; set; }
        public Flow Flow { get; set; }
        public IList<EventNode> CurrentNodes { get; set; } = new List<EventNode>();
        public IDictionary<Guid, object> PinValues = new Dictionary<Guid, object>();
    }
}
