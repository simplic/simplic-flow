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


        public override bool Execute(IFlowRuntimeService runtime, ValueScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            var values = scope.GetListValue<object>(InList);
            foreach (var value in values)
            {
                var newScope = scope.CreateChild();
                newScope.SetValue(Output, value);

                runtime.EnqueueNode(EachItemFlowOut, newScope);
            }

            runtime.EnqueueNode(CompletedFlowOut, scope);

            return true;
        }

        public ActionNode EachItemFlowOut { get; set; }
        public ActionNode CompletedFlowOut { get; set; }
        public DataPin InList { get; set; }
        public DataPin Output { get; set; }
    }
}
