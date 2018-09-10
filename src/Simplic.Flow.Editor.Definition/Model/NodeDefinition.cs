using System.Collections.Generic;

namespace Simplic.Flow.Editor.Definition
{
    public abstract class NodeDefinition
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IList<FlowPinDefinition> InFlowPins { get; set; } = new List<FlowPinDefinition>();
        public IList<FlowPinDefinition> OutFlowPins { get; set; } = new List<FlowPinDefinition>();
        public IList<DataPinDefinition> InDataPins { get; set; } = new List<DataPinDefinition>();
        public IList<DataPinDefinition> OutDataPins { get; set; } = new List<DataPinDefinition>();
    }
}
