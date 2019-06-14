using System.Linq;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Set Variable", Name = "SetVariableNode", Category = "Common")]
    public class SetVariableNode : ActionNode
    {
        public override string Name => nameof(SetVariableNode);

        public override string FriendlyName => nameof(SetVariableNode);

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var variableName = scope.GetValue<string>(InPinVariableName);
            var variable = runtime.Instance.Variables.FirstOrDefault(x => x.Name == variableName);
            if (variable != null)
            {
                // set variables value to in pin value
                variable.Value = scope.GetValue<object>(InPinVariableValue);                
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
            Id = "eb4e66a0-2f31-4a10-97bd-ab42b4e839ff",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinVariableName",
            DisplayName = "Variable Name")]
        public DataPin InPinVariableName { get; set; }

        [DataPinDefinition(
            Id = "36d48e4a-90c8-4f03-8bb4-3589522b48cf",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = "InPinVariableValue",
            DisplayName = "Variable Value")]
        public DataPin InPinVariableValue { get; set; }

        [DataPinDefinition(
            Id = "46c79695-d9f2-43e3-a44b-3a67b40f473a",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.Out,
            Name = "OutPinVariable",
            DisplayName = "Variable")]
        public DataPin OutPinVariable { get; set; }
    }
}
