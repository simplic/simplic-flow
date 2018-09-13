using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;
using Simplic.UI.MVC;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Simplic.Flow.Editor
{
    public abstract class NodeViewModel : ViewModelBase
    {
        #region Private Members                
        private NodeDefinition nodeDefinition;
        private NodeConfiguration nodeConfiguration;

        private const double DefaultWidth = 200;
        private const double DefaultHeight = 150;
        private ICommand openDefaultValueEditor;
        private ObservableCollection<DataPinDefaultValueViewModel> defaultValues;
        #endregion

        #region Constructor
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

                defaultValues.Add(new DataPinDefaultValueViewModel(configuration) { Parent = this });
            }

            if (nodeConfiguration == null)
                nodeConfiguration = new NodeConfiguration();

            openDefaultValueEditor = new RelayCommand((e) =>
            {
                var win = new Window();

                var content = new StackPanel();
                content.VerticalAlignment = VerticalAlignment.Stretch;
                content.HorizontalAlignment = HorizontalAlignment.Stretch;
                content.Orientation = Orientation.Vertical;

                foreach (var d in DefaultValues)
                    content.Children.Add(new Label() { Content = d.PinName });

                win.Content = content;
                win.Title = DisplayName;
                win.ShowDialog();
            });
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
                return nodeConfiguration.Id;
            }

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
                if (nodeConfiguration.Width == 0)
                    nodeConfiguration.Width = DefaultWidth;

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
                if (nodeConfiguration.Height == 0)
                    nodeConfiguration.Height = DefaultHeight;

                return nodeConfiguration.Height;
            }
            set
            {
                nodeConfiguration.Height = value;
                IsDirty = true;
                RaisePropertyChanged(nameof(Height));
            }
        }

        public ICommand OpenDefaultValueEditor { get => openDefaultValueEditor; set => openDefaultValueEditor = value; }
        internal ObservableCollection<DataPinDefaultValueViewModel> DefaultValues { get => defaultValues; set => defaultValues = value; }
        #endregion

        #endregion
    }
}
