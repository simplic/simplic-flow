using System;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Write Line to Console", Name = "ConsoleWriteLineNode")]
    public class ConsoleWriteLineNode : ActionNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine(scope.GetValue<object>(InPinToPrint));

            System.Console.WriteLine($"> {GetType().Name} {DateTime.Now} :: {DateTime.Now.Millisecond}");

            if (OutNode != null)
                runtime.EnqueueNode(OutNode, scope);

            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [DataPinDefinition(ContainerType = DataPinContainerType.Single, DataType = typeof(object), Direction = PinDirection.In, Name = "ConsoleWriteLineInPin", Id="...")]
        public DataPin InPinToPrint { get; set; }

        public override string FriendlyName { get { return nameof(ConsoleWriteLineNode); } }
        public override string Name { get { return nameof(ConsoleWriteLineNode); } }        
    }
}
