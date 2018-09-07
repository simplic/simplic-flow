using Newtonsoft.Json;
using Simplic.Collections.Generic;
using Simplic.Flow.Configuration;
using Simplic.Flow.Event;
using Simplic.Flow.EventQueue;
using Simplic.Flow.Log;
using Simplic.Flow.Node;
using Simplic.Flow.Node.IO;
using Simplic.FlowInstance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            this.unityContainer = unityContainer;

            flows = new List<Flow>();
            activeFlows = new List<Simplic.ActiveFlow.ActiveFlow>();
            eventQueue = new Dequeue<EventCall>();
            eventDelegates = new Dictionary<string, IList<EventDelegate>>();

            this.flowInstanceService = unityContainer.Resolve<IFlowInstanceService>();
            this.flowConfigurationService = unityContainer.Resolve<IFlowConfigurationService>();
            this.flowEventQueueService = unityContainer.Resolve<IFlowEventQueueService>();
            this.flowEventService = unityContainer.Resolve<IFlowEventService>();
            this.flowLogService = unityContainer.Resolve<IFlowLogService>();

            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ConsoleWriteLineNode>>("ConsoleWriteLineNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<ForeachNode>>("ForeachNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<SequenceNode>>("SequenceNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<StartWithConditionNode>>("StartWithConditionNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<DeleteFileNode>>("DeleteFileNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<GetDirectoryContentNode>>("GetDirectoryContentNode");
            unityContainer.RegisterType<INodeResolver, GenericNodeResolver<OnCheckDirectoryContentNode>>("OnCheckDirectoryContentNode");

            flowConfigurations = flowConfigurationService.GetAll().ToList();

            foreach (var configuration in flowConfigurations)
                flowLogService.Info($"Load flow configuration: {configuration.Name}");


            flows = CreateFlowsFromConfiguration();
            CreateActiveFlowsFrom(flowInstanceService.GetAllAlive().ToList());

            RefreshEventDelegates();
        }
        #endregion

        #region Private Methods

        #region [CreateActiveFlowsFrom]
        /// <summary>
        /// Creates <see cref="ActiveFlow.ActiveFlow"/> objects from a list of <see cref="FlowInstance.FlowInstance"/> objects
        /// </summary>
        /// <param name="flowInstances">Objects to create from</param>
        private void CreateActiveFlowsFrom(List<FlowInstance> flowInstances)
        {
            flowLogService.Info($"Running CreateActiveFlowsFrom with {string.Join(",", flowInstances)}");
            foreach (var flowInstance in flowInstances)
            {
                if (flowInstance.Flow == null)
                    flowInstance.Flow = flows.FirstOrDefault(x => x.Id == flowInstance.FlowId);

                if (flowInstance.IsAlive)
                {
                    var activeFlow = new ActiveFlow.ActiveFlow
                    {
                        FlowInstanceId = flowInstance.Id,
                        CurrentEventNodes = flowInstance.CurrentNodes,
                        FlowId = flowInstance?.Flow?.Id ?? flowInstance.FlowId
                    };
                    
                    activeFlows.Add(activeFlow);
                }
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
            flowLogService.Info($"Running CreateFlowsFromConfiguration with {string.Join(",", flowConfigurations)}");
            var list = new List<Flow>();
            foreach (var flowConfiguration in flowConfigurations)
            {
                var nodes = new List<BaseNode>();
                foreach (var node in flowConfiguration.Nodes)
                {
                    var resolver = unityContainer.Resolve<INodeResolver>($"{node.ClassName}");
                    var newNode = resolver.Create(node.Id, node.IsStartEvent);

                    // Create data pin instances inside node
                    newNode.CreateDataPins();

                    nodes.Add(newNode);
                    // Set default values
                    if (node?.Pins != null)
                    {
                        foreach (var pin in node.Pins)
                        {
                            var pinProperty = newNode.GetType().GetProperty(pin.Name,
                                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                            var defaultValueProperty = typeof(DataPin).GetProperty("DefaultValue",
                                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                            if (pin.DefaultValue != null)
                            {
                                var pinInstance = pinProperty.GetValue(newNode);

                                defaultValueProperty.SetValue(pinInstance, pin.DefaultValue);
                            }
                        }
                    }

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
            var unhandledEvents = flowEventQueueService.GetAllUnhandled();

            flowLogService.Info($"Unhandled events found: {unhandledEvents.Count()}");

            foreach (var flowEventQueue in unhandledEvents)
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
            flowLogService.Info($"Running SetEventQueueHandled with {eventQueueId}");
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
                var newFlowInstances = new List<FlowInstance>();

                while (eventQueue.Count > 0)
                {
                    var queueEntry = eventQueue.PopFirst();

                    flowLogService.Info($"Processing {queueEntry}");

                    if (!queueEntry.Delegate.IsStartEvent)
                    {
                        var finishedInstances = new List<ActiveFlow.ActiveFlow>();

                        // Notify ALL instances, which MIGHT BE continued
                        foreach (var activeFlow in activeFlows.Where(x => x.FlowId == queueEntry.Delegate.FlowId))
                        {
                            flowLogService.Info($"Continuing flow instance: {activeFlow.FlowInstanceId}");
                            System.Console.WriteLine("---- CONTINUE FLOW ----");

                            // Get from database
                            var flowInstance = flowInstanceService.GetById(activeFlow.FlowInstanceId);
                            flowInstance.Flow = flows.FirstOrDefault(x => x.Id == flowInstance.FlowId);

                            try
                            {
                                var runtime = new FlowRuntimeService(flowLogService, queueEntry.Args);
                                runtime.Run(flowInstance, queueEntry);

                                if (!flowInstance.IsAlive)
                                {
                                    finishedInstances.Add(activeFlow);
                                    flowLogService.Info($"Flow instance {activeFlow.FlowInstanceId} is not alive anymore.");
                                }


                                flowInstanceService.Save(flowInstance);
                            }
                            catch (Exception ex)
                            {
                                // Cancel active flow
                                flowInstance.IsFailed = true;

                                finishedInstances.Add(activeFlow);

                                // log the exception
                                flowLogService.Error($"Continue flow instance failed.", ex, activeFlow.FlowInstanceId, queueEntry.QueueId);

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
                        flowLogService.Info($"Create new flow instance {queueEntry.Args.EventName} : {queueEntry.Delegate.FlowId}");

                        var runtime = new FlowRuntimeService(flowLogService, queueEntry.Args);
                        var newFlowInstance = new FlowInstance
                        {
                            Flow = flows.FirstOrDefault(x => x.Id == queueEntry.Delegate.FlowId),
                            Id = Guid.NewGuid()
                        };

                        try
                        {
                            runtime.Run(newFlowInstance, queueEntry);
                            newFlowInstances.Add(newFlowInstance);

                            flowLogService.Info("Flow instance successfull", newFlowInstance?.Id);

                            // Save active flow instance after run
                            flowInstanceService.Save(newFlowInstance);
                        }
                        catch (Exception ex)
                        {
                            newFlowInstance.IsFailed = true;
                            flowInstanceService.Save(newFlowInstance);

                            flowLogService.Error($"NewFlowInstace could not be run", ex, newFlowInstance.Id, queueEntry.QueueId);
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
                flowLogService.Error($"FlowService.Run could not be run", ex);
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

            bool isEnqueued = false;

            // Find workflow
            var delegates = eventDelegates.FirstOrDefault(x => x.Key == args.EventName).Value;
            if (delegates != null && delegates.Count > 0)
            {
                foreach (var del in delegates)
                {
                    flowLogService.Info($"Create event call: {args.EventName} / Flow: {del.FlowId} / Is start event {del.IsStartEvent}");

                    eventQueue.PushBack(new EventCall { QueueId = args.QueueId, Args = args, Delegate = del });
                    isEnqueued = true;
                }
            }

            if (!isEnqueued)
            {
                flowLogService.Info($"No delegate was found for {args.EventName}, changing status to handled.");
                flowEventQueueService.SetHandled(args.QueueId, true);
            }
        }
        #endregion

        #region [RefreshEventDelegates]
        /// <summary>
        /// Cash event delegates
        /// </summary>
        public void RefreshEventDelegates()
        {
            flowLogService.Info("Create event delegate list");

            eventDelegates = new Dictionary<string, IList<EventDelegate>>();
            foreach (var flow in flows)
            {
                foreach (var eventNode in flow.Nodes.OfType<EventNode>())
                {
                    flowLogService.Info($"> Add {flow.Name}/{eventNode.Name}");

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

            flowLogService.Info($"Created delegates: {eventDelegates.Count}");
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