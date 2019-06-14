using System.Linq;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Get Variable", Name = "GetVariableNode", Category = "Common")]
    public class GetVariableNode : ActionNode
    {
        public override string Name => nameof(GetVariableNode);

        public override string FriendlyName => nameof(GetVariableNode);

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var variableName = scope.GetValue<string>(InPinVariableName);
            var variable = runtime.Instance.Variables.FirstOrDefault(x => x.Name == variableName);
            if (variable != null)
            {
                scope.SetValue(OutPinVariable, variable.Value);
                runtime.EnqueueNode(OutNode, scope);
            }
            else if (OutNode != null)
                runtime.EnqueueNode(OutNode, scope);

            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [DataPinDefinition(
            Id = "bd37f3da-ef88-47a9-9bfc-a5bdf3f9bf21",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinVariableName",
            DisplayName = "Variable Name")]
        public DataPin InPinVariableName { get; set; }

        [DataPinDefinition(
            Id = "4dcc6726-2b3f-4a58-bb07-436840c444e8",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.Out,
            Name = "OutPinVariable",
            DisplayName = "Variable")]
        public DataPin OutPinVariable { get; set; }
    }
}
