using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration
{
    /// <summary>
    /// Represents the flow configuration that the runtime needs. Not to be mistaken with the <see cref="FlowConfigurationModel"/>
    /// </summary>
    public class FlowConfiguration
    {
        /// <summary>
        /// Gets or sets the Id of the flow 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the flow
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of nodes inside this configuration
        /// </summary>
        public List<NodeConfiguration> Nodes { get; set; } = new List<NodeConfiguration>();

        /// <summary>
        /// Gets or sets the links between nodes
        /// </summary>
        public List<LinkConfiguration> Links { get; set; } = new List<LinkConfiguration>();

        /// <summary>
        /// Gets or sets the pins that the nodes contain
        /// </summary>
        public List<PinConfiguration> Pins { get; set; } = new List<PinConfiguration>();

        /// <summary>
        /// Gets or sets the configuration wide variables
        /// </summary>
        public List<FlowVariableConfiguration> Variables { get; set; } = new List<FlowVariableConfiguration>();

        /// <summary>
        /// Gets or sets whether the flow configuration is active
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}