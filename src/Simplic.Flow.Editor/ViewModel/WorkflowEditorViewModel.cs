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
        private object selectedItem;
        private ObservableCollection<NodeConnectionViewModel> connections;
        #endregion

        #region Constructor
        public WorkflowEditorViewModel()
        {
            connections = new ObservableCollection<NodeConnectionViewModel>();
            Nodes = new ObservableCollection<NodeViewModel>();
            NodeDefinitions = new List<Definition.NodeDefinition>();

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
        }
        #endregion

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

        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                // We have to reset the selection first
                selectedItem = null;
                RaisePropertyChanged(nameof(SelectedItem));

                // Refresh selection
                selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }
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
            this.SelectedItem = node as NodeViewModel;
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

                var nodeViewModel = new ActionNodeViewModel(def, null);

                var actionShape = (shape as ActionNodeShape);
                actionShape.DataContext = nodeViewModel;

                actionShape.CreateConnectors();
                actionShape.LoadConnectorText();
                //return new NodeViewModel() { DisplayName = "<<Neu>>" };
                return nodeViewModel;
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
                Nodes.Remove(node as NodeViewModel);
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