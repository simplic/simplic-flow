using System;
using System.IO;
using System.Linq;

namespace Simplic.Flow.Node.IO
{
    [EventNodeDefinition(DisplayName = "On execute flow", Name = "OnExecuteFlowEvent", EventName = "OnExecuteFlowEvent", Category = "Common")]
    public class OnExecuteFlowEvent : EventNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var target = scope.GetValue<string>(InPinTarget);
            var args = runtime.FlowEventArgs as OnExecuteFlowEventArgs;

            if (args == null)
            {
                Console.WriteLine($"Arguments not found in {nameof(OnExecuteFlowEventArgs)}");
                return false;
            }

            scope.SetValue(OutPinData01, args.Data01);
            scope.SetValue(OutPinData02, args.Data02);
            scope.SetValue(OutPinData03, args.Data03);

            if (OutNode != null)
                runtime.EnqueueNode(OutNode, scope);

            return true;
        }

        public override bool ShouldExecute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var obj = (runtime.FlowEventArgs as OnExecuteFlowEventArgs);
            var target = scope.GetValue<string>(InPinTarget);

            if (obj == null)
                return false;

            if (string.IsNullOrWhiteSpace(obj.Target))
                return false;

            if (string.IsNullOrWhiteSpace(target))
                return false;

            return obj.Target.ToLower()?.Trim() == target.ToLower().Trim();
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "1d867597-292a-49ae-a63e-e6628f9814f4",
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
            Id = "d7e22a2f-3c4a-450e-bcf2-5ca4f8327258",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.Out,
            Name = nameof(OutPinData01),
            DisplayName = "Data 01")]
        public DataPin OutPinData01 { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "a623844a-0312-4794-9492-8ef59201fde2",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.Out,
            Name = nameof(OutPinData02),
            DisplayName = "Data 02")]
        public DataPin OutPinData02 { get; set; }

        /// <summary>
        /// Gets or sets the pin node
        /// </summary>
        [DataPinDefinition(
            Id = "24e7ca30-a0c4-465d-b554-b9d42c341b85",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(object),
            Direction = PinDirection.Out,
            Name = nameof(OutPinData03),
            DisplayName = "Data 03")]
        public DataPin OutPinData03 { get; set; }

        public override string EventName
        {
            get
            {
                return nameof(OnExecuteFlowEvent);
            }
        }

        public override string FriendlyName
        {
            get
            {
                return nameof(OnExecuteFlowEvent);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(OnExecuteFlowEvent);
            }
        }
    }
}
