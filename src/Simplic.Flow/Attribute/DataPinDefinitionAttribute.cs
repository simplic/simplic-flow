using System;

namespace Simplic.Flow
{
    public class DataPinDefinitionAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public DataPinContainerType ContainerType { get; set; }
        public string Name { get; set; }
        public Type DataType { get; set; }
        public PinDirection Direction { get; set; }
        public string Id { get; set; }
    }
}