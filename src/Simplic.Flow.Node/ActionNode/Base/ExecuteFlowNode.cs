using CommonServiceLocator;
using Simplic.Flow.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Node
{
    /// <summary>
    /// Clear pin node
    /// </summary>
    [ActionNodeDefinition(DisplayName = "Execute flow", Name = nameof(ExecuteFlowNode), Category = "Common")]
    public class ExecuteFlowNode : ActionNode
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
                var target = scope.GetValue<string>(InPinTarget);

                if (string.IsNullOrWhiteSpace(target))
                {
                    Console.WriteLine($"No target is set in {nameof(ExecuteFlowNode)}");
                    return false;
                }

                var eventService = ServiceLocator.Current.GetInstance<IFlowEventService>();

                var data01 = scope.GetValue<string>(InPinData01);
                var data02 = scope.GetValue<string>(InPinData02);
                var data03 = scope.GetValue<string>(InPinData03);

                var id = $"{target}EFE{data01 ?? ""}{data02 ?? ""}{data02 ?? ""}";

                if (id.Length > 255)
                    id = id.Substring(0, 255);

                eventService.InvokeEvent(new OnExecuteFlowEventArgs
                {
                    Id = id,
                    Target = target,
                    EventName = "OnExecuteFlowEvent",
                    Data01 = data01,
                    Data02 = data02,
                    Data03 = data03,
                    ObjectId = target,
                    UserId = 0
                });

                if (SuccessOutNode != null)
                    runtime.EnqueueNode(SuccessOutNode, scope);
            }
            catch
            {
                if (FailedOutNode != null)
                    runtime.EnqueueNode(FailedOutNode, scope);
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
            Id = "8a9221ab-eee9-49c8-baf9-c4c4c73e6626",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = nameof(InPinTarget),
            DisplayName = "Target")]
        public DataPin InPinTarget { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "e3050f11-687a-4f7d-82cb-632825e7c39d",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinData01),
            DisplayName = "Data 01")]
        public DataPin InPinData01 { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "e633f0bf-8b2a-46a9-b1d2-057473e17e8c",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinData02),
            DisplayName = "Data 02")]
        public DataPin InPinData02 { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "8a97ab6d-4219-4e5d-adf6-62e2e06a6db6",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.In,
            Name = nameof(InPinData03),
            DisplayName = "Data 03")]
        public DataPin InPinData03 { get; set; }

        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public override string FriendlyName { get { return nameof(ExecuteFlowNode); } }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public override string Name { get { return nameof(ExecuteFlowNode); } }
    }
}