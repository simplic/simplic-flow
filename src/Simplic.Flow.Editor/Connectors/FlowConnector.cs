using System;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    public class FlowConnector : BaseConnector
    {
        public FlowConnector(string name, string text, ConnectorDirection connectorDirection)
            : base(name, text, connectorDirection)
        {
            this.Style = Application.Current.Resources["FlowConnectorTemplate"] as Style;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {            
            if (this.Connection != null || this.ConnectorDirection == ConnectorDirection.In)
                this.Cursor = Cursors.No;
            else
                this.Cursor = Cursors.Arrow;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {            
            base.OnMouseLeave(e);
        }

        
        public IConnection Connection { get; set; }
        
    }
}
