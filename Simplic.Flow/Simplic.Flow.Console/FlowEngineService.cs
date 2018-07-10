using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simplic.Flow.Event;

namespace Simplic.Flow.Console
{
    public class FlowEngineService : IFLowEngineService
    {
        private IList<Flow> flows;
        private IList<Flow> activeFlows;
        private IDictionary<string, IList<EventDelegate>> eventDelegates;

        public FlowEngineService()
        {
            flows = new List<Flow>();
            activeFlows = new List<Flow>();
        }

        public void Run()
        {
            // Run a single cycle
        }

        public void EnqueueEvent(FlowEventArgs args)
        {
            // Find workflow
            var delegates = eventDelegates.FirstOrDefault(x => x.Key == args.EventName).Value;
            if (delegates != null && delegates.Count > 0)
            {
                
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

        public IList<Flow> ActiveFlows
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
