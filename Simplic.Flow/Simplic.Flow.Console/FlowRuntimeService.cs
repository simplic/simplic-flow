using Simplic.Collections.Generic;
using Simplic.Flow.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class FlowRuntimeService : IFlowRuntimeService
    {
        private Dequeue<ActionNode> nextNodes = new Dequeue<ActionNode>();
        private IList<ActionNode> tempNextNodes = new List<ActionNode>();
        private FlowInstance instance;

        public void Run(FlowInstance instance, EventCall call)
        {
            this.instance = instance;

            if (!instance.CurrentNodes.Any())
            {
                // Start new
                foreach (var startNode in instance.Flow.Nodes.OfType<EventNode>().Where(x => x.IsStartEvent && x.EventName == call.Delegate.EventName))
                {
                    // Pass arguments to event
                    startNode.Arguments = call.Args;

                    // Execute event
                    Execute(startNode);
                }
            }
            else
            {
                var executedEvents = new List<EventNode>();
                foreach (var eventNode in instance.CurrentNodes.Where(x => x.EventName == call.Delegate.EventName))
                {
                    eventNode.Arguments = call.Args;

                    if (Execute(eventNode))
                        executedEvents.Add(eventNode);
                }

                // Remove continued events
                foreach (var executedEvent in executedEvents)
                    instance.CurrentNodes.Remove(executedEvent);

                executedEvents.Clear();
            }

            while (nextNodes.Any())
            {
                var nextNode = nextNodes.PopFirst();

                if (nextNode is EventNode)
                    instance.CurrentNodes.Add(nextNode as EventNode);
                else
                    Execute(nextNode);
            }
        }

        public bool Execute(ActionNode node)
        {
            tempNextNodes = new List<ActionNode>();

            if (node.Execute(this))
            {
                foreach (var nextNode in tempNextNodes)
                    if (nextNode != null)
                        nextNodes.PushBack(nextNode);

                return true;
            }

            return false;
        }

        public void EnqueueNode(ActionNode node, params PinScope[] scope)
        {
            tempNextNodes.Add(node);
        }

        public T GetValue<T>(DataPin inPin)
        {
            var value = (T)instance.PinValues.FirstOrDefault(x => x.Key == inPin.Id).Value;

            // Check

            return value;
        }

        public IList<T> GetListValue<T>(DataPin inPin)
        {
            var value = (IList<T>)instance.PinValues.FirstOrDefault(x => x.Key == inPin.Id).Value;

            // Check

            return value;
        }

        public void SetValue(DataPin outPin, object value)
        {
            instance.PinValues[outPin.Id] = value;
        }
    }
}
