using System.Windows.Input;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    public class FlowConnector : RadDiagramConnector
    {
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (this.Connection != null || this.FlowConnectorDirection.ToString() == "drIn")
                this.Cursor = Cursors.No;
            else
                this.Cursor = Cursors.Pen;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            //(this as IConnector).IsActive = false;
            base.OnMouseLeave(e);
        }

        public FlowConnectorDirection FlowConnectorDirection { get; set; }
        public IConnection Connection { get; set; }
    }
}
