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

            var jsonFileContent = File.ReadAllText("c:\\users\\yavuz\\desktop\\flow.json");
            var jsonObj = JsonConvert.DeserializeObject<List<FlowConfiguration>>(jsonFileContent,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

            if (jsonObj != null)            
                flowConfigurations = jsonObj;                
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
            //var list = new List<FlowConfiguration>();
            //list.Add(flowConfiguration);
            //list.Add(flowConfiguration);
            //list.Add(flowConfiguration);

            //string serializedConfiguration = JsonConvert.SerializeObject(list, Formatting.Indented,
            //    new JsonSerializerSettings
            //    {
            //        TypeNameHandling = TypeNameHandling.All,
            //        PreserveReferencesHandling = PreserveReferencesHandling.Objects
            //    });            

            flowConfigurations.Add(flowConfiguration);
            return true;
        }
    }
}
