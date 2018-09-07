﻿using System.Collections.Generic;

namespace Simplic.Flow.Editor
{
    public abstract class NodeDefinition
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IList<FlowPinDefinition> InFlowPins { get; set; }
        public IList<FlowPinDefinition> OutFlowPins { get; set; }
        public IList<DataPinDefinition> InDataPins { get; set; }
        public IList<DataPinDefinition> OutDataPins { get; set; }
    }
}
