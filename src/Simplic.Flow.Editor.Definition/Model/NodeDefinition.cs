using System.Collections.Generic;

namespace Simplic.Flow.Editor.Definition
{
    public abstract class NodeDefinition
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Tooltip { get; set; }
        public string Category { get; set; }
        /// <summary>
        /// Gets or sets the node documentation URL.
        /// </summary>
        public string DocumentationUrl { get; set; }
        /// <summary>
        /// Gets or sets the full type name of the node.
        /// <para>The full type name is used to try to dynamically create the node documentation URL if not explicitly given as an attribute.</para>
        /// </summary>
        public string FullTypeName { get; set; }
        public IList<FlowPinDefinition> InFlowPins { get; set; } = new List<FlowPinDefinition>();
        public IList<FlowPinDefinition> OutFlowPins { get; set; } = new List<FlowPinDefinition>();
        public IList<DataPinDefinition> InDataPins { get; set; } = new List<DataPinDefinition>();
        public IList<DataPinDefinition> OutDataPins { get; set; } = new List<DataPinDefinition>();
    }
}
