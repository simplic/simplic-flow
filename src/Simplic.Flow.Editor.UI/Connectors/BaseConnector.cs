using System.Windows;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// Base Connector
    /// </summary>
    public abstract class BaseConnector : RadDiagramConnector
    {
        private bool isPinTextCreated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Connector name</param>
        /// <param name="text">Connector display name</param>
        /// <param name="connectorDirection">Connector direction</param>
        public BaseConnector(string name, string text, ConnectorDirection connectorDirection)
        {
            this.Name = name;
            this.Text = text;
            this.ConnectorDirection = connectorDirection;
            this.Loaded += BaseConnector_Loaded;            
        }

        private void BaseConnector_Loaded(object sender, RoutedEventArgs e)
        {
            if (isPinTextCreated) return;

            var parentNode = Framework.UI.WPFVisualTreeHelper.FindParent<BaseNodeShape>(this);
            parentNode.LoadConnectorText(this);

            this.Loaded -= BaseConnector_Loaded;

            isPinTextCreated = true;
        }

        /// <summary>
        /// Shows a cursor on mouse over
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            if (this.Connection != null || this.ConnectorDirection == ConnectorDirection.In)
                this.Cursor = System.Windows.Input.Cursors.No;
            else
                this.Cursor = System.Windows.Input.Cursors.Arrow;

            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Gets or sets the text of the connector
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the connector direction
        /// </summary>
        public ConnectorDirection ConnectorDirection { get; set; }
        
        /// <summary>
        /// Gets or sets the connection variable
        /// </summary>
        public IConnection Connection { get; set; }
    }
}