using System;

namespace Simplic.Flow.Node
{
    public class StartWithConditionNode : ConditionNode
    {
        protected override bool Compare(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var val1 = scope.GetValue<string>(InPinConditionA);
            var val2 = scope.GetValue<string>(InPinConditionB);

            return val1.StartsWith(val2);
        }

        public override string FriendlyName { get { return nameof(StartWithConditionNode); } }
        public override string Name { get { return nameof(StartWithConditionNode); } }
    }
}