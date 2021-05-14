using Simplic.Flow.Event;

namespace Simplic.Flow.Node
{
    public class OnExecuteFlowEventArgs : FlowEventArgs
    {
        public string Target { get; set; }
        public object Data01{ get; set; }
        public object Data02{ get; set; }
        public object Data03{ get; set; }
    }
}
