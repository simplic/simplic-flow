using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;

namespace Simplic.Flow.Editor
{
    public class FunctionNode : RadDiagramShape
    {
        static FunctionNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FunctionNode), 
                new FrameworkPropertyMetadata(typeof(FunctionNode)));            
        }

        public FunctionNode()
        {
            this.BorderBrush = Brushes.Black;
            this.BorderThickness = new Thickness(5);
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
            var flowIn = new FlowConnector()
            {
                Offset = new Point(0, 0.23),
                Name = "flowIn",
                FlowConnectorDirection = FlowConnectorDirection.In                
            };

            var flowOut = new FlowConnector()
            {
                Offset = new Point(1, 0.23),
                Name = "flowOut",
                FlowConnectorDirection = FlowConnectorDirection.Out
            };


            this.Connectors.Add(flowIn);
            this.Connectors.Add(flowOut);
        }

        protected override void UpdateVisualStates()
        {
            base.UpdateVisualStates();
            // show always connectors
            VisualStateManager.GoToState(this, "ConnectorsAdornerVisible", false);
        }
    }
}
