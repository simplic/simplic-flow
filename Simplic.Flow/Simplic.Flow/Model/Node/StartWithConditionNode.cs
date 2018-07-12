namespace Simplic.Flow
{
    public class StartWithConditionNode : ConditionNode
    {
        protected override bool Compare(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var val1 = scope.GetValue<string>(ConditionPinIn1);
            var val2 = scope.GetValue<string>(ConditionPinIn2);

            return val1.StartsWith(val2);
        }
    }
}
