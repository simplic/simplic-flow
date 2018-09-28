using System;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Greater Than (For Integer)", Name = "IntGreaterThanNode", Category = "Common")]
    public class IntGreaterThanNode : ActionNode
    {
        public override string Name => nameof(IntGreaterThanNode);

        public override string FriendlyName => nameof(IntGreaterThanNode);

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var valueA = scope.GetValue<int>(InPinConditionA);
            var valueB = scope.GetValue<int>(InPinConditionB);

            if (valueA > valueB)
                runtime.EnqueueNode(OutNodeTrue, scope);
            else
                runtime.EnqueueNode(OutNodeFalse, scope);

            runtime.EnqueueNode(OutNodeAny, scope);

            return true;
        }

        [FlowPinDefinition(DisplayName = "True", Name = "OutNodeTrue", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeTrue { get; set; }

        [FlowPinDefinition(DisplayName = "False", Name = "OutNodeFalse", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeFalse { get; set; }

        [FlowPinDefinition(DisplayName = "Any", Name = "OutNodeAny", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeAny { get; set; }

        [DataPinDefinition(
            Id = "be903df8-df10-4779-9e11-28f0896b9eb7",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(int),
            Direction = PinDirection.In,
            Name = "InPinConditionA",
            DisplayName = "Integer A")]
        public DataPin InPinConditionA { get; set; }

        [DataPinDefinition(
            Id = "a86a129a-7cc2-4c85-b830-e76155121652",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(int),
            Direction = PinDirection.In,
            Name = "InPinConditionB",
            DisplayName = "Integer B")]
        public DataPin InPinConditionB { get; set; }

        [DataPinDefinition(
            Id = "8f354c3a-8756-4e65-a248-e6899af9bd09",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(bool),
            Direction = PinDirection.Out,
            Name = "OutPinBoolean",
            DisplayName = "Result")]
        public DataPin OutPinBoolean { get; set; }
    }
}
