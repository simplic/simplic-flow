namespace Simplic.Flow
{
    public abstract class ActionNode : Node
    {
        public abstract bool Execute(IFlowRuntimeService runtime);
    }
}
