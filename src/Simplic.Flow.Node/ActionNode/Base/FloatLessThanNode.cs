namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Less Than (For Float)", Name = "FloatLessThanNode", Category = "Common")]
    public class FloatLessThanNode : ActionNode
    {
        public override string Name => nameof(FloatLessThanNode);

        public override string FriendlyName => nameof(FloatLessThanNode);

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var valueA = scope.GetValue<float>(InPinConditionA);
            var valueB = scope.GetValue<float>(InPinConditionB);

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
            Id = "06f3ca2f-2a20-49f9-9a1c-d63ec162bca0",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(float),
            Direction = PinDirection.In,
            Name = "InPinConditionA",
            DisplayName = "Float A")]
        public DataPin InPinConditionA { get; set; }

        [DataPinDefinition(
            Id = "0f5f9882-40bc-4392-912c-4eb7160ae044",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(float),
            Direction = PinDirection.In,
            Name = "InPinConditionB",
            DisplayName = "Float B")]
        public DataPin InPinConditionB { get; set; }

        [DataPinDefinition(
            Id = "2bf25678-f005-4dd3-8c73-5f2a8ecd2c0e",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(bool),
            Direction = PinDirection.Out,
            Name = "OutPinBoolean",
            DisplayName = "Result")]
        public DataPin OutPinBoolean { get; set; }
    }
}
