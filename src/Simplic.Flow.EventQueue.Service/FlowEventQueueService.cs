using System;
using System.Collections.Generic;

namespace Simplic.Flow.EventQueue.Service
{
    public class FlowEventQueueService : IFlowEventQueueService
    {
        private readonly IFlowEventQueueRepository flowEventQueueRepository;

        public FlowEventQueueService(IFlowEventQueueRepository flowEventQueueRepository)
        {
            this.flowEventQueueRepository = flowEventQueueRepository;
        }

        public EventQueueModel Get(string id)
        {
            return flowEventQueueRepository.Get(id);
        }

        public IEnumerable<EventQueueModel> GetAll()
        {
            return flowEventQueueRepository.GetAll();
        }

        public IList<EventServiceTarget> GetEventTargets() => flowEventQueueRepository.GetEventTargets();

        public IEnumerable<EventQueueModel> GetAllUnhandled(string machineName, string serviceName)
        {
            return flowEventQueueRepository.GetAllUnhandled(machineName, serviceName);
        }

        public bool Save(EventQueueModel model)
        {
            if (model.CreateDateTime == default(DateTime))
                model.CreateDateTime = DateTime.Now;

            return flowEventQueueRepository.Save(model);
        }

        public bool SetHandled(string id, bool isHandled)
        {
            return flowEventQueueRepository.SetHandled(id, isHandled);
        }

        public void ClearEventQueue()
        {
            flowEventQueueRepository.ClearEventQueue();
        }

        public void Remove(string id) => flowEventQueueRepository.Remove(id);

        public bool SetFailed(string id) => flowEventQueueRepository.SetFailed(id);
    }
}