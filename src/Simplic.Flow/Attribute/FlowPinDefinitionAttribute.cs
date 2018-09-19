using System;

namespace Simplic.Flow
{
    public class FlowPinDefinitionAttribute : Attribute
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Tooltip { get; set; }
        public PinDirection PinDirection { get; set; }
        public bool AllowMultiple { get; set; } = false;
    }
}
