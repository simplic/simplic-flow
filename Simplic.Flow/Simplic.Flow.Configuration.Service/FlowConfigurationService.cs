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
            throw new NotImplementedException();
        }

        public IEnumerable<FlowConfiguration> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Save(FlowConfiguration flowConfiguration)
        {            
            return flowConfigurationRepository.Save(flowConfiguration);
        }
    }
}
