﻿using Simplic.Flow.Editor.Definition;
using Simplic.Framework.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// Base node shape, sets default values and connectors
    /// </summary>
    public abstract class BaseNodeShape : RadDiagramShape
    {
        #region Private Members
        private IDictionary<TextBlock, Point> outPinTexts = new Dictionary<TextBlock, Point>();
        private bool isPinTextCreated = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseNodeShape()
        {
            //DataContext = this;
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom("#0f0f0f");
            this.Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RectangleShape);
            this.IsRotationEnabled = false;
            this.IsResizingEnabled = false;
            this.UseDefaultConnectors = false;
            this.UseGlidingConnector = false;
            this.BorderBrush = (Brush)bc.ConvertFrom("#000000");
            this.BorderThickness = new Thickness(1);
            this.IsEditable = false;
            this.IsManipulationAdornerVisible = false;
            this.MinWidth = 200;
            this.MinHeight = 150;
        }
        #endregion

        #region Protected Methods

        #region [OnIsSelectedChanged]
        /// <summary>
        /// Override OnIsSelectedChanged to highlight selected node
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            if (newValue == true)
            {
                this.BorderThickness = new Thickness(2);
                this.BorderBrush = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                this.BorderThickness = new Thickness(1);
                this.BorderBrush = new SolidColorBrush(Colors.Black);
            }

            base.OnIsSelectedChanged(oldValue, newValue);
        }
        #endregion

        #region [UpdateVisualStates]
        /// <summary>
        /// Overrides UpdateVisualStates to always show connectors
        /// </summary>
        protected override void UpdateVisualStates()
        {
            base.UpdateVisualStates();

            // show always connectors
            VisualStateManager.GoToState(this, "ConnectorsAdornerVisible", false);
        } 
        #endregion

        #endregion

        #region Private Methods

        #region [Text_Loaded]
        /// <summary>
        /// Text loaded event, rearranges text positions based on their width
        /// </summary>
        /// <param name="sender">Text object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void Text_Loaded(object sender, RoutedEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var position = outPinTexts[textBlock];

            textBlock.SetLocation(position.X - textBlock.ActualWidth - 10, position.Y);

            textBlock.Loaded -= Text_Loaded;
            outPinTexts.Remove(textBlock);
        } 
        #endregion

        #endregion

        #region Public Methods

        #region [LoadConnectorText]
        /// <summary>
        /// Creates connector text based on connectors
        /// </summary>
        public void LoadConnectorText()
        {
            if (isPinTextCreated) return;

            var leftOffset = 8;
            var topOffset = 8.5d;

            var leftOffsetFlow = 9.5d;
            var topOffsetFlow = 5.5d;

            var canvas = WPFVisualTreeHelper.FindChild<Canvas>(this);
            var connectors = WPFVisualTreeHelper.FindChildren<RadDiagramConnector>(this);

            foreach (var item in connectors)
            {
                var connector = item as BaseConnector;

                var text = new TextBlock { Text = connector.Text, Foreground = Brushes.White };
                canvas.Children.Add(text);

                var connectorLocation = item.TranslatePoint(new Point(0, 0), this);

                if (connector.ConnectorDirection == ConnectorDirection.In)
                {
                    if (connector is FlowConnector)
                        text.SetLocation(connectorLocation.X + leftOffsetFlow, connectorLocation.Y - topOffsetFlow);
                    else
                        text.SetLocation(connectorLocation.X + leftOffset, connectorLocation.Y - topOffset);
                }
                else
                {
                    if (connector is FlowConnector)
                        outPinTexts[text] = new Point(connectorLocation.X, connectorLocation.Y - topOffsetFlow);
                    else
                        outPinTexts[text] = new Point(connectorLocation.X, connectorLocation.Y - topOffset);                    

                    text.Loaded += Text_Loaded;
                }
            }

            isPinTextCreated = true;
        }
        #endregion

        #region [CreateConnectors]
        /// <summary>
        /// Creates connectors and calls LoadConnectorText method
        /// </summary>
        public virtual void CreateConnectors()
        {
            if (ViewModel?.DataPins == null || ViewModel?.FlowPins == null)
                return;

            // Fill connector list
            foreach (var pin in ViewModel.DataPins)
            {
                var dataConnector = new DataConnector(pin.Name, pin.DisplayName,
                    pin.PinDirection == PinDirectionDefinition.In ? ConnectorDirection.In : ConnectorDirection.Out,
                    pin.Type)
                {
                    DataContext = pin
                };

                DataConnectors.Add(dataConnector);
            }

            foreach (var pin in ViewModel.FlowPins)
            {
                var flowConnector = new FlowConnector(pin.Name, pin.DisplayName,
                    pin.PinDirection == PinDirectionDefinition.In ? ConnectorDirection.In : ConnectorDirection.Out)
                {
                    DataContext = pin
                };

                FlowConnectors.Add(flowConnector);
            }

            double heightOffset = 0.28;
            double xLeft = 0.04;
            double xRight = 0.96;
            double yTop = heightOffset;

            foreach (var flowConnector in FlowConnectors
                .Where(connector => connector.ConnectorDirection == ConnectorDirection.In))
            {
                flowConnector.Offset = new Point(xLeft, yTop);
                this.Connectors.Add(flowConnector);
                yTop += 0.12;
            }

            foreach (var dataConnector in DataConnectors
                .Where(connector => connector.ConnectorDirection == ConnectorDirection.In))
            {
                dataConnector.Offset = new Point(xLeft - 0.01, yTop);
                this.Connectors.Add(dataConnector);
                yTop += 0.12;
            }

            yTop = heightOffset;
            foreach (var flowConnector in FlowConnectors
                .Where(connector => connector.ConnectorDirection == ConnectorDirection.Out))
            {
                flowConnector.Offset = new Point(xRight, yTop);
                this.Connectors.Add(flowConnector);
                yTop += 0.12;
            }

            foreach (var dataConnector in DataConnectors
                .Where(connector => connector.ConnectorDirection == ConnectorDirection.Out))
            {
                dataConnector.Offset = new Point(xRight, yTop);
                this.Connectors.Add(dataConnector);
                yTop += 0.12;
            }            
        } 

        #endregion

        #region [Serialize]
        /// <summary>
        /// Overrides Serialize methods to carry custom data
        /// </summary>
        /// <returns>SerializationInfo</returns>
        public override Telerik.Windows.Diagrams.Core.SerializationInfo Serialize()
        {
            var obj = base.Serialize();

            obj[nameof(Name)] = Name;

            return obj;
        }
        #endregion

        #region [Deserialize]
        /// <summary>
        /// Overrides Deserialize methods to get custom data
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        public override void Deserialize(Telerik.Windows.Diagrams.Core.SerializationInfo info)
        {
            base.Deserialize(info);

            this.Name = info[nameof(Name)]?.ToString();
        }
        #endregion

        #endregion

        #region Public Properties

        #region [HeaderText]
        public string HeaderText { get { return ViewModel.DisplayName; } }
        #endregion

        #region [TooltipText]
        public string TooltipText { get; set; }
        #endregion

        #region [FlowConnectors]
        public IList<FlowConnector> FlowConnectors { get; set; } = new List<FlowConnector>();
        #endregion

        #region [DataConnectors]
        public IList<DataConnector> DataConnectors { get; set; } = new List<DataConnector>();
        #endregion

        #region [ViewModel]
        public NodeViewModel ViewModel { get { return DataContext as NodeViewModel; } }  
        #endregion

        #endregion
    }
}
