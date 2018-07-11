using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow
{
    public abstract class ConditionNode : ActionNode
    {
        public override string FriendlyName { get; }
        
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            var result = Compare(runtime, scope);

            if (result)
                runtime.EnqueueNode(TrueFlowOut, scope);
            else
                runtime.EnqueueNode(FalseFlowOut, scope);

            runtime.EnqueueNode(AnyFlowOut, scope);

            return true;
        }

        protected abstract bool Compare(IFlowRuntimeService runtime, DataPinScope scope);

        public ActionNode TrueFlowOut { get; set; }
        public ActionNode FalseFlowOut { get; set; }
        public ActionNode AnyFlowOut { get; set; }
        public DataPin ConditionPinIn1 { get; set; }
        public DataPin ConditionPinIn2 { get; set; }
        public DataPin BooleanDataOut { get; set; }
    }
}
