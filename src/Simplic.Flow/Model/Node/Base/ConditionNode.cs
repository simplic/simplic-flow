namespace Simplic.Flow
{
    public abstract class ConditionNode : ActionNode
    {
        public override string FriendlyName { get; }
        
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            System.Console.WriteLine($"Execute: {GetType().Name}");

            var result = Compare(runtime, scope);

            if (result)
                runtime.EnqueueNode(OutNodeTrue, scope);
            else
                runtime.EnqueueNode(OutNodeFalse, scope);

            runtime.EnqueueNode(OutNodeAny, scope);

            return true;
        }

        protected abstract bool Compare(IFlowRuntimeService runtime, DataPinScope scope);

        public ActionNode OutNodeTrue { get; set; }
        public ActionNode OutNodeFalse { get; set; }
        public ActionNode OutNodeAny { get; set; }
        public DataPin InPinConditionA { get; set; }
        public DataPin InPinConditionB { get; set; }
        public DataPin OutPinBoolean { get; set; }
    }
}
