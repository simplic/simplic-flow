using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simplic.Flow.Configuration.Data.Memory
{
    public class FlowConfigurationMemoryRepository : IFlowConfigurationRepository
    {
        private readonly IList<FlowConfiguration> flowConfigurations;

        public FlowConfigurationMemoryRepository()
        {
            flowConfigurations = new List<FlowConfiguration>();

            var filePath = "C:\\dev\\simplic-flow\\Simplic.Flow\\Simplic.Flow.Console\\SampleFlow.json";
            if (File.Exists(filePath))
            {
                var jsonFileContent = File.ReadAllText(filePath);
                var jsonObj = JsonConvert.DeserializeObject<List<FlowConfiguration>>(jsonFileContent);

                if (jsonObj != null)
                    flowConfigurations = jsonObj;
            }            
        }

        public FlowConfiguration Get(Guid id)
        {
            return flowConfigurations.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<FlowConfiguration> GetAll()
        {
            return flowConfigurations;
        }

        public IEnumerable<FlowConfiguration> GetAll(bool getOnlyActive = true)
        {
            throw new NotImplementedException();
        }

        public FlowConfiguration GetByExportId(Guid exportId)
        {
            throw new NotImplementedException();
        }

        public bool Save(FlowConfiguration flowConfiguration)
        {
            string serializedConfiguration = JsonConvert.SerializeObject(flowConfiguration, Formatting.Indented);

            flowConfigurations.Add(flowConfiguration);
            return true;
        }

        public bool SetDeleted(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool SetStatus(Guid id, bool isActive)
        {
            throw new NotImplementedException();
        }
    }
}