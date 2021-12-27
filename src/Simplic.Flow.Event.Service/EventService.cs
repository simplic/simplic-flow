using Newtonsoft.Json;
using Simplic.Flow.EventQueue;
using System.Text;

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
            var argsByte = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(args, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            }));

            flowEventQueueService.Save(new EventQueueModel
            {
                Id = args.Id,
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
                Id = $"{eventName}_{objectId}",
                EventName = eventName,
                ObjectId = objectId,
                UserId = userId,
                Object = obj
            });
        }
    }
}