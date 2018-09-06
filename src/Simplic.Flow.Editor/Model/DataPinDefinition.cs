using System;

namespace Simplic.Flow.Editor
{
    public class DataPinDefinition
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Type Type { get; set; }
        public PinDirectionDefinition PinDirection { get; set; }
    }
}
