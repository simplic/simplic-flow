using System;
using System.Windows;

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
        /// <param name="isGeneric">Is a generic type ?</param>
        /// <param name="allowedTypes">if a generic type, this limits the types</param>
        public DataConnector(string name, string text, ConnectorDirection connectorDirection, Type connectorDataType, bool isGeneric, string allowedTypes) 
            : base(name, text, connectorDirection)
        {
            this.ConnectorDataType = connectorDataType;            
            this.DataContext = this;
            this.ToolTip = $"{Text} ({connectorDataType?.Name})";
            this.IsGeneric = isGeneric;
            this.AllowedTypes = allowedTypes;
            
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

        public bool IsGeneric { get; set; }
        public string AllowedTypes { get; set; }
    }
}