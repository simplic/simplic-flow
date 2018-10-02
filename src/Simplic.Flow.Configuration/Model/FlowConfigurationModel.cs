using System;

namespace Simplic.Flow.Configuration
{
    /// <summary>
    /// Represents FlowConfiguration table in the db
    /// </summary>
    public class FlowConfigurationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Configuration { get; set; }
    }
}
