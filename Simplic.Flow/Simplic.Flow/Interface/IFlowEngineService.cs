using Simplic.Flow.Event;

namespace Simplic.Flow
{
    public interface IFlowService
    {
        void Run();
        void EnqueueEvent(FlowEventArgs args);
    }
}