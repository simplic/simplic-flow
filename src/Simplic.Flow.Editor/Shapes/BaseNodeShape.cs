﻿using Simplic.Flow.Editor.Definition;
using Simplic.Framework.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;

namespace Simplic.Flow.Editor
{
    public abstract class BaseNodeShape : RadDiagramShape
    {
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
            this.MinWidth = 200;
            this.MinHeight = 150;
            this.BorderBrush = (Brush)bc.ConvertFrom("#000000");
            this.BorderThickness = new Thickness(1);
            this.IsEditable = false;

            Loaded += BaseNodeShape_Loaded;
        }

        private IDictionary<TextBlock, Point> outPinTexts = new Dictionary<TextBlock, Point>();

        private void BaseNodeShape_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConnectorText();
            Loaded -= BaseNodeShape_Loaded;
        }

        public void LoadConnectorText()
        {
            var leftOffset = 6;
            var topOffset = 8.5d;

            var canvas = WPFVisualTreeHelper.FindChild<Canvas>(this);
            var connectors = WPFVisualTreeHelper.FindChildren<RadDiagramConnector>(this);
            foreach (var item in connectors)
            {
                var connector = item as BaseConnector;

                var text = new TextBlock { Text = connector.Text, Foreground = Brushes.White };
                canvas.Children.Add(text);

                var connectorLocation = item.TranslatePoint(new Point(0, 0), this);

                if (connector.ConnectorDirection == ConnectorDirection.In)
                    text.SetLocation(connectorLocation.X + leftOffset, connectorLocation.Y - topOffset);
                else
                {
                    outPinTexts[text] = new Point(connectorLocation.X, connectorLocation.Y - topOffset);

                    text.Loaded += Text_Loaded;
                }
            }
        }

        private void Text_Loaded(object sender, RoutedEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var position = outPinTexts[textBlock];

            textBlock.SetLocation(position.X - textBlock.ActualWidth - 10, position.Y);

            textBlock.Loaded -= Text_Loaded;
            outPinTexts.Remove(textBlock);
        }

        public virtual void CreateConnectors()
        {
            if (ViewModel?.DataPins == null || ViewModel?.FlowPins == null)
                return;

            // Fill connector list
            foreach (var pin in ViewModel.DataPins)
                DataConnectors.Add(new DataConnector(pin.Name, pin.Name, pin.PinDirection == PinDirectionDefinition.In ? ConnectorDirection.In : ConnectorDirection.Out, pin.Type)
                {
                    DataContext = pin
                });


            foreach (var pin in ViewModel.FlowPins)
                FlowConnectors.Add(new FlowConnector(pin.Name, pin.Name, pin.PinDirection == PinDirectionDefinition.In ? ConnectorDirection.In : ConnectorDirection.Out)
                {
                    DataContext = pin
                });

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
                dataConnector.Offset = new Point(xLeft, yTop);
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

        protected override void UpdateVisualStates()
        {
            base.UpdateVisualStates();

            // show always connectors
            VisualStateManager.GoToState(this, "ConnectorsAdornerVisible", false);
        }

        public string HeaderText { get; set; }
        public string TooltipText { get; set; }
        public IList<FlowConnector> FlowConnectors { get; set; } = new List<FlowConnector>();
        public IList<DataConnector> DataConnectors { get; set; } = new List<DataConnector>();
        public NodeViewModel ViewModel { get { return DataContext as NodeViewModel; } }
    }
}
