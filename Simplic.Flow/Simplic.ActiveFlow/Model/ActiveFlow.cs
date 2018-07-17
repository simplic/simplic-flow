using Simplic.Flow;
using System;
using System.Collections.Generic;

namespace Simplic.ActiveFlow
{
    public class ActiveFlow
    {
        public Guid FlowInstanceId { get; set; }
        public Guid FlowId { get; set; }
        public IList<NodeScope<EventNode>> CurrentEventNodes { get; set; }
    }
}
