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
        public IList<NodeScope<EventNode>> CurrentNodes { get; set; } = new List<NodeScope<EventNode>>();
        public DataPinScope Scope = new DataPinScope();
    }
}
