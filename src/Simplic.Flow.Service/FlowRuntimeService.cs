using Simplic.Collections.Generic;
using Simplic.Flow.Event;
using Simplic.Flow.Log;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.Flow.Service
{
    public class FlowRuntimeService : IFlowRuntimeService
    {
        private Dequeue<NodeScope<ActionNode>> nextNodes = new Dequeue<NodeScope<ActionNode>>();
        private IList<NodeScope<ActionNode>> tempNextNodes = new List<NodeScope<ActionNode>>();
        private readonly IFlowLogService flowLogService;
        private FlowInstance instance;
        private EventCall eventCall;

        public FlowEventArgs FlowEventArgs { get; private set; }
        public FlowInstance Instance { get { return instance; } }

        /// <summary>
        /// Initialize new runtime instance
        /// </summary>
        /// <param name="flowLogService">Log service</param>
        public FlowRuntimeService(IFlowLogService flowLogService, FlowEventArgs flowEventArgs)
        {
            this.flowLogService = flowLogService;
            this.FlowEventArgs = flowEventArgs;
        }

        public void Run(FlowInstance instance, EventCall call)
        {
            this.instance = instance;
            this.eventCall = call;

            if (!instance.CurrentNodes.Any())
            {
                // Start new
                foreach (var startNode in instance.Flow.Nodes.OfType<EventNode>().Where(
                    x => x.IsStartEvent && x.Id == call.Delegate.EventNodeId))
                {
                    // Pass arguments to event
                    if (!startNode.ShouldExecute(instance.DataScope))
                        continue;

                    // Execute event
                    Execute(new NodeScope<EventNode> { Node = startNode, NodeId = startNode.Id, Scope = instance.DataScope });
                }
            }
            else
            {
                var executedEvents = new List<NodeScope<EventNode>>();
                foreach (var eventNode in instance.CurrentNodes.Where(x => x.NodeId == call.Delegate.EventNodeId))
                {
                    if (!eventNode.Node.ShouldExecute(eventNode.Scope))
                        continue;

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

                if (nextNode.Node is EventNode)
                {
                    instance.CurrentNodes.Add(new NodeScope<EventNode>
                    {
                        Node = nextNode.Node as EventNode,
                        Scope = nextNode.Scope,
                        NodeId = nextNode.NodeId
                    });                    
                }
                else
                {
                    Execute(nextNode);
                }
            }
        }

        public bool Execute<T>(NodeScope<T> nodeScope) where T : ActionNode
        {
            tempNextNodes = new List<NodeScope<ActionNode>>();

            if (nodeScope.Node.Execute(this, nodeScope.Scope))
            {
                foreach (var nextNode in tempNextNodes)
                    if (nextNode != null)
                        nextNodes.PushBack(nextNode);

                return true;
            }

            return false;
        }

        public bool EnqueueNode(ActionNode node, DataPinScope scope)
        {
            // Ensure the node is set
            if (node != null)
            {
                tempNextNodes.Add(new NodeScope<ActionNode> { Node = node, NodeId = node.Id, Scope = scope });
                return true;
            }

            return false;
        }
    }
}