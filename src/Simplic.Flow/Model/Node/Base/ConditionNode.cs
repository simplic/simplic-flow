using Simplic.Flow.Event;

namespace Simplic.Flow
{
    public abstract class ConditionNode : ActionNode
    {
        public override string FriendlyName { get; }
        
        public override bool Execute(IFlowRuntimeService runtime, FlowEventArgs args, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            var result = Compare(runtime, args, scope);

            if (result)
                runtime.EnqueueNode(OutNodeTrue, scope);
            else
                runtime.EnqueueNode(OutNodeFalse, scope);

            runtime.EnqueueNode(OutNodeAny, scope);

            return true;
        }

        protected abstract bool Compare(IFlowRuntimeService runtime, FlowEventArgs args, DataPinScope scope);

        public ActionNode OutNodeTrue { get; set; }
        public ActionNode OutNodeFalse { get; set; }
        public ActionNode OutNodeAny { get; set; }
        public DataPin InPinCondition1 { get; set; }
        public DataPin InPinCondition { get; set; }
        public DataPin OutPinBoolean { get; set; }
    }
}
