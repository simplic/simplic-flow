using System;
using System.Collections.Generic;

namespace Simplic.FlowInstance
{
    public interface IFlowInstanceRepository
    {
        FlowInstance GetById(Guid flowInstanceId);
        IEnumerable<FlowInstance> GetAll();
        bool Save(FlowInstance flowInstance);        
    }
}
