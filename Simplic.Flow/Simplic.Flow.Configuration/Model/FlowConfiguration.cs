using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration
{
    public class FlowConfiguration
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public List<NodeConfiguration> Nodes { get; set; }
        public List<LinkConfiguration> Links { get; set; }
        public List<PinConfiguration> Pins { get; set; }
    }
}
