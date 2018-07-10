using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow
{
    public abstract class ConditionNode : ActionNode
    {
        public override string FriendlyName => throw new NotImplementedException();
        
        public override bool Execute()
        {
            var result = Compare();
            var scope = new PinScope { Value = result, Pin = BooleanDataOut };

            if (result)
                EnqueueNode(TrueFlowOut, scope);
            else
                EnqueueNode(FalseFlowOut, scope);

            EnqueueNode(AnyFlowOut, scope);

            return true;
        }

        protected abstract bool Compare();

        public ActionNode TrueFlowOut { get; set; }
        public ActionNode FalseFlowOut { get; set; }
        public ActionNode AnyFlowOut { get; set; }
        public DataPin ConditionPinIn1 { get; set; }
        public DataPin ConditionPinIn2 { get; set; }
        public DataPin BooleanDataOut { get; set; }
    }
}
