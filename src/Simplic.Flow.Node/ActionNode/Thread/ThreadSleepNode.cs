using System;
using System.Threading;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Thread sleep", Name = "ThreadSleepNode")]
    public class ThreadSleepNode : ActionNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var value = scope.GetValue<int>(InPinWaitTime);
            Thread.Sleep(value);

            if (OutNode != null)
                runtime.EnqueueNode(OutNode, scope);

            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [DataPinDefinition(
            Id = "a812a3b3-ef58-422b-a34a-70665d189f38",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(int),
            Direction = PinDirection.In,
            Name = "InPinWaitTime",
            DisplayName = "Wait time (ms)")]
        public DataPin InPinWaitTime { get; set; }

        public override string FriendlyName { get { return nameof(ThreadSleepNode); } }
        public override string Name { get { return nameof(ThreadSleepNode); } }
    }
}
