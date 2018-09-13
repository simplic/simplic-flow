using Newtonsoft.Json;
using Simplic.Flow.Editor.Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    public class WorkflowEditorViewModel : Simplic.UI.MVC.ViewModelBase, IObservableGraphSource
    {
        #region Private Members
        private Guid id;
        private ObservableCollection<NodeConnectionViewModel> connections;
        private Simplic.Flow.Configuration.FlowConfiguration flowConfiguration;
        #endregion

        #region Constructor
        public WorkflowEditorViewModel(IList<Definition.NodeDefinition> nodeDefinitions,
            Simplic.Flow.Configuration.FlowConfiguration flowConfiguration)
        {
            connections = new ObservableCollection<NodeConnectionViewModel>();
            Nodes = new ObservableCollection<NodeViewModel>();
            NodeDefinitions = nodeDefinitions;

            Nodes.CollectionChanged += (s, e) =>
            {
                IsDirty = true;
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems.OfType<Simplic.UI.MVC.ViewModelBase>())
                    {
                        item.Parent = this;
                    }
                }
            };

            if (flowConfiguration == null)
                this.flowConfiguration = new Configuration.FlowConfiguration()
                {
                    Id = Guid.NewGuid(),
                    Name = "New Workflow"
                };
            else
                this.flowConfiguration = flowConfiguration;

            FillConfiguration();
        }
        #endregion

        private void FillConfiguration()
        {
            if (!flowConfiguration.Nodes.Any())
                return;

            // create node view models
            foreach (var node in flowConfiguration.Nodes)
            {
                NodeViewModel nodeViewModel = null;
                var nodeDefinition = NodeDefinitions.Where(x => x.Name == node.ClassName).FirstOrDefault();

                if (node.NodeType == "ActionNode")
                {
                    nodeViewModel = new ActionNodeViewModel(nodeDefinition, node);
                }
                else if (node.NodeType == "EventNode")
                {
                    nodeViewModel = new EventNodeViewModel(nodeDefinition, node);
                }

                Nodes.Add(nodeViewModel);
            }

            // create flow links
            foreach (var flow in flowConfiguration.Links)
            {
                var sourceNodeViewModel = Nodes.Where(x => x.Id == flow.From.NodeId).FirstOrDefault();
                var sourceConnectorViewModel = sourceNodeViewModel.FlowPins.Where(x => x.Name == flow.From.PinName).FirstOrDefault();

                var targetNodeViewModel = Nodes.Where(x => x.Id == flow.To.NodeId).FirstOrDefault();
                var targetConnectorViewModel = targetNodeViewModel.FlowPins.Where(x => x.Name == flow.To.PinName).FirstOrDefault();

                var connectionViewModel = new NodeConnectionViewModel(sourceNodeViewModel, targetNodeViewModel, sourceConnectorViewModel, targetConnectorViewModel);
                Connections.Add(connectionViewModel);
            }

            // create data pins
            foreach (var pin in flowConfiguration.Pins)
            {
                var sourceNodeViewModel = Nodes.Where(x => x.Id == pin.From.NodeId).FirstOrDefault();
                var sourceConnectorViewModel = sourceNodeViewModel.DataPins.Where(x => x.Name == pin.From.PinName).FirstOrDefault();

                var targetNodeViewModel = Nodes.Where(x => x.Id == pin.To.NodeId).FirstOrDefault();
                var targetConnectorViewModel = targetNodeViewModel.DataPins.Where(x => x.Name == pin.To.PinName).FirstOrDefault();

                var connectionViewModel = new NodeConnectionViewModel(sourceNodeViewModel, targetNodeViewModel, sourceConnectorViewModel, targetConnectorViewModel);
                Connections.Add(connectionViewModel);
            }
        }

        #region Public Properties
        public Guid Id
        {
            get { return id; }
            set { id = value; IsDirty = true; RaisePropertyChanged(nameof(Id)); }
        }

        public ObservableCollection<NodeViewModel> Nodes { get; }

        public IList<Definition.NodeDefinition> NodeDefinitions { get; set; }

        IEnumerable IGraphSource.Items { get { return Nodes; } }

        public ObservableCollection<NodeConnectionViewModel> Connections
        {
            get { return connections; }
            set { connections = value; IsDirty = true; RaisePropertyChanged(nameof(Connections)); }
        }

        IEnumerable<ILink> IGraphSource.Links { get { return connections; } }

        public ConnectorViewModel SourceConnector { get; set; }

        public ConnectorViewModel TargetConnector { get; set; }

        #endregion

        #region Public Methods        

        void IObservableGraphSource.AddLink(ILink link)
        {
            IsDirty = true;

            var connection = link as NodeConnectionViewModel;

            if (TargetConnector != null)
                connection.TargetConnectorViewModel = TargetConnector;

            connections.Add(connection);
        }

        void IObservableGraphSource.AddNode(object node)
        {
            Nodes.Add(node as NodeViewModel);
            IsDirty = true;
        }

        ILink IObservableGraphSource.CreateLink(object source, object target)
        {
            IsDirty = true;

            return new NodeConnectionViewModel(source as NodeViewModel, target as NodeViewModel, SourceConnector, null);
        }

        object IObservableGraphSource.CreateNode(IShape shape)
        {
            IsDirty = true;
            if (shape is ActionNodeShape)
            {
                var actionNodeShape = shape as ActionNodeShape;

                var def = NodeDefinitions.Where(x => x.Name == actionNodeShape.Name).FirstOrDefault();

                var nodeConfig = new Configuration.NodeConfiguration
                {
                    Id = Guid.NewGuid(),
                    ClassName = def.Name,
                    NodeType = def is ActionNodeDefinition ? "ActionNode" : "EventNode"
                };

                this.flowConfiguration.Nodes.Add(nodeConfig);

                var actionNodeViewModel = new ActionNodeViewModel(def, nodeConfig);

                actionNodeShape.DataContext = actionNodeViewModel;
                actionNodeShape.CreateConnectors();

                return actionNodeViewModel;
            }

            return null;
        }

        bool IObservableGraphSource.RemoveLink(ILink link)
        {
            IsDirty = true;

            if (connections.Contains(link))
            {
                connections.Remove(link as NodeConnectionViewModel);
                return true;
            }
            else
            {
                return false;
            }
        }

        bool IObservableGraphSource.RemoveNode(object node)
        {
            IsDirty = true;

            if (Nodes.Contains(node))
            {
                var nodeVm = node as NodeViewModel;
                Nodes.Remove(nodeVm);

                var connections = Connections.Where(x =>
                       !Nodes.Any(y => y.Id == x.SourceViewModel?.Id)
                    || !Nodes.Any(y => y.Id == x.TargetViewModel?.Id)).ToList();

                foreach (var connection in connections)
                    Connections.Remove(connection);

                return true;
            }
            else
            {
                return false;
            }
        }

        //public void SerializeNode(object model, SerializationInfo info)
        //{

        //}

        //public void SerializeLink(ILink link, SerializationInfo info)
        //{

        //}

        //public object DeserializeNode(IShape shape, SerializationInfo info)
        //{
        //    var obj = base.Serialize();

        //    obj["Name"] = Name;

        //    return obj;
        //}

        //public ILink DeserializeLink(IConnection connection, SerializationInfo info)
        //{
        //    return null;
        //}


        public string Serialize()
        {
            // Create links
            flowConfiguration.Links.Clear();
            flowConfiguration.Links = connections.Where(x => x.FlowLink != null).Select(x => x.FlowLink).ToList();
            flowConfiguration.Pins = connections.Where(x => x.DataLink != null).Select(x => x.DataLink).ToList();

            var json = JsonConvert.SerializeObject(flowConfiguration);

            return json;
        }
        #endregion
    }
}