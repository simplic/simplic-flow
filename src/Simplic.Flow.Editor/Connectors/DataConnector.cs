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
            DataContext = this;
            ToolTip = $"{Text} ({connectorDataType.Name})";

            FillDataTemplate();
        }

        private void FillDataTemplate()
        {
            var styleTemplate = ConnectorDirection == ConnectorDirection.In ? "Left" : "Right";            
            
            if (Application.Current.Resources.Contains($"DataConnector{ConnectorDataType.Name}Template"))
                this.Style = Application.Current.Resources[$"DataConnector{ConnectorDataType.Name}Template"] as Style;
            else
                this.Style = Application.Current.Resources[$"DataConnectorTemplate"] as Style;
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