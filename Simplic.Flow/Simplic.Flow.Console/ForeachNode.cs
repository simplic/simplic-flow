using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class ForeachNode : ActionNode
    {
        public override string FriendlyName { get; }


        public override bool Execute(IFlowRuntimeService runtime)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            var values = runtime.GetListValue<object>(InList);
            foreach (var value in values)
            {
                var scope = new PinScope
                {
                    Pin = Output,
                    Value = value
                };

                runtime.EnqueueNode(EachItemFlowOut, scope);
            }

            runtime.EnqueueNode(CompletedFlowOut);

            return true;
        }

        public ActionNode EachItemFlowOut { get; set; }
        public ActionNode CompletedFlowOut { get; set; }
        public DataPin InList { get; set; }
        public DataPin Output { get; set; }
    }
}
