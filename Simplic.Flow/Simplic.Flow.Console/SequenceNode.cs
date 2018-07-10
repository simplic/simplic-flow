using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class SequenceNode : ActionNode
    {
        public override string FriendlyName => throw new NotImplementedException();
        
        public override bool Execute()
        {
            foreach (var node in FlowOutNodes)
            {
                EnqueueNode(node);
            }
            
            return true;
        }

        public IList<ActionNode> FlowOutNodes { get; set; }
    }
}
