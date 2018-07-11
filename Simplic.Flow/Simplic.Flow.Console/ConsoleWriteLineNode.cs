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

        public override bool Execute()
        {
            System.Console.WriteLine(GetValue<object>(ToPrint));
            EnqueueNode(FlowOut);

            return true;
        }

        public ActionNode FlowOut { get; set; }
        public DataPin ToPrint { get; set; }
    }
}
