using Simplic.Collections.Generic;
using Simplic.Flow.Event;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.Flow.Service
{
    public class FlowEngineService : IFlowEngineService
    {
        private IList<Flow> flows;
        private IList<FlowInstance> activeFlows;
        private IDictionary<string, IList<EventDelegate>> eventDelegates;
        private Dequeue<EventCall> eventQueue;
        private readonly IFlowInstanceRepository flowInstanceRepository;

        public FlowEngineService(IFlowInstanceRepository flowInstanceRepository)
        {
            flows = new List<Flow>();
            activeFlows = new List<FlowInstance>();
            eventQueue = new Dequeue<EventCall>();
            eventDelegates = new Dictionary<string, IList<EventDelegate>>();
            this.flowInstanceRepository = flowInstanceRepository;

            activeFlows = flowInstanceRepository.GetAll().ToList();
        }
        
        /// <summary>
        /// Run a single cycle
        /// </summary>
        public void Run()
        {
            // Run a single cycle

            // pop event entries from queue first
            var newActiveFlows = new List<FlowInstance>();

            while (eventQueue.Count > 0)
            {
                var queueEntry = eventQueue.PopFirst();

                if (!queueEntry.Delegate.IsStartEvent)
                {
                    var finishedInstances = new List<FlowInstance>();

                    // Notify ALL instances, which MIGHT BE continued
                    foreach (var activeFlow in activeFlows.Where(x => x.Flow.Id == queueEntry.Delegate.FlowId))
                    {
                        System.Console.WriteLine("---- CONTINUE FLOW ----");

                        var runtime = new FlowRuntimeService();
                        runtime.Run(activeFlow, queueEntry);

                        if (!activeFlow.IsAlive)
                            finishedInstances.Add(activeFlow);
                    }

                    foreach(var flowInstance in finishedInstances)
                    {
                        // we call save instead of set as finished, as the IsAlive property already set to false
                        // flowInstanceRepository.SetAsFinished(flowInstance);
                        flowInstanceRepository.Save(flowInstance);
                        activeFlows.Remove(flowInstance);
                    }
                }
                else
                {
                    System.Console.WriteLine("---- NEW FLOW----");
                    var runtime = new FlowRuntimeService();
                    var newFlow = new FlowInstance
                    {
                        Flow = flows.FirstOrDefault(x => x.Id == queueEntry.Delegate.FlowId),
                        Id = Guid.NewGuid()
                    };

                    runtime.Run(newFlow, queueEntry);
                    newActiveFlows.Add(newFlow);
                }
            }

            foreach (var newFlowInstance in newActiveFlows)
            {
                if (newFlowInstance.IsAlive)
                    activeFlows.Add(newFlowInstance);

                flowInstanceRepository.Save(newFlowInstance);
            }
        }

        /// <summary>
        /// Enqueue event
        /// </summary>
        /// <param name="args"></param>
        public void EnqueueEvent(FlowEventArgs args)
        {
            // Find workflow
            var delegates = eventDelegates.FirstOrDefault(x => x.Key == args.EventName).Value;
            if (delegates != null && delegates.Count > 0)
            {
                foreach (var del in delegates)
                {
                    eventQueue.PushBack(new EventCall { Args = args, Delegate = del });
                }
            }
        }

        /// <summary>
        /// Cash event delegates
        /// </summary>
        public void RefreshEventDelegates()
        {
            eventDelegates = new Dictionary<string, IList<EventDelegate>>();
            foreach (var flow in flows)
            {
                foreach (var eventNode in flow.Nodes.OfType<EventNode>())
                {
                    IList<EventDelegate> eventDelegateList = null;

                    if (!eventDelegates.ContainsKey(eventNode.EventName))
                    {
                        eventDelegateList = new List<EventDelegate>();
                        eventDelegates[eventNode.EventName] = eventDelegateList;
                    }
                    else
                        eventDelegateList = eventDelegates[eventNode.EventName];

                    eventDelegateList.Add(new EventDelegate
                    {
                        FlowId = flow.Id,
                        EventName = eventNode.EventName,
                        EventNodeId = eventNode.Id,
                        IsStartEvent = eventNode.IsStartEvent
                    });
                }
            }
        }

        public IList<FlowInstance> ActiveFlows
        {
            get
            {
                return activeFlows;
            }

            set
            {
                activeFlows = value;
            }
        }

        public IList<Flow> Flows
        {
            get
            {
                return flows;
            }

            set
            {
                flows = value;
            }
        }
    }
}
