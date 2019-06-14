using System;

namespace Simplic.Flow
{
    /// <summary>
    /// Attribute for defining pins
    /// </summary>
    public class DataPinDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the unique (flow wide unique) Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the unique(in node) name of the pin
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name of the pin
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the tool tip text used on GUI
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Gets or sets the container type (e.g. List, Single)
        /// </summary>
        public DataPinContainerType ContainerType { get; set; }
        
        /// <summary>
        /// Gets or sets the data type of the pin. Could be null if <see cref="IsGeneric"/> is true.
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        /// Gets or sets the direction of the pin
        /// </summary>
        public PinDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the default value of the pin
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets if the pin has a generic <see cref="DataType"/>
        /// </summary>
        public bool IsGeneric { get; set; }

        /// <summary>
        /// Gets or sets allowed types as string seperated by comma (e.g. "UInt32,Single,Int64")
        /// </summary>
        public string AllowedTypes { get; set; }
    }
}