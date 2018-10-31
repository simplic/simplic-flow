using System;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Clear pin node
    /// </summary>
    [ActionNodeDefinition(DisplayName = "ClearPin", Name = "ClearPinNode", Category = "Common")]
    public class ClearPinNode : ActionNode
    {
        /// <summary>
        /// Clear pin value
        /// </summary>
        /// <param name="runtime">Runtime instance</param>
        /// <param name="scope">Scope instance</param>
        /// <returns>True</returns>
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            scope.SetValue(InPinPin, null);

            if (OutNode != null)
                runtime.EnqueueNode(OutNode, scope);

            return true;
        }        

        /// <summary>
        /// Gets or sets the flow out node
        /// </summary>
        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "f30691da-9792-49b9-b9a1-eb2bb2a446c6",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinPin),
            DisplayName = "Pin")]
        public DataPin InPinPin { get; set; }

        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public override string FriendlyName { get { return nameof(ClearPinNode); } }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public override string Name { get { return nameof(ClearPinNode); } }
    }
}