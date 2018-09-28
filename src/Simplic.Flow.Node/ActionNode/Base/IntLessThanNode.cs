namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Less Than (For Integer)", Name = "IntLessThanNode", Category = "Common")]
    public class IntLessThanNode : ActionNode
    {
        public override string Name => nameof(IntLessThanNode);

        public override string FriendlyName => nameof(IntLessThanNode);

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var valueA = scope.GetValue<int>(InPinConditionA);
            var valueB = scope.GetValue<int>(InPinConditionB);

            if (valueA < valueB)
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
            Id = "d422ab8e-b82a-42b7-95a9-f19ba9901d2e",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(int),
            Direction = PinDirection.In,
            Name = "InPinConditionA",
            DisplayName = "Integer A")]
        public DataPin InPinConditionA { get; set; }

        [DataPinDefinition(
            Id = "fc41f002-311f-4afc-93e1-98da80aa9bc6",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(int),
            Direction = PinDirection.In,
            Name = "InPinConditionB",
            DisplayName = "Integer B")]
        public DataPin InPinConditionB { get; set; }

        [DataPinDefinition(
            Id = "4fbd74d9-4b57-4a45-92e8-c58461ca0792",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(bool),
            Direction = PinDirection.Out,
            Name = "OutPinBoolean",
            DisplayName = "Result")]
        public DataPin OutPinBoolean { get; set; }
    }
}
