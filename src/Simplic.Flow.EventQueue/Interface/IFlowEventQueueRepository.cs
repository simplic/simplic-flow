using System;
using System.Collections.Generic;

namespace Simplic.Flow.EventQueue
{
    public interface IFlowEventQueueRepository
    {
        IEnumerable<EventQueueModel> GetAll();
        IEnumerable<EventQueueModel> GetAllUnhandled(string machineName, string serviceName);
        EventQueueModel Get(Guid id);
        bool Save(EventQueueModel model);
        bool SetHandled(Guid id, bool isHandled);
        bool SetFailed(Guid eventQueueId);
        void ClearEventQueue();
        void Remove(Guid queueId);
        IList<EventServiceTarget> GetEventTargets();
    }
}
