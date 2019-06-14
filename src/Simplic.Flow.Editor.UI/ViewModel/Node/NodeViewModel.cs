using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;
using Simplic.UI.MVC;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// NodeViewModel
    /// </summary>
    public abstract class NodeViewModel : ViewModelBase
    {
        #region Private Members                
        private NodeDefinition nodeDefinition;
        private NodeConfiguration nodeConfiguration;

        private const double DefaultWidth = 200;
        private const double DefaultHeight = 150;
        private ObservableCollection<DataPinDefaultValueViewModel> defaultValues;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodeDefinition">NodeDefinition</param>
        /// <param name="nodeConfiguration">NodeConfiguration</param>
        public NodeViewModel(NodeDefinition nodeDefinition, NodeConfiguration nodeConfiguration)
        {
            this.nodeDefinition = nodeDefinition;
            this.nodeConfiguration = nodeConfiguration;

            FlowPins = new ObservableCollection<FlowConnectorViewModel>();
            DataPins = new ObservableCollection<DataConnectorViewModel>();
            defaultValues = new ObservableCollection<DataPinDefaultValueViewModel>();

            FillPins();

            foreach (var inPin in DataPins.Where(x => x.PinDirection == PinDirectionDefinition.In))
            {
                // Skip if array
                
                var configuration = new NodePinConfiguration
                {
                    Name = inPin.Name,
                    DefaultValue = nodeConfiguration.Pins.FirstOrDefault(x => x.Name == inPin.Name)?.DefaultValue
                };

                defaultValues.Add(new DataPinDefaultValueViewModel(configuration, inPin.DisplayName)
                {
                    Parent = this
                });
            }

            if (nodeConfiguration == null)
                nodeConfiguration = new NodeConfiguration();
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

        #region Public Methods

        #region [CreateConfiguration]
        /// <summary>
        /// Creates a configuration with default values
        /// </summary>
        /// <returns>NodeConfiguration</returns>
        public virtual NodeConfiguration CreateConfiguration()
        {
            nodeConfiguration.Pins.Clear();
            foreach (var defaultValue in defaultValues)
            {
                if (!string.IsNullOrWhiteSpace(defaultValue.DefaultValue))
                {
                    nodeConfiguration.Pins.Add(new NodePinConfiguration()
                    {
                        DefaultValue = defaultValue.DefaultValue,
                        Name = defaultValue.PinName
                    });
                }
            }

            return nodeConfiguration;
        }
        #endregion

        #endregion

        protected void CreateFlowInPin()
        {
            if (!nodeDefinition.InFlowPins.Any())
            {
                var pin = new FlowPinDefinition
                {
                    AllowMultiple = false,
                    DisplayName = "In",
                    Id = Guid.NewGuid(),
                    Name = "FlowIn",
                    PinDirection = PinDirectionDefinition.In
                };
                nodeDefinition.InFlowPins.Add(pin);

                FlowPins.Add(new FlowConnectorViewModel(pin)
                {
                    Parent = this,
                    PinDirection = PinDirectionDefinition.In
                });
            }
        }

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
            get { return nodeConfiguration.Id; }
            set
            {
                IsDirty = true;
                nodeConfiguration.Id = value;
                RaisePropertyChanged(nameof(Id));
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
            get { return new Point(nodeConfiguration.PositionX, nodeConfiguration.PositionY); }
            set
            {
                nodeConfiguration.PositionX = value.X;
                nodeConfiguration.PositionY = value.Y;
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
                return nodeConfiguration.Width;
            }
            set
            {
                nodeConfiguration.Width = value;
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
                return nodeConfiguration.Height;
            }

            set
            {
                nodeConfiguration.Height = value;
                IsDirty = true;
                RaisePropertyChanged(nameof(Height));
            }
        }
        #endregion

        #region [DefaultValues]
        public ObservableCollection<DataPinDefaultValueViewModel> DefaultValues
        {
            get { return defaultValues; }
            set { defaultValues = value; RaisePropertyChanged(nameof(DefaultValues)); }
        } 
        #endregion

        #endregion
    }
}