using System;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "End With Condition", Name = "EndWithConditionNode", Category = "Common")]
    public class EndWithConditionNode : ConditionNode
    {
        protected override bool Compare(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var val1 = scope.GetValue<string>(InPinConditionA);
            var val2 = scope.GetValue<string>(InPinConditionB);

            if(string.IsNullOrWhiteSpace(val1) || string.IsNullOrWhiteSpace(val2))
            {
                return false;
            }

            return val1.EndsWith(val2);
        }

        public override string FriendlyName { get { return nameof(EndWithConditionNode); } }
        public override string Name { get { return nameof(EndWithConditionNode); } }
    }
}