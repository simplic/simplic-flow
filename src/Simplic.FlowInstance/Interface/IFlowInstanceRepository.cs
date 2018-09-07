using System;
using System.Collections.Generic;
using Simplic.Flow;

namespace Simplic.FlowInstance
{
    public interface IFlowInstanceRepository
    {
        Flow.FlowInstance GetById(Guid flowInstanceId);
        IEnumerable<Flow.FlowInstance> GetAll();
        IEnumerable<Flow.FlowInstance> GetAllAlive();
        bool Save(Flow.FlowInstance flowInstance);        
    }
}
