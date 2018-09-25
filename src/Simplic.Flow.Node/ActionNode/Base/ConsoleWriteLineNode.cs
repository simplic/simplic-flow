using System;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Console out", Name = "ConsoleWriteLineNode")]
    public class ConsoleWriteLineNode : ActionNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                var value = scope.GetValue<object>(InPinToPrint);
                System.Console.WriteLine(value);
            }
            catch (Exception ex)
            {

                throw;
            }
            

            if (OutNode != null)
                runtime.EnqueueNode(OutNode, scope);

            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [DataPinDefinition(
            Id = "b6ffc7b8-8f06-409c-8d27-7757518c2ab6", 
            ContainerType = DataPinContainerType.Single, 
            DataType = typeof(object), 
            Direction = PinDirection.In, 
            Name = "InPinToPrint",
            DisplayName = "To Print")]
        public DataPin InPinToPrint { get; set; }

        public override string FriendlyName { get { return nameof(ConsoleWriteLineNode); } }
        public override string Name { get { return nameof(ConsoleWriteLineNode); } }        
    }
}
