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

        public EventQueueModel Get(Guid id)
        {
            return flowEventQueueRepository.Get(id);
        }

        public IEnumerable<EventQueueModel> GetAll()
        {
            return flowEventQueueRepository.GetAll();
        }

        public IEnumerable<EventQueueModel> GetAllUnhandled()
        {
            return flowEventQueueRepository.GetAllUnhandled();
        }

        public bool Save(EventQueueModel model)
        {
            if (model.CreateDateTime == default(DateTime))
                model.CreateDateTime = DateTime.Now;

            return flowEventQueueRepository.Save(model);
        }

        public bool SetHandled(Guid id, bool isHandled)
        {
            return flowEventQueueRepository.SetHandled(id, isHandled);
        }

        public void ClearEventQueue()
        {
            flowEventQueueRepository.ClearEventQueue();
        }

        public void Remove(Guid id) => flowEventQueueRepository.Remove(id);

        public bool SetFailed(Guid id) => flowEventQueueRepository.SetFailed(id);
    }
}