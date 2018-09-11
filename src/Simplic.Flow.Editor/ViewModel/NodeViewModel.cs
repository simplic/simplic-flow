using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Simplic.Flow.Editor
{
    public abstract class NodeViewModel : Simplic.UI.MVC.ViewModelBase
    {
        #region Private Members
        private bool isActive;
        private Point position;
        private double width;
        private double height;
        private Guid id;

        private NodeDefinition nodeDefinition;
        private NodeConfiguration nodeConfiguration;
        #endregion

        #region Constructor
        public NodeViewModel(NodeDefinition nodeDefinition, NodeConfiguration nodeConfiguration)
        {
            this.nodeDefinition = nodeDefinition;
            this.nodeConfiguration = nodeConfiguration;

            FlowPins = new ObservableCollection<FlowConnectorViewModel>();
            DataPins = new ObservableCollection<DataConnectorViewModel>();

            FillPins();
        }
        #endregion

        #region Private Methods

        #region [FillPins]
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
        #endregion

        #endregion

        #region Public Properties

        #region [FlowPins]
        public ObservableCollection<FlowConnectorViewModel> FlowPins { get; }
        #endregion
        
        #region [DataPins]
        public ObservableCollection<DataConnectorViewModel> DataPins { get; }
        #endregion

        #region [Id]
        public Guid Id
        {
            get
            {
                return id;
            }

            set
            {
                IsDirty = true;
                id = value;

                RaisePropertyChanged(nameof(Id));
            }
        }
        #endregion

        #region [IsActive]
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
        #endregion

        #region [DisplayName]
        public string DisplayName
        {
            get { return nodeDefinition.DisplayName; }
        }
        #endregion

        #region [Tooltip]
        public string Tooltip
        {
            get { return nodeDefinition.Tooltip; }
        }
        #endregion

        #region [Position]
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
        #endregion

        #region [Width]
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
        #endregion

        #region [Height]
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
        #endregion

        #endregion
    }
}
