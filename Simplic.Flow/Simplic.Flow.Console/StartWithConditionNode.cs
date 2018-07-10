using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class StartWithConditionNode : ConditionNode
    {
        protected override bool Compare()
        {
            var val1 = GetValue<string>(ConditionPinIn1);
            var val2 = GetValue<string>(ConditionPinIn2);

            return val1.StartsWith(val2);
        }
    }
}
