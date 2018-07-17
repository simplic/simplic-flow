using Simplic.Collections.Generic;
using Simplic.Flow.Event;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.Flow.Service
{
    public class FlowRuntimeService : IFlowRuntimeService
    {
        private Dequeue<NodeScope<ActionNode>> nextNodes = new Dequeue<NodeScope<ActionNode>>();
        private IList<NodeScope<ActionNode>> tempNextNodes = new List<NodeScope<ActionNode>>();
        private FlowInstance.FlowInstance instance;

        public void Run(FlowInstance.FlowInstance instance, EventCall call)
        {
            this.instance = instance;

            if (!instance.CurrentNodes.Any())
            {
                // Start new
                foreach (var startNode in instance.Flow.Nodes.OfType<EventNode>().Where(
                    x => x.IsStartEvent && x.Id == call.Delegate.EventNodeId))
                {
                    // Pass arguments to event
                    startNode.Arguments = call.Args;

                    // Execute event
                    Execute(new NodeScope<EventNode> { Node = startNode, NodeId = startNode.Id, Scope = instance.DataScope });
                }
            }
            else
            {
                var executedEvents = new List<NodeScope<EventNode>>();
                foreach (var eventNode in instance.CurrentNodes.Where(x => x.NodeId == call.Delegate.EventNodeId))
                {
                    eventNode.Node.Arguments = call.Args;

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

                    //TODO: should we save the state here ?
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