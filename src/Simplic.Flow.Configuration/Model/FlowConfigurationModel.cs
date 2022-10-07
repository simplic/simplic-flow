using System;

namespace Simplic.Flow.Configuration
{
    /// <summary>
    /// Represents FlowConfiguration table in the db
    /// </summary>
    public class FlowConfigurationModel
    {
        /// <summary>
        /// Gets or sets the Id of the flow 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the flow
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the flow configuration json as byte array. 
        /// </summary>
        public byte[] Configuration { get; set; }

        /// <summary>
        /// Gets or sets if the configuration is active. If not, it will not be processed.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the flow configuration is deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets the current machine name
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the service name
        /// </summary>
        public string ServiceName { get; set; }
    }
}
