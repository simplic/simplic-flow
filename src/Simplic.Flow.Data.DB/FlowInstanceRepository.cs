using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Data.DB
{
    public class FlowInstanceRepository : IFlowInstanceRepository
    {
        public IEnumerable<FlowInstance> GetAll()
        {
            throw new NotImplementedException();
        }

        public FlowInstance GetById(Guid instanceId)
        {
            throw new NotImplementedException();
        }

        public bool Save(FlowInstance flowInstance)
        {
            throw new NotImplementedException();
        }
    }
}
