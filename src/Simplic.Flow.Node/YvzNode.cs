namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "YvzNode", Name = "YvzNode", Category = "Math")]
    public class YvzNode : ActionNode
    {
        public override string Name => "Yvz";

        public override string FriendlyName => "Yvz";

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [DataPinDefinition(
            Id = "fab3ec1c-b144-4de0-a0ae-40d60ba59a45",
            ContainerType = DataPinContainerType.Single,
            Direction = PinDirection.Out,
            DataType = typeof(int),
            Name = "OutPinResult",
            DisplayName = "Result")]
        public DataPin OutPinResult { get; set; }

        [DataPinDefinition(
            Id = "45e3131a-100d-4b8d-8bba-b8a2f0dd074c",
            ContainerType = DataPinContainerType.Single,
            Direction = PinDirection.Out,
            DataType = typeof(float),
            Name = "OutPinResult2",
            DisplayName = "Result2")]
        public DataPin OutPinResult2 { get; set; }
    }
}
