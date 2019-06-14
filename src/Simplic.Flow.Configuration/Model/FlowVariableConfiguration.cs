using System;

namespace Simplic.Flow.Configuration
{
    /// <summary>
    /// Represents a flow variable
    /// </summary>
    public class FlowVariableConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the variable
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the variable
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the variable
        /// </summary>
        public Type Type { get; set; }
    }
}
