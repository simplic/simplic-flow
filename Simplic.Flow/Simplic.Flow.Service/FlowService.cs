using Newtonsoft.Json;
using Simplic.Collections.Generic;
using Simplic.Flow.Configuration;
using Simplic.Flow.Event;
using Simplic.Flow.EventQueue;
using Simplic.Flow.Log;
using Simplic.Flow.Node;
using Simplic.FlowInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;

namespace Simplic.Flow.Service
{
    public class FlowService : IFlowService
    {
        #region Private Members
        private IList<Flow> flows;
        private IList<ActiveFlow.ActiveFlow> activeFlows;
        private IList<FlowConfiguration> flowConfigurations;
        private IDictionary<string, IList<EventDelegate>> eventDelegates;
        private Dequeue<EventCall> eventQueue;

        private readonly IUnityContainer unityContainer;
        private readonly IFlowInstanceService flowInstanceService;
        private readonly IFlowConfigurationService flowConfigurationService;
        private readonly IFlowEventQueueService flowEventQueueService;
        private readonly IFlowEventService flowEventService;
        private readonly IFlowLogService flowLogService;
        #endregion

        #region Constructor
        public FlowService(IUnityContainer unityContainer)
        {
            flows = new List<Flow>();
            activeFlows = new List<Simplic.ActiveFlow.ActiveFlow>();
            eventQueue = new Dequeue<EventCall>();
            eventDelegates = new Dictionary<string, IList<EventDelegate>>();

            this.flowInstanceService = unityContainer.Resolve<IFlowInstanceService>();
            this.flowConfigurationService = unityContainer.Resolve<IFlowConfigurationService>();
            this.flowEventQueueService = unityContainer.Resolve<IFlowEventQueueService>();
            this.flowEventService = unityContainer.Resolve<IFlowEventService>();
            this.flowLogService = unityContainer.Resolve<IFlowLogService>();

            unityContainer.RegisterType<INodeResolver, ConsoleWriteLineNodeResolver>("ConsoleWriteLineNode");
            unityContainer.RegisterType<INodeResolver, ForeachNodeResolver>("ForeachNode");
            unityContainer.RegisterType<INodeResolver, OnDocumentScannedNodeResolver>("OnDocumentScannedNode");
            unityContainer.RegisterType<INodeResolver, SequenceNodeResolver>("SequenceNode");
            unityContainer.RegisterType<INodeResolver, StartWithConditionNodeResolver>("StartWithConditionNode");
            
            flowConfigurations = flowConfigurationService.GetAll().ToList();
            flows = CreateFlowsFromConfiguration();
            CreateActiveFlowsFrom(flowInstanceService.GetAll().ToList());
        }
        #endregion

        #region Private Methods

        #region [CreateActiveFlowsFrom]
        /// <summary>
        /// Creates <see cref="ActiveFlow.ActiveFlow"/> objects from a list of <see cref="FlowInstance.FlowInstance"/> objects
        /// </summary>
        /// <param name="flowInstances">Objects to create from</param>
        private void CreateActiveFlowsFrom(List<FlowInstance.FlowInstance> flowInstances)
        {
            foreach (var flowInstance in flowInstances)
            {
                if (flowInstance.IsAlive)
                {
                    var activeFlow = new ActiveFlow.ActiveFlow
                    {
                        FlowInstanceId = flowInstance.Id,
                        CurrentEventNodes = flowInstance.CurrentNodes,
                        FlowId = flowInstance.Flow.Id

                    };
                    activeFlows.Add(activeFlow);
                }

                flowInstanceService.Save(flowInstance);
            }
        }
        #endregion

