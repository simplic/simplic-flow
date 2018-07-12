namespace Simplic.Flow.Event
{
    public class EventCall
    {
        public EventDelegate Delegate { get; set; }
        public FlowEventArgs Args { get; set; }
    }
}
