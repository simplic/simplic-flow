using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class SequenceNode : ActionNode
    {
        public override string FriendlyName { get; }
        
        public override bool Execute(IFlowRuntimeService runtime, ValueScope scope)
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
