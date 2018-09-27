using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration
{
    public class FlowConfiguration
    {        
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public List<NodeConfiguration> Nodes { get; set; } = new List<NodeConfiguration>();
        public List<LinkConfiguration> Links { get; set; } = new List<LinkConfiguration>();
        public List<PinConfiguration> Pins { get; set; } = new List<PinConfiguration>();
        public List<FlowVariableConfiguration> Variables { get; set; } = new List<FlowVariableConfiguration>();
    }
}