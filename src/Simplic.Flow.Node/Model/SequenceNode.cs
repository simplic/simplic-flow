using Simplic.Flow.Event;
using System.Collections.Generic;
using System;

namespace Simplic.Flow.Node
{
    public class SequenceNode : ActionNode
    {
        public override string FriendlyName { get; }
        
        public override bool Execute(IFlowRuntimeService runtime, FlowEventArgs args, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            foreach (var node in OutNodes)
            {
                runtime.EnqueueNode(node, scope);
            }
            
            return true;
        }

        public IList<ActionNode> OutNodes { get; set; } = new List<ActionNode>();

        public override string Name
        {
            get
            {
                return nameof(SequenceNode);
            }
        }
    }
}
