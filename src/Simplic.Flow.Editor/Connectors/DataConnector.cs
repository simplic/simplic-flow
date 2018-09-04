using System;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor
{
    public class DataConnector : BaseConnector
    {
        public DataConnector(string name, string text, ConnectorDirection connectorDirection, Type connectorDataType) 
            : base(name, text, connectorDirection)
        {
            this.ConnectorDataType = connectorDataType;
            var styleTemplate = connectorDirection == ConnectorDirection.In ? "Left" : "Right";
            this.Style = Application.Current.Resources[$"DataConnector{styleTemplate}Template"] as Style;
            DataContext = this;
            ToolTip = $"{Text} ({connectorDataType.Name})";
        }
        
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (this.Connection != null || this.ConnectorDirection == ConnectorDirection.In)
                this.Cursor = Cursors.No;
            else
                this.Cursor = Cursors.Arrow;

            base.OnMouseEnter(e);
        }

        public IConnection Connection { get; set; }
        public Type ConnectorDataType { get; set; }
    }
}