using System;
using Telerik.Windows.Controls.Diagrams;

namespace Simplic.Flow.Editor
{
    public abstract class BaseConnector : RadDiagramConnector
    {
        public BaseConnector(string name, string text, ConnectorDirection connectorDirection)
        {
            this.Name = name;
            this.Text = text;
            this.ConnectorDirection = connectorDirection;                  
        }

        public string Text { get; set; }
        public ConnectorDirection ConnectorDirection { get; set; }        
    }
}