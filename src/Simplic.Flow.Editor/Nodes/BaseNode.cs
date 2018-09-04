using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;

namespace Simplic.Flow.Editor
{
    public abstract class BaseNode : RadDiagramShape
    {
        public BaseNode(string headerText, IList<FlowConnector> flowConnectors, IList<DataConnector> dataConnectors)
        {
            DataContext = this;
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom("#0f0f0f");
            this.Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RectangleShape);
            this.IsRotationEnabled = false;
            this.IsResizingEnabled = false;
            this.UseDefaultConnectors = false;
            this.UseGlidingConnector = false;
            this.Width = 200;
            this.Height = 150;
            this.BorderBrush = (Brush)bc.ConvertFrom("#000000");
            this.BorderThickness = new Thickness(1);
            this.IsEditable = false;

            this.HeaderText = headerText;
            this.FlowConnectors = flowConnectors;
            this.DataConnectors = dataConnectors;

            CreateHeader();
            CreateConnectors();
        }        

        protected virtual void CreateConnectors()
        {
            double xLeft = 0.04;
            double xRight = 0.96;
            double yTop = 0.26;

            foreach (var flowConnector in FlowConnectors
                .Where(connector => connector.ConnectorDirection == ConnectorDirection.In))
            {
                flowConnector.Offset = new Point(xLeft, yTop);
                this.Connectors.Add(flowConnector);
                yTop += 0.10;
            }

            foreach (var dataConnector in DataConnectors
                .Where(connector => connector.ConnectorDirection == ConnectorDirection.In))
            {
                dataConnector.Offset = new Point(xLeft, yTop);
                this.Connectors.Add(dataConnector);
                yTop += 0.10;
            }

            yTop = 0.26;
            foreach (var flowConnector in FlowConnectors
                .Where(connector => connector.ConnectorDirection == ConnectorDirection.Out))
            {
                flowConnector.Offset = new Point(xRight, yTop);
                this.Connectors.Add(flowConnector);
                yTop += 0.10;
            }

            foreach (var dataConnector in DataConnectors
                .Where(connector => connector.ConnectorDirection == ConnectorDirection.Out))
            {
                dataConnector.Offset = new Point(xRight, yTop);
                this.Connectors.Add(dataConnector);
                yTop += 0.10;
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
        public IList<FlowConnector> FlowConnectors { get; set; }
        public IList<DataConnector> DataConnectors { get; set; }        
    }
}
