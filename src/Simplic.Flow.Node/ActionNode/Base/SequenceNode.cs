using System;
using System.Collections.Generic;

namespace Simplic.Flow.Node
{
    public class SequenceNode : ActionNode
    {                
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            foreach (var node in OutNodes)
            {
                runtime.EnqueueNode(node, scope);
            }
            
            return true;
        }

        public IList<ActionNode> OutNodes { get; set; } = new List<ActionNode>();
        public override string FriendlyName { get { return nameof(SequenceNode); } }
        public override string Name { get { return nameof(SequenceNode); } }
    }
}
