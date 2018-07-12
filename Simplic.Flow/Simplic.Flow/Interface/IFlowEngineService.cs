using Simplic.Flow.Event;

namespace Simplic.Flow
{
    public interface IFlowEngineService
    {
        void Run();
        void EnqueueEvent(FlowEventArgs args);
    }
}
