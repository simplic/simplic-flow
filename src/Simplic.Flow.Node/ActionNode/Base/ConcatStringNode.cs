using Simplic.Flow.Log;
using System;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Clear pin node
    /// </summary>
    [ActionNodeDefinition(DisplayName = "Concat", Name = "ConcatStringNode", Category = "String")]
    public class ConcatStringNode : ActionNode
    {
        /// <summary>
        /// Clear pin value
        /// </summary>
        /// <param name="runtime">Runtime instance</param>
        /// <param name="scope">Scope instance</param>
        /// <returns>True</returns>
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var str1 = scope.GetValue<string>(InPinString1);
            var str2 = scope.GetValue<string>(InPinString2);

            try
            {
                scope.SetValue(OutPinString, $"{str1 ?? ""}{str2 ?? ""}");

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
            Id = "9c8f4db5-1643-42d3-9ecc-4c843e63b30d",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = nameof(OutPinString),
            DisplayName = "String")]
        public DataPin OutPinString { get; set; }

        /// <summary>
        /// Gets or sets the format node
        /// </summary>
        [DataPinDefinition(
            Id = "33f55f43-eae9-4390-a816-4b082f253929",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = nameof(InPinString1),
            DisplayName = "String1")]
        public DataPin InPinString1 { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "3bd3ad0a-bce3-47d4-bdac-0f67a1168633",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = nameof(InPinString2),
            DisplayName = "String2")]
        public DataPin InPinString2 { get; set; }

        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public override string FriendlyName { get { return nameof(ConcatStringNode); } }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public override string Name { get { return nameof(ConcatStringNode); } }
    }
}