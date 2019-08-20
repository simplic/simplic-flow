using Simplic.Flow.Log;
using System;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Clear pin node
    /// </summary>
    [ActionNodeDefinition(DisplayName = "ToString", Name = "ToStringNode", Category = "String")]
    public class ToStringNode : ActionNode
    {
        /// <summary>
        /// Clear pin value
        /// </summary>
        /// <param name="runtime">Runtime instance</param>
        /// <param name="scope">Scope instance</param>
        /// <returns>True</returns>
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var obj = scope.GetValue<object>(InPinObject);
            var format = scope.GetValue<string>(InPinFormat);

            try
            {
                if (obj != null)
                {
                    if (string.IsNullOrWhiteSpace(format))
                        scope.SetValue(OutPinString, obj.ToString());
                    else
                    {
                        var method = obj.GetType().GetMethod("ToString", new[] { typeof(string) });
                        if (method == null)
                            throw new Exception("ToString with format parameter is not allowed in ToStringNode");

                        scope.SetValue(OutPinString, method.Invoke(obj, new[] { format }));
                    }
                }

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
            Id = "169a786e-5fca-49a9-b7b2-c64b5b028302",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinObject),
            DisplayName = "Object")]
        public DataPin InPinObject { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "c7021867-e630-46f9-904d-2fc835854f2c",
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
            Id = "fe23a672-8e56-4e29-9298-d057137ae1a7",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = nameof(InPinFormat),
            DisplayName = "Format")]
        public DataPin InPinFormat { get; set; }

        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public override string FriendlyName { get { return nameof(ToStringNode); } }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public override string Name { get { return nameof(ToStringNode); } }
    }
}