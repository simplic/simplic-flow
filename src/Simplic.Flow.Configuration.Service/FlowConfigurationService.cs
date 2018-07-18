using System;
using System.Collections.Generic;

namespace Simplic.Flow.Configuration.Service
{
    public class FlowConfigurationService : IFlowConfigurationService
    {
        private readonly IFlowConfigurationRepository flowConfigurationRepository;
        public FlowConfigurationService(IFlowConfigurationRepository flowConfigurationRepository)
        {
            this.flowConfigurationRepository = flowConfigurationRepository;
        }

        public FlowConfiguration Get(Guid id)
        {
            return this.flowConfigurationRepository.Get(id);
        }

        public IEnumerable<FlowConfiguration> GetAll()
        {
            return this.flowConfigurationRepository.GetAll();
        }

        public bool Save(FlowConfiguration flowConfiguration)
        {            
            return flowConfigurationRepository.Save(flowConfiguration);
        }
    }
}
