using System;

namespace Simplic.Flow
{
    public class NodeDefinitionAttribute : Attribute
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
