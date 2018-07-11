using System.Collections.Generic;

namespace Simplic.Flow
{
    public interface IFlowRuntimeService
    {
        void EnqueueNode(ActionNode node, ValueScope scope);
    }
}