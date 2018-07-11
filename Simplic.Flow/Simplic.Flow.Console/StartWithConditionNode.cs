using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class StartWithConditionNode : ConditionNode
    {
        protected override bool Compare(IFlowRuntimeService runtime, ValueScope scope)
        {
            var val1 = scope.GetValue<string>(ConditionPinIn1);
            var val2 = scope.GetValue<string>(ConditionPinIn2);

            return val1.StartsWith(val2);
        }
    }
}
