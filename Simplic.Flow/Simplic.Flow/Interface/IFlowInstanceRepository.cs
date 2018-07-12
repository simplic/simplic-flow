using System;
using System.Collections.Generic;

namespace Simplic.Flow
{
    public interface IFlowInstanceRepository
    {
        FlowInstance GetById(Guid instanceId);
        IEnumerable<FlowInstance> GetAll();
        bool Save(FlowInstance flowInstance);
    }
}
