using System;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "For Each", Name = "ForeachNode", Category = "Common")]
    public class ForeachNode : ActionNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            var values = scope.GetListValue<object>(InPinList);
            foreach (var value in values)
            {
                var newScope = scope.CreateChild();
                newScope.SetValue(OutPin, value);

                runtime.EnqueueNode(OutNodeEachItem, newScope);
            }

            runtime.EnqueueNode(OutNodeCompleted, scope);

            return true;
        }        

        [FlowPinDefinition(DisplayName = "Each Item", Name = "OutNodeEachItem", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeEachItem { get; set; }

        [FlowPinDefinition(DisplayName = "Completed", Name = "OutNodeCompleted", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeCompleted { get; set; }

        [DataPinDefinition(
            Id = "20c769b3-7796-457d-a024-50467f3902e0",
            ContainerType = DataPinContainerType.List,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = "InPinList",
            DisplayName = "Array")]
        public DataPin InPinList { get; set; }

        [DataPinDefinition(
            Id = "9c503704-835c-45af-9183-cd6f58b014ff",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.Out,
            Name = "OutPin",
            DisplayName = "Each item")]
        public DataPin OutPin { get; set; }
        public override string FriendlyName { get { return nameof(ForeachNode); } }
        public override string Name { get { return nameof(ForeachNode); } }
    }
}