using Simplic.Flow.Event;

namespace Simplic.Flow.Node
{
    public class StartWithConditionNode : ConditionNode
    {
        protected override bool Compare(IFlowRuntimeService runtime, FlowEventArgs args, DataPinScope scope)
        {
            var val1 = scope.GetValue<string>(InPinCondition1);
            var val2 = scope.GetValue<string>(InPinCondition);

            return val1.StartsWith(val2);
        }

        public override string FriendlyName
        {
            get
            {
                return nameof(StartWithConditionNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(StartWithConditionNode);
            }
        }
    }
}