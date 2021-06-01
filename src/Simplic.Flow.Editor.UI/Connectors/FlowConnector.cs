using System.Windows;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// Flow connector type
    /// </summary>
    public class FlowConnector : BaseConnector
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Connector name</param>
        /// <param name="text">Connector display name</param>
        /// <param name="connectorDirection">Connector direction</param>
        public FlowConnector(string name, string text, ConnectorDirection connectorDirection)
            : base(name, text, connectorDirection)
        {
            this.Style = TryFindResource("FlowConnectorTemplate") as Style;            
            ToolTip = Text;            
        }                
    }
}
