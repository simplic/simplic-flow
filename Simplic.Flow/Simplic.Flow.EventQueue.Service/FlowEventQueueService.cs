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
            return flowEventQueueRepository.Save(model);
        }

        public bool SetHandled(Guid id, bool isHandled)
        {
            return flowEventQueueRepository.SetHandled(id, isHandled);
        }
    }
}