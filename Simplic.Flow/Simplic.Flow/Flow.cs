using System;
using System.Collections.Generic;

namespace Simplic.Flow
{
    public class Flow
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<ActionNode> Nodes { get; set; }
    }
}
