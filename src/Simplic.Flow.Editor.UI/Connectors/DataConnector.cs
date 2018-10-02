using System;
using System.Windows;
using System.Windows.Input;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// Data Connector type
    /// </summary>
    public class DataConnector : BaseConnector
    {        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Connector name</param>
        /// <param name="text">Connector display name</param>
        /// <param name="connectorDirection">Connector direction</param>
        /// <param name="connectorDataType">Connector data type</param>
        public DataConnector(string name, string text, ConnectorDirection connectorDirection, Type connectorDataType) 
            : base(name, text, connectorDirection)
        {
            this.ConnectorDataType = connectorDataType;            
            DataContext = this;
            ToolTip = $"{Text} ({connectorDataType.Name})";

            FillDataTemplate();
        }

        /// <summary>
        /// Decides which template to use based on data type
        /// </summary>
        private void FillDataTemplate()
        {            
            this.Style = TryFindResource($"DataConnectorTemplate") as Style;
        }

        /// <summary>
        /// Gets or sets the connector data type
        /// </summary>
        public Type ConnectorDataType { get; set; }
    }
}