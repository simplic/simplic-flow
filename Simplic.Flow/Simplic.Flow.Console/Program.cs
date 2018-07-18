using Simplic.Flow.Configuration;
using Simplic.Flow.Configuration.Data.Memory;
using Simplic.Flow.Configuration.Service;
using Simplic.Flow.Event;
using Simplic.Flow.Service;
using Simplic.FlowInstance;
using Simplic.FlowInstance.Data.DB;
using Simplic.Sql;
using System;
using Unity;

namespace Simplic.Flow.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateConfiguration();

            // ==================================================================
            #region Manuel configuration
            //var flow = new Flow();
            //flow.Id = Guid.NewGuid();

            //var evt = flow.CreateNode<OnDocumentScannedNode>();
            //var evt2 = flow.CreateNode<OnDocumentScannedNode>();
            //evt.IsStartEvent = true;

            //var seq = flow.CreateNode<SequenceNode>();
            //var print1 = flow.CreateNode<ConsoleWriteLineNode>();
            //var print2 = flow.CreateNode<ConsoleWriteLineNode>();
            //var print3 = flow.CreateNode<ConsoleWriteLineNode>();
            //var array = flow.CreateNode<CreateStringArraySampleNode>();
            //var fenode = flow.CreateNode<ForeachNode>();
            //var print4 = flow.CreateNode<ConsoleWriteLineNode>();

            //// Flow direction
            //evt.FlowOut = seq;
            //seq.FlowOutNodes.Add(print1);
            //seq.FlowOutNodes.Add(print2);

            //print2.FlowOut = evt2;
            //evt2.FlowOut = print3;

            //// Add foreach part
            //print1.FlowOut = array;
            //array.FlowOut = fenode;
            //fenode.EachItemFlowOut = print4;

            //// Data flow
            //print1.ToPrint = evt.DocumentOut;
            //print2.ToPrint = evt.DocumentOut;
            //print3.ToPrint = evt.DocumentOut;

            //fenode.InList = array.StringArrayOut;
            //print4.ToPrint = fenode.Output; 
            #endregion
            // ==================================================================

            // ==================================================================

            IUnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType<ISqlService, Sql.SqlAnywhere.Service.SqlService>();
            unityContainer.RegisterType<IFlowInstanceRepository, FlowInstanceRepository>();
            unityContainer.RegisterType<IFlowConfigurationRepository, FlowConfigurationMemoryRepository>();
            unityContainer.RegisterType<IFlowConfigurationService, FlowConfigurationService>();            

            var engine = new FlowService(unityContainer);

            engine.RefreshEventDelegates();

            engine.EnqueueEvent(new FlowEventArgs { EventName = nameof(OnDocumentScannedNode), ObjectId = Guid.Empty });

            engine.Run();

            engine.EnqueueEvent(new FlowEventArgs { EventName = nameof(OnDocumentScannedNode), ObjectId = Guid.Empty });
            engine.Run();

            engine.EnqueueEvent(new FlowEventArgs { EventName = nameof(OnDocumentScannedNode), ObjectId = Guid.Empty });
            engine.Run();
            // ==================================================================

            System.Console.ReadLine();
        }

        private static void CreateConfiguration()
        {
            return;
            IFlowConfigurationRepository repo = new FlowConfigurationMemoryRepository();
            var service = new FlowConfigurationService(repo);

            var flowConfiguration = new FlowConfiguration
            {
                Id = Guid.NewGuid(),
                Name = "Document scan and process2"                
            };

            var onDocumentScannedNode = new NodeConfiguration
            {
                Id = Guid.NewGuid(),
                NodeType = "EventNode",
                ClassName = "OnDocumentScannedNode",
                IsStartEvent = true
            };

            flowConfiguration.Nodes.Add(onDocumentScannedNode);

            var sequenceNode = new NodeConfiguration
            {
                Id = Guid.NewGuid(),
                NodeType = "ActionNode",
                ClassName = "SequenceNode"
            };
            flowConfiguration.Nodes.Add(sequenceNode);

            var consoleWriteLineNode = new NodeConfiguration
            {
                Id = Guid.NewGuid(),
                NodeType = "ActionNode",
                ClassName = "ConsoleWriteLineNode"
            };
            flowConfiguration.Nodes.Add(consoleWriteLineNode);

            var consoleWriteLineNode2 = new NodeConfiguration
            {
                Id = Guid.NewGuid(),
                NodeType = "ActionNode",
                ClassName = "ConsoleWriteLineNode"
            };
            flowConfiguration.Nodes.Add(consoleWriteLineNode2);


            var onDocumentScannedNode2 = new NodeConfiguration
            {
                Id = Guid.NewGuid(),
                NodeType = "EventNode",
                ClassName = "OnDocumentScannedNode",
                IsStartEvent = false
            };
            flowConfiguration.Nodes.Add(onDocumentScannedNode2);

            var consoleWriteLineNode3 = new NodeConfiguration
            {
                Id = Guid.NewGuid(),
                NodeType = "ActionNode",
                ClassName = "ConsoleWriteLineNode"
            };
            flowConfiguration.Nodes.Add(consoleWriteLineNode3);


            flowConfiguration.Links = new System.Collections.Generic.List<LinkConfiguration> {
                new LinkConfiguration
                {
                    From = new Link
                    {
                        NodeId = onDocumentScannedNode.Id,
                        PinName = "FlowOut"
                    },
                    To = new Link
                    {
                        NodeId = sequenceNode.Id
                    }
                },
                new LinkConfiguration
                {
                    From = new Link
                    {
                        NodeId = sequenceNode.Id,
                        PinName = "FlowOutNodes"
                    },
                    To = new Link
                    {
                        NodeId = consoleWriteLineNode.Id
                    }
                },
                new LinkConfiguration
                {
                    From = new Link
                    {
                        NodeId = sequenceNode.Id,
                        PinName = "FlowOutNodes"
                    },
                    To = new Link
                    {
                        NodeId = consoleWriteLineNode2.Id
                    }
                },                
                new LinkConfiguration
                {
                    From = new Link
                    {
                        NodeId = consoleWriteLineNode2.Id,
                        PinName = "FlowOut"
                    },
                    To = new Link
                    {
                        NodeId = onDocumentScannedNode2.Id
                    }
                },
                new LinkConfiguration
                {
                    From = new Link
                    {
                        NodeId = onDocumentScannedNode2.Id,
                        PinName = "FlowOut"
                    },
                    To = new Link
                    {
                        NodeId = consoleWriteLineNode3.Id                        
                    }
                }
            };

            flowConfiguration.Pins = new System.Collections.Generic.List<PinConfiguration>
            {
                new PinConfiguration
                {
                    From = new Link
                    {
                        NodeId = onDocumentScannedNode.Id,
                        PinName = "DocumentOut"
                    },
                    To = new Link
                    {
                        NodeId = consoleWriteLineNode.Id,
                        PinName = "ToPrint"
                    }
                },
                new PinConfiguration
                {
                    From = new Link
                    {
                        NodeId = onDocumentScannedNode.Id,
                        PinName = "DocumentOut"
                    },
                    To = new Link
                    {
                        NodeId = consoleWriteLineNode2.Id,
                        PinName = "ToPrint"
                    }
                },
                new PinConfiguration
                {
                    From = new Link
                    {
                        NodeId = onDocumentScannedNode2.Id,
                        PinName = "DocumentOut"
                    }
                    ,
                    To = new Link
                    {
                        NodeId = consoleWriteLineNode3.Id,
                        PinName = "ToPrint"
                    }
                }
            };
            
            var jsonStr = service.Save(flowConfiguration);
        }
    }
}
