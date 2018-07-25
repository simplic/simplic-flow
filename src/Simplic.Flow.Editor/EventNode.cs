using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;

namespace Simplic.Flow.Editor
{
    public class EventNode : RadDiagramShape
    {
        static EventNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventNode), 
                new FrameworkPropertyMetadata(typeof(EventNode)));
        }

        public EventNode()
        {
            this.BorderBrush = Brushes.Black;
            this.BorderThickness = new Thickness(2);
            this.Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RectangleShape);
            this.IsRotationEnabled = false;
            this.IsResizingEnabled = false;
            this.UseDefaultConnectors = false;
            this.UseGlidingConnector = false;
            this.Width = 200;
            this.Height = 150;
            this.Padding = new Thickness(0);
            this.Stroke = Brushes.Yellow;
            this.StrokeThickness = 2;

            CreateHeader();
            CreateConnectors();            
        }

        private void CreateHeader()
        {
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom("#0f0f0f");            
        }

        private void CreateConnectors()
        {
            var flowOut = new FlowConnector()
            {
                Offset = new Point(0.96, 0.24),
                FlowConnectorDirection = FlowConnectorDirection.Out,
                Name = "FlowOut"                
            };

            var dataOut = new DataConnector()
            {
                Offset = new Point(0.96, 0.75),
                FlowConnectorDirection = FlowConnectorDirection.Out,
                Name = "DataOut"                
            };

            this.Connectors.Add(flowOut);
            this.Connectors.Add(dataOut);
        }

        protected override void UpdateVisualStates()
        {
            base.UpdateVisualStates();
            VisualStateManager.GoToState(this, "ConnectorsAdornerVisible", false);
        }
    }
}