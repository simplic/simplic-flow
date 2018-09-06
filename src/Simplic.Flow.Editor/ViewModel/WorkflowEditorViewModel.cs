using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    public class WorkflowEditorViewModel : Simplic.UI.MVC.ViewModelBase, IObservableGraphSource
    {
        private Guid id;
        private object selectedItem;
        private ObservableCollection<NodeConnectionViewModel> connections;

        public WorkflowEditorViewModel()
        {
            connections = new ObservableCollection<NodeConnectionViewModel>();
            Nodes = new ObservableCollection<NodeViewModel>();

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

        public ObservableCollection<NodeViewModel> Nodes { get; }

        public Guid Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                IsDirty = true;
                RaisePropertyChanged(nameof(Id));
            }
        }

        IEnumerable IGraphSource.Items
        {
            get
            {
                return Nodes;
            }
        }

        public ObservableCollection<NodeConnectionViewModel> Connections
        {
            get
            {
                return connections;
            }

            set
            {
                connections = value;
                IsDirty = true;
                RaisePropertyChanged(nameof(Connections));
            }
        }

        IEnumerable<ILink> IGraphSource.Links
        {
            get
            {
                return connections;
            }
        }

        public ConnectorViewModel SourceConnector { get; set; }
        public ConnectorViewModel TargetConnector { get; set; }
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
            if (shape is Telerik.Windows.Controls.RadDiagramShape)
            {
                var inFlowPins = new List<FlowPinDefinition>();
                inFlowPins.Add(new FlowPinDefinition { Id = Guid.NewGuid(), Name = "FlowIn", PinDirection = PinDirectionDefinition.In });

                var inDataPins = new List<DataPinDefinition>();
                inDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "EmailAdressIn", Type = typeof(string), PinDirection = PinDirectionDefinition.In });
                inDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "SubjectIn", Type = typeof(string), PinDirection = PinDirectionDefinition.In });

                var outFlowPins = new List<FlowPinDefinition>();
                outFlowPins.Add(new FlowPinDefinition { Id = Guid.NewGuid(), Name = "FlowOut", PinDirection = PinDirectionDefinition.Out });

                var outDataPins = new List<DataPinDefinition>();
                outDataPins.Add(new DataPinDefinition { Id = Guid.NewGuid(), Name = "ResultOut", Type = typeof(bool), PinDirection = PinDirectionDefinition.Out });

                var emailNodeDefinition2 = new ActionNodeDefinition
                {                    
                    Name = "Send Fax",                    
                    InFlowPins = inFlowPins,
                    InDataPins = inDataPins,
                    OutFlowPins = outFlowPins,
                    OutDataPins = outDataPins
                };

                return new ActionNodeViewModel(emailNodeDefinition2, null);
                //return new NodeViewModel() { DisplayName = "<<Neu>>" };
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

        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }

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
    }
}
