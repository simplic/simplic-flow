using Simplic.Collections.Generic;
using Simplic.Flow.Configuration;
using Simplic.Flow.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Simplic.Flow.Service
{
    public class FlowEngineService : IFlowEngineService
    {
        private IList<Flow> flows;
        private IList<FlowInstance> activeFlows;
        private IList<FlowConfiguration> flowConfigurations;
        private IDictionary<string, IList<EventDelegate>> eventDelegates;
        private Dequeue<EventCall> eventQueue;
        private readonly IFlowInstanceRepository flowInstanceRepository;
        private readonly IFlowConfigurationService flowConfigurationService;
        private readonly IUnityContainer unityContainer;

        public FlowEngineService(IFlowInstanceRepository flowInstanceRepository, IFlowConfigurationService flowConfigurationService)
        {
            flows = new List<Flow>();
            activeFlows = new List<FlowInstance>();
            eventQueue = new Dequeue<EventCall>();
            eventDelegates = new Dictionary<string, IList<EventDelegate>>();
            this.flowInstanceRepository = flowInstanceRepository;
            this.flowConfigurationService = flowConfigurationService;

            activeFlows = flowInstanceRepository.GetAll().ToList();
            flowConfigurations = flowConfigurationService.GetAll().ToList();

            unityContainer = new UnityContainer();            
            unityContainer.RegisterType<INodeResolver, ConsoleWriteLineNodeResolver>("ConsoleWriteLineNode");
            unityContainer.RegisterType<INodeResolver, ForeachNodeResolver>("ForeachNode");
            unityContainer.RegisterType<INodeResolver, OnDocumentScannedNodeResolver>("OnDocumentScannedNode");
            unityContainer.RegisterType<INodeResolver, SequenceNodeResolver>("SequenceNode");
            unityContainer.RegisterType<INodeResolver, StartWithConditionNodeResolver>("StartWithConditionNode");

            flows = CreateFlowsFromConfiguration();
        }

        private IList<Flow> CreateFlowsFromConfiguration()
        {
            var list = new List<Flow>();
            foreach (var flowConfiguration in flowConfigurations)
            {
                var nodes = new List<Node>();
                foreach (var node in flowConfiguration.Nodes)
                {
                    var resolver = unityContainer.Resolve<INodeResolver>($"{node.ClassName}");
                    nodes.Add(resolver.Create(node.Id, node.IsStartEvent));
                }

                foreach (var pin in flowConfiguration.Pins)
                {
                    // Find from to pin
                    var fromNode = nodes.FirstOrDefault(x => x.Id == pin.From.NodeId);
                    var toNode = nodes.FirstOrDefault(x => x.Id == pin.To.NodeId);

                    // Find from member
                    var fromProperty = fromNode.GetType().GetProperty(pin.From.PinName,
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                        
                    var toProperty = toNode.GetType().GetProperty(pin.To.PinName,
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                    // Set properpty
                    toProperty.SetValue(toNode, fromProperty.GetValue(fromNode));
                }
                
                foreach (var link in flowConfiguration.Links)
                {
                    // Find from to pin
                    var fromNode = nodes.FirstOrDefault(x => x.Id == link.From.NodeId);
                    var toNode = nodes.FirstOrDefault(x => x.Id == link.To.NodeId);

                    // Find from property
                    var property = fromNode.GetType().GetProperty(link.From.PinName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                    /* this one is tricky.
                     * here we check if the property is a list.
                     * if so, we get the value of the property (which is type of IList)
                     * and find the Add method and invoke it.
                     */
                    if (property.PropertyType.Name == "IList`1")
                    {                        
                        // retrieves current List value to call Add method
                        var customList = property.GetValue(fromNode);
                        
                        // gets metadata of the List.Add method
                        var addMethod = customList.GetType().GetMethod("Add");
                        addMethod.Invoke(customList, new object[] { toNode });                       
                    }
                    else
                    {
                        property.SetValue(fromNode, toNode);
                    }                                                
                }

                list.Add(new Flow
                {
                    Id = flowConfiguration.Id,
                    Name = flowConfiguration.Name,
                    Nodes = nodes
                });
            }

            return list;
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
