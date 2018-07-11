using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simplic.Flow.Event;
using Simplic.Collections.Generic;

namespace Simplic.Flow.Console
{
    public class FlowEngineService : IFlowEngineService
    {
        private IList<Flow> flows;
        private IList<FlowInstance> activeFlows;
        private IDictionary<string, IList<EventDelegate>> eventDelegates;
        private Dequeue<EventQueue> eventQueue;

        public FlowEngineService()
        {
            flows = new List<Flow>();
            activeFlows = new List<FlowInstance>();
            eventQueue = new Dequeue<EventQueue>();
            eventDelegates = new Dictionary<string, IList<EventDelegate>>();
        }

        /// <summary>
        /// Run a single cycle
        /// </summary>
        public void Run()
        {
            // Run a single cycle

            // pop event entries from queue first
            while (eventQueue.Count > 0)
            {
                var queueEntry = eventQueue.PopFirst();
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
                    eventQueue.PushBack(new EventQueue { Args = args, Delegate = del });
                }
            }
        }

        public IEnumerable<FlowEventArgs> ReadPersistantEvents()
        {
            // Return WorkflowEventArgs from database
            return new List<FlowEventArgs>();
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

                    if (eventDelegates.ContainsKey(eventNode.EventName))
                    {
                        eventDelegateList = new List<EventDelegate>();
                        eventDelegates[eventNode.EventName] = eventDelegateList;
                    }
                    else
                        eventDelegateList = eventDelegates[eventNode.EventName];

                    eventDelegateList.Add(new EventDelegate { FlowId = flow.Id, EventName = eventNode.EventName });
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
