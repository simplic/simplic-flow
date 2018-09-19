using System;

namespace Simplic.Flow.Editor.Definition
{
    public class FlowPinDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public string DisplayName { get; set; }
        public PinDirectionDefinition PinDirection { get; set; }
        public bool AllowMultiple { get; set; } = false;
    }
}
