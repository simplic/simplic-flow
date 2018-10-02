using System;
using System.Collections.Generic;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Sequence", Name = "SequenceNode", Category = "Common")]
    public class SequenceNode : ActionNode
    {                
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {         
            foreach (var node in OutNodes)
            {
                runtime.EnqueueNode(node, scope);
            }
            
            return true;
        }

        [FlowPinDefinition(AllowMultiple = true, 
            DisplayName = "Out", 
            Name = "OutNodes", 
            PinDirection = PinDirection.Out)]
        public IList<ActionNode> OutNodes { get; set; } = new List<ActionNode>();
        public override string FriendlyName { get { return nameof(SequenceNode); } }
        public override string Name { get { return nameof(SequenceNode); } }
    }
}
