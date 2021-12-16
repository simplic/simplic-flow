using Simplic.Flow.Editor.Definition;
using System.Collections.Generic;

namespace Simplic.Flow.NodeGenerator
{
    public class JsonNodeDefinition
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ToolTip { get; set; }
        public string Category { get; set; }
        public string MethodName { get; set; }
        public string Namespace { get; set; }
        public IList<FlowPinDefinition> InFlowPins { get; set; }
        public IList<FlowPinDefinition> OutFlowPins { get; set; }
        public IList<DataPinDefinition> InDataPins { get; set; }
        public IList<DataPinDefinition> OutDataPins { get; set; }
    }
}
