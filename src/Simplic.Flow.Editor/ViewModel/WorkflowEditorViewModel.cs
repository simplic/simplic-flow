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

            this.flowConfiguration = flowConfiguration;
            FillConfiguration();
        }
        #endregion

        private void FillConfiguration()
        {
            foreach (var item in flowConfiguration.Nodes)
            {
              
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
                var actionNodeViewModel = new ActionNodeViewModel(def, null) {
                    Id = Guid.NewGuid()
                };

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
                var connection = Connections.Where(x => x.SourceViewModel?.Id == nodeVm.Id || 
                    x.TargetViewModel?.Id == nodeVm.Id).FirstOrDefault();  
                
                if (connection != null)
                    Connections.Remove(connection);
                
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion
    }
}