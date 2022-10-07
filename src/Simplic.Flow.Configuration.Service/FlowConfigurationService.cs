using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration.Service
{
    /// <summary>
    /// Service for flow configuration operations.
    /// </summary>
    public class FlowConfigurationService : IFlowConfigurationService
    {
        private readonly IFlowConfigurationRepository flowConfigurationRepository;

        /// <summary>
        /// Constructor for FlowConfigurationService.
        /// </summary>
        public FlowConfigurationService(IFlowConfigurationRepository flowConfigurationRepository)
        {
            this.flowConfigurationRepository = flowConfigurationRepository;
        }

        /// <summary>
        /// Clones a configuration, gives it a new name and a new id.
        /// </summary>
        /// <param name="configurationGuid">Configuration Id to clone</param>
        /// <param name="newConfigurationName">New configuration name</param>
        /// <returns>Clone configuration</returns>
        public FlowConfiguration Clone(Guid configurationGuid, string newConfigurationName)
        {
            var original = flowConfigurationRepository.Get(configurationGuid);
            if (original == null)
                throw new FlowConfigurationServiceException($"Flow Configuration with id {configurationGuid} was not found.");

            original.Id = Guid.NewGuid();
            original.Name = newConfigurationName;

            return original;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public FlowConfiguration Get(Guid id)
        {
            return flowConfigurationRepository.Get(id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<FlowConfiguration> GetAll(bool getOnlyActive = true)
        {
            return flowConfigurationRepository.GetAll(getOnlyActive);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Save(FlowConfiguration flowConfiguration)
        {
            return flowConfigurationRepository.Save(flowConfiguration);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool SetStatus(Guid id, bool isActive)
        {
            return flowConfigurationRepository.SetStatus(id, isActive);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool SetDeleted(Guid id)
        {
            return flowConfigurationRepository.SetDeleted(id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public FlowConfiguration GetByExportId(Guid exportId)
        {
            return flowConfigurationRepository.GetByExportId(exportId);
        }
    }
}