        #region [CreateFlowsFromConfiguration]
        /// <summary>
        /// Creates a list <see cref="Flow"/> objects from <see cref="FlowConfiguration"/> objects
        /// </summary>
        /// <returns>A list <see cref="Flow"/> objects</returns>
        private IList<Flow> CreateFlowsFromConfiguration()
        {
            var list = new List<Flow>();
            foreach (var flowConfiguration in flowConfigurations)
            {
                var nodes = new List<BaseNode>();
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
        #endregion

        #region [LoadEventQueue]
        /// <summary>
        /// Loads the event queue from the repository
        /// </summary>
        private void LoadEventQueue()
        {
            foreach (var flowEventQueue in flowEventQueueService.GetAllUnhandled())
            {
                var eventArgs = JsonConvert.DeserializeObject<FlowEventArgs>(
                    Encoding.UTF8.GetString(flowEventQueue.Args),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                eventArgs.QueueId = flowEventQueue.Id;
                eventArgs.UserId = flowEventQueue.CreateUserId;

                EnqueueEvent(eventArgs);
            }
        }
        #endregion

        #region [SetEventQueueHandled]
        /// <summary>
        /// Sets an event queue to handled
        /// </summary>
        /// <param name="eventQueueId">Event queue to update</param>
        private bool SetEventQueueHandled(Guid eventQueueId)
        {
            return flowEventQueueService.SetHandled(eventQueueId, true);
        }
        #endregion

        #endregion

        #region Public Methods

        #region [Run]
        /// <summary>
        /// Run a single cycle
        /// </summary>
        public void Run()
        {
            try
            {
                // load event queue from db            
                LoadEventQueue();

                // pop event entries from queue first
                var newFlowInstances = new List<FlowInstance.FlowInstance>();

                while (eventQueue.Count > 0)
                {
                    var queueEntry = eventQueue.PopFirst();

                    if (!queueEntry.Delegate.IsStartEvent)
                    {
                        var finishedInstances = new List<ActiveFlow.ActiveFlow>();

                        // Notify ALL instances, which MIGHT BE continued
                        foreach (var activeFlow in activeFlows.Where(x => x.FlowId == queueEntry.Delegate.FlowId))
                        {
                            System.Console.WriteLine("---- CONTINUE FLOW ----");

                            var flowInstance = flowInstanceService.GetById(activeFlow.FlowInstanceId);

                            try
                            {
                                var runtime = new FlowRuntimeService();
                                runtime.Run(flowInstance, queueEntry);

                                if (!flowInstance.IsAlive)
                                    finishedInstances.Add(activeFlow);
                                                       
                                flowInstanceService.Save(flowInstance);
                            }
                            catch (Exception ex)
                            {
                                // Cancel active flow
                                flowInstance.IsFailed = true;

                                finishedInstances.Add(activeFlow);

                                // log the exception

                                flowInstanceService.Save(flowInstance);
                                throw;
                            }
                        }

                        foreach (var activeFlow in finishedInstances)
                        {
                            activeFlows.Remove(activeFlow);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("---- NEW FLOW----");
                        var runtime = new FlowRuntimeService();
                        var newFlow = new FlowInstance.FlowInstance
                        {
                            Flow = flows.FirstOrDefault(x => x.Id == queueEntry.Delegate.FlowId),
                            Id = Guid.NewGuid()
                        };

                        try
                        {
                            runtime.Run(newFlow, queueEntry);
                            newFlowInstances.Add(newFlow);
                        }
                        catch (Exception ex)
                        {
                            newFlow.IsFailed = true;
                            flowInstanceService.Save(newFlow);
                            throw;
                        }
                    }

                    // change the event status to handled and save to the database
                    SetEventQueueHandled(queueEntry.QueueId);
                }

                CreateActiveFlowsFrom(newFlowInstances);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region [EnqueueEvent]
        /// <summary>
        /// Enqueue event
        /// </summary>
        /// <param name="args"></param>
        public void EnqueueEvent(FlowEventArgs args)
        {
            flowLogService.Info($"Enqueue event: {args.EventName} / object id {args.ObjectId ?? "<unset>"}");

            // Find workflow
            var delegates = eventDelegates.FirstOrDefault(x => x.Key == args.EventName).Value;
            if (delegates != null && delegates.Count > 0)
            {
                foreach (var del in delegates)
                {
                    flowLogService.Info($"Create event call: {args.EventName} / Flow: {del.FlowId} / Is start event {del.IsStartEvent}");

                    eventQueue.PushBack(new EventCall { QueueId = args.QueueId, Args = args, Delegate = del });
                }
            }
        }
        #endregion

        #region [RefreshEventDelegates]
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
        #endregion 

        #endregion

        #region Properties

        public IList<ActiveFlow.ActiveFlow> ActiveFlows
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

        #endregion
    }
}