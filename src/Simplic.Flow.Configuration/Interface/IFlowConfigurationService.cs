using System;

namespace Simplic.Flow.Configuration
{
    /// <summary>
    /// Interface for flow configuration operations service.
    /// </summary>
    public interface IFlowConfigurationService : IFlowConfigurationRepository
    {
        /// <summary>
        /// Clones a configuration, gives it a new name and a new id.
        /// </summary>
        /// <param name="configurationGuid">Configuration Id to clone</param>
        /// <param name="newConfigurationName">New configuration name</param>
        /// <returns>Clone configuration</returns>
        FlowConfiguration Clone(Guid configurationGuid, string newConfigurationName);
    }
}