using System.Collections.Generic;

namespace Simplic.Flow.Node
{
    public class SequenceNode : ActionNode
    {
        public override string FriendlyName { get; }
        
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            foreach (var node in FlowOutNodes)
            {
                runtime.EnqueueNode(node, scope);
            }
            
            return true;
        }

        public IList<ActionNode> FlowOutNodes { get; set; } = new List<ActionNode>();
    }
}
