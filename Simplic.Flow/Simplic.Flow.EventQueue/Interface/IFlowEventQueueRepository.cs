using System;
using System.Collections.Generic;

namespace Simplic.Flow.EventQueue
{
    public interface IFlowEventQueueRepository
    {
        IEnumerable<EventQueueModel> GetAll();
        IEnumerable<EventQueueModel> GetAllUnhandled();
        EventQueueModel Get(Guid id);
        bool Save(EventQueueModel model);
        bool SetHandled(Guid id, bool isHandled);        
    }
}
