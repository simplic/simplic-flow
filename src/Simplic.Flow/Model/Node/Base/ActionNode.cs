using Simplic.Flow.Event;

namespace Simplic.Flow
{
    public abstract class ActionNode : BaseNode
    {
        public abstract bool Execute(IFlowRuntimeService runtime, DataPinScope scope);
    }
}
