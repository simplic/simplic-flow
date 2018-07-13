using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.Flow.Configuration.Data.Memory
{
    public class FlowConfigurationMemoryRepository : IFlowConfigurationRepository
    {
        private readonly IList<FlowConfiguration> flowConfigurations;

        public FlowConfigurationMemoryRepository()
        {
            flowConfigurations = new List<FlowConfiguration>();
        }

        public FlowConfiguration Get(Guid id)
        {
            return flowConfigurations.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<FlowConfiguration> GetAll()
        {
            return flowConfigurations;
        }

        public bool Save(FlowConfiguration flowConfiguration)
        {
            string serializedConfiguration = JsonConvert.SerializeObject(flowConfiguration, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });            

            flowConfigurations.Add(flowConfiguration);
            return true;
        }
    }
}
