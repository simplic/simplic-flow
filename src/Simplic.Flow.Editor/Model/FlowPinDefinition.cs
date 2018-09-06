using System;

namespace Simplic.Flow.Editor
{
    public class FlowPinDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public PinDirectionDefinition PinDirection { get; set; }
    }
}
