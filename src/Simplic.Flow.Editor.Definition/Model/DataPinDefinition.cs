using System;

namespace Simplic.Flow.Editor.Definition
{
    public class DataPinDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }        
        public Type Type { get; set; }
        public PinDirectionDefinition PinDirection { get; set; }        
    }
}
