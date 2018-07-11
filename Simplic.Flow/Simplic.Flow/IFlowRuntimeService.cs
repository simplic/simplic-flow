using System.Collections.Generic;

namespace Simplic.Flow
{
    public interface IFlowRuntimeService
    {
        T GetValue<T>(DataPin inPin);
        IList<T> GetListValue<T>(DataPin inPin);
        void SetValue(DataPin pin, object value);
        void EnqueueNode(ActionNode node, params PinScope[] scope);
    }
}