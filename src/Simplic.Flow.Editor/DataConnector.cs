using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    public class DataConnector : RadDiagramConnector
    {
        public DataConnector()
        {
            this.Style = Application.Current.Resources["DataConnectorTemplate"] as Style;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (this.Connection != null || this.FlowConnectorDirection == FlowConnectorDirection.In)
                this.Cursor = Cursors.No;
            else
                this.Cursor = Cursors.Arrow;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
        }

        public FlowConnectorDirection FlowConnectorDirection { get; set; }
        public IConnection Connection { get; set; }
    }
}