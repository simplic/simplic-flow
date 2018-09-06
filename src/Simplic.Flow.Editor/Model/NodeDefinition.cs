using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Editor
{
    public abstract class NodeDefinition
    {
        public string Name { get; set; }
        public IList<FlowPinDefinition> InFlowPins { get; set; }
        public IList<FlowPinDefinition> OutFlowPins { get; set; }
        public IList<DataPinDefinition> InDataPins { get; set; }
        public IList<DataPinDefinition> OutDataPins { get; set; }
    }
}
