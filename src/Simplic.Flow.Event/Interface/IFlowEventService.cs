namespace Simplic.Flow.Event
{
    public interface IFlowEventService
    {
        void InvokeEvent(FlowEventArgs args);
        void InvokeEvent(string eventName, object objectId, object obj, int userId);
    }
}
