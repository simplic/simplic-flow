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


        public override bool Execute()
        {
            var values = GetListValue<object>(InList);
            foreach (var value in values)
            {
                var sope = new PinScope
                {
                    Pin = Output,
                    Value = value
                };

                EnqueueNode(EachItemFlowOut, sope);
            }

            EnqueueNode(CompletedFlowOut);

            return true;
        }

        public ActionNode EachItemFlowOut { get; set; }
        public ActionNode CompletedFlowOut { get; set; }
        public DataPin InList { get; set; }
        public DataPin Output { get; set; }
    }
}
