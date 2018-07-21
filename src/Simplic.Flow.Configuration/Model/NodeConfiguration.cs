using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration
{
    public class NodeConfiguration
    {
        public Guid Id { get; set; }
        public string NodeType { get; set; }
        public string ClassName { get; set; }
        public bool IsStartEvent { get; set; }
        public List<NodePinConfiguration> Pins { get; set; } = new List<NodePinConfiguration>();
    }
}