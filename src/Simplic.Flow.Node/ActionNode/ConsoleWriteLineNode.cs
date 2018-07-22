using System;

namespace Simplic.Flow.Node
{
    public class ConsoleWriteLineNode : ActionNode
    {
        public ConsoleWriteLineNode()
        {
            InPinToPrint = new DataPin
            {
                ContainerType = DataPinContainerType.Single,
                DataType = typeof(object),
                Direction = PinDirection.In,
                Id = Guid.NewGuid(),
                Name = "ConsoleWriteLineInPin"
            };
        }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine(scope.GetValue<object>(InPinToPrint));

            System.Console.WriteLine($"> {GetType().Name} {DateTime.Now} :: {DateTime.Now.Millisecond}");

            if (OutNode != null)
                runtime.EnqueueNode(OutNode, scope);

            return true;
        }

        public ActionNode OutNode { get; set; }
        public DataPin InPinToPrint { get; set; }

        public override string FriendlyName { get { return nameof(ConsoleWriteLineNode); } }
        public override string Name { get { return nameof(ConsoleWriteLineNode); } }        
    }
}
