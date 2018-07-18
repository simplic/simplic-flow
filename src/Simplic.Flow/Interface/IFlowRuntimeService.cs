using System.Collections.Generic;

namespace Simplic.Flow
{
    public interface IFlowRuntimeService
    {
        bool EnqueueNode(ActionNode node, DataPinScope scope);
    }
}