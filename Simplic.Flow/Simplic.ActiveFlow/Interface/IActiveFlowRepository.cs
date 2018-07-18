using System;
using System.Collections.Generic;

namespace Simplic.ActiveFlow
{
    public interface IActiveFlowRepository
    {
        ActiveFlow Get(Guid id);
        IEnumerable<ActiveFlow> GetAll();
        IEnumerable<ActiveFlow> GetAllAlive();
        bool Save(ActiveFlow activeFlow);
    }
}
