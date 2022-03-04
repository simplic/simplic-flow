using System;

namespace Simplic.Flow
{
    public class NodeDefinitionAttribute : Attribute
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Tooltip { get; set; }
        public string Category { get; set; }
        /// <summary>
        /// Gets or sets the node documentation URL.
        /// </summary>
        public string DocumentationUrl { get; set; }
    }
}
