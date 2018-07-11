using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class ConsoleWriteLineNode : ActionNode
    {
        public override string FriendlyName { get; }

        public ConsoleWriteLineNode()
        {
            ToPrint = new DataPin
            {
                ContainerType = DataPinContainerType.Single,
                DataType = typeof(object),
                Direction = PinDirection.In,
                Id = Guid.NewGuid(),
                Name = "ConsoleWriteLineInPin"
            };
        }

        public override bool Execute(IFlowRuntimeService runtime, ValueScope scope)
        {
            System.Console.WriteLine(scope.GetValue<object>(ToPrint));

            System.Console.WriteLine($"> {GetType().Name} {DateTime.Now} :: {DateTime.Now.Millisecond}");

            if (FlowOut != null)
                runtime.EnqueueNode(FlowOut, scope);

            return true;
        }
        
        public ActionNode FlowOut { get; set; }
        public DataPin ToPrint { get; set; }
    }
}
