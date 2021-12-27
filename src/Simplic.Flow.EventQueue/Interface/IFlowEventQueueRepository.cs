using System;
using System.Collections.Generic;

namespace Simplic.Flow.EventQueue
{
    public interface IFlowEventQueueRepository
    {
        IEnumerable<EventQueueModel> GetAll();
        IEnumerable<EventQueueModel> GetAllUnhandled(string machineName, string serviceName);
        EventQueueModel Get(string id);
        bool Save(EventQueueModel model);
        bool SetHandled(string id, bool isHandled);
        bool SetFailed(string id);
        void ClearEventQueue();
        void Remove(string id);
        IList<EventServiceTarget> GetEventTargets();
    }
}
