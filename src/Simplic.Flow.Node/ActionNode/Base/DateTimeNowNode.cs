using Simplic.Flow.Log;
using System;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Clear pin node
    /// </summary>
    [ActionNodeDefinition(DisplayName = "DateTimeNow", Name = "DateTimeNowNode", Category = "DateTime")]
    public class DateTimeNowNode : ActionNode
    {
        /// <summary>
        /// Clear pin value
        /// </summary>
        /// <param name="runtime">Runtime instance</param>
        /// <param name="scope">Scope instance</param>
        /// <returns>True</returns>
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                scope.SetValue(OutPinNow, DateTime.Now);

                if (SuccessOutNode != null)
                    runtime.EnqueueNode(SuccessOutNode, scope);
            }
            catch (Exception ex)
            {
                if (FailedOutNode != null)
                    runtime.EnqueueNode(FailedOutNode, scope);

                throw ex;
            }

            return true;
        }

        /// <summary>
        /// Gets or sets the flow out node
        /// </summary>
        [FlowPinDefinition(DisplayName = "Success", Name = "SuccessOutNode", PinDirection = PinDirection.Out)]
        public ActionNode SuccessOutNode { get; set; }

        /// <summary>
        /// Gets or sets the flow out node
        /// </summary>
        [FlowPinDefinition(DisplayName = "Failed", Name = "FailedOutNode", PinDirection = PinDirection.Out)]
        public ActionNode FailedOutNode { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "0876cbf9-4901-4913-9dca-5e2f91e102d2",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(DateTime),
            Direction = PinDirection.Out,
            Name = nameof(OutPinNow),
            DisplayName = "Now")]
        public DataPin OutPinNow { get; set; }

        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public override string FriendlyName { get { return nameof(DateTimeNowNode); } }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public override string Name { get { return nameof(DateTimeNowNode); } }
    }
}