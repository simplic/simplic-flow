using Simplic.Flow.EventQueue;

namespace Simplic.Flow.Event.Service
{
    public class FlowEventService : IFlowEventService
    {
        private readonly IFlowEventQueueService flowEventQueueService;

        public FlowEventService(IFlowEventQueueService flowEventQueueService)
        {
            this.flowEventQueueService = flowEventQueueService;
        }

        public void InvokeEvent(FlowEventArgs args)
        {
            var argsByte = new byte[] { };

            flowEventQueueService.Save(new EventQueueModel
            {
                Id = args.QueueId,
                EventName = args.EventName,
                Handled = false,
                CreateUserId = args.UserId,
                Args = argsByte
            });
        }

        public void InvokeEvent(string eventName, object objectId, object obj, int userId)
        {
            InvokeEvent(new FlowEventArgs
            {
                EventName = eventName,
                ObjectId = objectId,
                UserId = userId,
                Object = obj
            });
        }
    }
}