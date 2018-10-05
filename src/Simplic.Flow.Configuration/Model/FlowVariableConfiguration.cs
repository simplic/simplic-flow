using System;

namespace Simplic.Flow.Configuration
{
    public class FlowVariableConfiguration
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }
    }
}
