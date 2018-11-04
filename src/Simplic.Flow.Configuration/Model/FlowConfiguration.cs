using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration
{
    /// <summary>
    /// Represents a flow configuration
    /// </summary>
    public class FlowConfiguration
    {        
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public List<NodeConfiguration> Nodes { get; set; } = new List<NodeConfiguration>();
        public List<LinkConfiguration> Links { get; set; } = new List<LinkConfiguration>();
        public List<PinConfiguration> Pins { get; set; } = new List<PinConfiguration>();
        public List<FlowVariableConfiguration> Variables { get; set; } = new List<FlowVariableConfiguration>();

        /// <summary>
        /// Gets or sets whether the flow configuration is active
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}