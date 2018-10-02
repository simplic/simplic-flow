namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Greater Than (For Float)", Name = "FloatGreaterThanNode", Category = "Common")]
    public class FloatGreaterThanNode : ActionNode
    {
        public override string Name => nameof(FloatGreaterThanNode);

        public override string FriendlyName => nameof(FloatGreaterThanNode);

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var valueA = scope.GetValue<float>(InPinConditionA);
            var valueB = scope.GetValue<float>(InPinConditionB);

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
            Id = "50d2a2fc-ed5c-4bda-a227-b51835708ec9",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(float),
            Direction = PinDirection.In,
            Name = "InPinConditionA",
            DisplayName = "Float A")]
        public DataPin InPinConditionA { get; set; }

        [DataPinDefinition(
            Id = "ee5a871e-bb1e-4802-b233-2c4133e0284a",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(float),
            Direction = PinDirection.In,
            Name = "InPinConditionB",
            DisplayName = "Float B")]
        public DataPin InPinConditionB { get; set; }

        [DataPinDefinition(
            Id = "8149972c-cc8f-4a36-876f-373904c67984",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(bool),
            Direction = PinDirection.Out,
            Name = "OutPinBoolean",
            DisplayName = "Result")]
        public DataPin OutPinBoolean { get; set; }
    }
}
