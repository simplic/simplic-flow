namespace Simplic.Flow.Service
{
    /// <summary>
    /// Holds an information about a thread being processed
    /// </summary>
    public class ThreadStateInfo
    {
        public FlowInstance FlowInstance { get; set; }
        public ActiveFlow.ActiveFlow ActiveFlow { get; set; }
        public EventCall EventCall { get; set; }
        public bool IsStartEvent { get; set; }
    }
}
