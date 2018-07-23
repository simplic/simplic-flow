using Simplic.Flow.Event;

namespace Simplic.Flow
{
    public interface IFlowRuntimeService
    {
        bool EnqueueNode(ActionNode node, DataPinScope scope);

        FlowEventArgs FlowEventArgs { get; }

        FlowInstance Instance { get; }
    }
}