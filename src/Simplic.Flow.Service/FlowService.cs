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
using System.Threading;
using Unity;

namespace Simplic.Flow.Service
{
    public class ThreadInfo
    {
        public FlowInstance FlowInstance { get; set; }
        public ActiveFlow.ActiveFlow ActiveFlow { get; set; }
        public EventCall EventCall { get; set; }        
        public bool IsStartEvent { get; set; }        
    }

    public class FlowService : IFlowService
    {
        #region Events
        private event EventHandler OnStarted;
        private event EventHandler OnCompleted;
        private object eventLock = new Object();

        event EventHandler IFlowService.OnStarted
        {
            add
            {
                lock (eventLock)
                {
                    OnStarted += value;
                }
            }
            remove
            {
                lock (eventLock)
                {
                    OnStarted -= value;
                }
            }
        }

        event EventHandler IFlowService.OnCompleted
        {
            add
            {
                lock (eventLock)
                {
                    OnCompleted += value;
                }
            }
            remove
            {
                lock (eventLock)
                {
                    OnCompleted -= value;
                }
            }
        } 
        #endregion

        #region Private Members
        private IList<Flow> flows;
        private IList<ActiveFlow.ActiveFlow> activeFlows;
        private IList<FlowConfiguration> flowConfigurations;
        private IList<ActiveFlow.ActiveFlow> finishedInstances = new List<ActiveFlow.ActiveFlow>();
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
            if (flowConfigurations.Count > 0)
                flowLogService.Info($"# {flowConfigurations.Count} Flow Configuration were found: {string.Join(", ", flowConfigurations.Select(x => $"\"{x.Name}\""))}");
            else
            {
                flowLogService.Info("No flow configurations were found!");
                return;
            }

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
            if (flowInstances.Count > 0)
                flowLogService.Info($"> Creating active flows from {string.Join(",", flowInstances.Select(x => $"\"{x.Flow.Name}\""))}");

            foreach (var flowInstance in flowInstances)
            {
                CreateActiveFlow(flowInstance);
            }

            if (activeFlows.Count > 0)
                flowLogService.Info($"> {activeFlows.Count} active flows were created.");
        }
        #endregion

        #region [CreateActiveFlow]
        /// <summary>
        /// CreateActiveFlow
        /// </summary>
        /// <param name="flowInstance"></param>
        private void CreateActiveFlow(FlowInstance flowInstance)
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
        #endregion

        #region [CreateFlowsFromConfiguration]
        /// <summary>
        /// Creates a list <see cref="Flow"/> objects from <see cref="FlowConfiguration"/> objects
        /// </summary>
        /// <returns>A list <see cref="Flow"/> objects</returns>
        private IList<Flow> CreateFlowsFromConfiguration()
        {
            flowLogService.Info($"> Creating flows from configurations...");

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

            flowLogService.Info($"> {flowConfigurations.Count} flows created.");

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

            if (unhandledEvents.Count() > 0)
                flowLogService.Info($"> {unhandledEvents.Count()} unhandled events were found.");

            foreach (var flowEventQueue in unhandledEvents)
            {
                var eventArgs = JsonConvert.DeserializeObject<FlowEventArgs>(
                    Encoding.UTF8.GetString(flowEventQueue.Args),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                // if there are no event args create an empty object
                if (eventArgs == null)
                    eventArgs = new FlowEventArgs();


                eventArgs.EventName = flowEventQueue.EventName;
                eventArgs.QueueId = flowEventQueue.Id;
                eventArgs.UserId = flowEventQueue.CreateUserId;

                EnqueueEvent(eventArgs);
            }

            if (eventQueue.Count() > 0)
                flowLogService.Info($"> {eventQueue.Count()} events were created.");
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

        private void ProcessEvent(object param)
        {
            var threadInfo = param as ThreadInfo;

            var runtime = new FlowRuntimeService(flowLogService, threadInfo.EventCall.Args);

            if (threadInfo.IsStartEvent)
            {
                try
                {
                    runtime.Run(threadInfo.FlowInstance, threadInfo.EventCall);
                    if (!threadInfo.FlowInstance.IsAlive)
                    {                        
                        activeFlows.Remove(threadInfo.ActiveFlow);
                        flowLogService.Info($"Flow instance {threadInfo.ActiveFlow.FlowInstanceId} is not alive anymore.");
                    }

                    // Save active flow instance after run
                    flowInstanceService.Save(threadInfo.FlowInstance);
                }
                catch (Exception ex)
                {
                    // Cancel active flow
                    threadInfo.FlowInstance.IsFailed = true;

                    activeFlows.Remove(threadInfo.ActiveFlow);

                    // log the exception
                    flowLogService.Error($"Continue flow instance failed.", ex, threadInfo.ActiveFlow.FlowInstanceId, threadInfo.EventCall.QueueId);

                    flowInstanceService.Save(threadInfo.FlowInstance);
                    throw;
                }                
            }
            else
            {                                
                try
                {
                    runtime.Run(threadInfo.FlowInstance, threadInfo.EventCall);
                    CreateActiveFlow(threadInfo.FlowInstance);                    

                    // Save active flow instance after run
                    flowInstanceService.Save(threadInfo.FlowInstance);
                }
                catch (Exception ex)
                {
                    threadInfo.FlowInstance.IsFailed = true;
                    flowInstanceService.Save(threadInfo.FlowInstance);

                    flowLogService.Error($"- NewFlowInstace could not be run", ex, threadInfo.FlowInstance.Id, threadInfo.EventCall.QueueId);
                }                
            }            
        }

        #endregion

        #region Public Methods

        #region [Run]
        /// <summary>
        /// Run a single cycle
        /// </summary>
        public void Run()
        {
            // Raise event before the process has begun.
            OnStarted?.Invoke(this, EventArgs.Empty);

            flowLogService.Info($"> Running at {DateTime.Now}");
            try
            {
                // load event queue from db            
                LoadEventQueue();                

                if (eventQueue.Count() == 0)
                {
                    flowLogService.Info($"- Event queue is empty. Nothing to do.");
                    return;
                }
                                    
                //var newFlowInstances = new List<FlowInstance>();

                while (eventQueue.Count > 0)
                {
                    // pop event entries from queue first
                    var queueEntry = eventQueue.PopFirst();

                    flowLogService.Info($"> Processing {queueEntry.Args.EventName}...");

                    if (queueEntry.Delegate.IsStartEvent)
                    {
                        flowLogService.Info($"- Create new flow instance {queueEntry.Args.EventName} : {queueEntry.Delegate.FlowId}");

                        var newFlowInstance = new FlowInstance
                        {
                            Flow = flows.FirstOrDefault(x => x.Id == queueEntry.Delegate.FlowId),
                            Id = Guid.NewGuid()
                        };

                        ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessEvent), new ThreadInfo
                        {
                            FlowInstance = newFlowInstance,
                            EventCall = queueEntry
                        });
                    }
                    else
                    {
                        // Notify ALL instances, which MIGHT BE continued
                        foreach (var activeFlow in activeFlows.Where(x => x.FlowId == queueEntry.Delegate.FlowId))
                        {
                            flowLogService.Info($"Continuing flow instance: {activeFlow.FlowInstanceId}");

                            // Get from database
                            var flowInstance = flowInstanceService.GetById(activeFlow.FlowInstanceId);
                            flowInstance.Flow = flows.FirstOrDefault(x => x.Id == flowInstance.FlowId);

                            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessEvent), new ThreadInfo
                            {
                                IsStartEvent = false,
                                FlowInstance = flowInstance,
                                EventCall = queueEntry,
                                ActiveFlow = activeFlow
                            });

                        }
                    }
                    
                    // change the event status to handled and save to the database
                    SetEventQueueHandled(queueEntry.QueueId);                    
                }

                //CreateActiveFlowsFrom(newFlowInstances);
            }
            catch (Exception ex)
            {
                flowLogService.Error($"- FlowService.Run could not be run", ex);

                // Raise event after the process has begun.
                OnCompleted?.Invoke(this, EventArgs.Empty);
                throw;
            }

            // Raise event after the process has begun.
            OnCompleted?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region [EnqueueEvent]
        /// <summary>
        /// Enqueue event
        /// </summary>
        /// <param name="args"></param>
        public void EnqueueEvent(FlowEventArgs args)
        {
            flowLogService.Info($"- Enqueue event: {args.EventName} / object id {args.ObjectId ?? "<unset>"}");

            bool isEnqueued = false;

            // Find workflow
            var delegates = eventDelegates.FirstOrDefault(x => x.Key == args.EventName).Value;
            if (delegates != null && delegates.Count > 0)
            {
                foreach (var del in delegates)
                {
                    flowLogService.Info($"- Create event call: {args.EventName} / Flow: {del.FlowId} / Is start event {del.IsStartEvent}");

                    eventQueue.PushBack(new EventCall { QueueId = args.QueueId, Args = args, Delegate = del });
                    isEnqueued = true;
                }
            }

            if (!isEnqueued)
            {
                flowLogService.Info($"- No delegate was found for {args.EventName}, changing status to handled.");
                flowEventQueueService.SetHandled(args.QueueId, true);
            }
        }
        #endregion

        #region [RefreshEventDelegates]
        /// <summary>
        /// Cache event delegates
        /// </summary>
        public void RefreshEventDelegates()
        {
            flowLogService.Info("> Creating event delegates...");

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

            flowLogService.Info($"> {eventDelegates.Count} event delegates were created. \"{string.Join(", ", eventDelegates.Select(x => x.Key))}\"");
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