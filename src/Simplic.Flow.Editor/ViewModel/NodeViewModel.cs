using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Simplic.Flow.Editor
{
    public abstract class NodeViewModel : Simplic.UI.MVC.ViewModelBase
    {
        private string displayName;
        private bool isActive;        
        private Point position;
        private double width;
        private double height;

        private NodeDefinition nodeDefinition;
        private NodeConfiguration nodeConfiguration;

        public NodeViewModel(NodeDefinition nodeDefinition, NodeConfiguration nodeConfiguration)
        {
            this.nodeDefinition = nodeDefinition;
            this.nodeConfiguration = nodeConfiguration;

            FlowPins = new ObservableCollection<FlowConnectorViewModel>();
            DataPins = new ObservableCollection<DataConnectorViewModel>();

            FillPins();
        }

        private void FillPins()
        {
            foreach (var pin in nodeDefinition.InFlowPins)
            {
                FlowPins.Add(new FlowConnectorViewModel(pin)
                {
                    Parent = this,
                    PinDirection = PinDirectionDefinition.In
                });
            }

            foreach (var pin in nodeDefinition.InDataPins)
            {
                DataPins.Add(new DataConnectorViewModel(pin)
                {
                    Parent = this,
                    PinDirection = PinDirectionDefinition.In
                });
            }

            foreach (var pin in nodeDefinition.OutFlowPins)
            {
                FlowPins.Add(new FlowConnectorViewModel(pin)
                {
                    Parent = this,
                    PinDirection = PinDirectionDefinition.Out
                });
            }

            foreach (var pin in nodeDefinition.OutDataPins)
            {
                DataPins.Add(new DataConnectorViewModel(pin)
                {
                    Parent = this,
                    PinDirection = PinDirectionDefinition.Out
                });
            }
        }

        public ObservableCollection<FlowConnectorViewModel> FlowPins { get; }
        public ObservableCollection<DataConnectorViewModel> DataPins { get; }

        public Guid Id
        {
            get
            {
                return nodeConfiguration != null ? nodeConfiguration.Id : Guid.Empty;
            }

            set
            {
                IsDirty = true;
                nodeConfiguration.Id = value;

                RaisePropertyChanged(nameof(Id));
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                IsDirty = true;
                isActive = value;
                RaisePropertyChanged(nameof(IsActive));
            }
        }

        public string DisplayName
        {
            get
            {
                return nodeDefinition.Name;
            }            
        }

        public Point Position
        {
            get { return position; }
            set
            {
                position = value;
                IsDirty = true;                
                RaisePropertyChanged(nameof(Position));
            }
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                IsDirty = true;
                RaisePropertyChanged(nameof(Width));
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                IsDirty = true;
                RaisePropertyChanged(nameof(Height));
            }
        }       
    }
}
