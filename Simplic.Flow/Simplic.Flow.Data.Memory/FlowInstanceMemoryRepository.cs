﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.FlowInstance.Data.Memory
{
    public class FlowInstanceMemoryRepository : IFlowInstanceRepository
    {
        private readonly IList<FlowInstance> flowInstances;
        public FlowInstanceMemoryRepository()
        {
            flowInstances = new List<FlowInstance>();
        }

        public IEnumerable<FlowInstance> GetAll()
        {
            return flowInstances;
        }

        public FlowInstance GetById(Guid instanceId)
        {
            return flowInstances.Where(x => x.Id == instanceId).FirstOrDefault();
        }

        public bool Save(FlowInstance flowInstance)
        {
            var index = flowInstances.IndexOf(flowInstance);            
            if (index > -1)                                    
                flowInstances[index] = flowInstance;
            else
                flowInstances.Add(flowInstance);


            string serializedConfiguration = JsonConvert.SerializeObject(flowInstance, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

            var obj = JsonConvert.DeserializeObject<FlowInstance>(serializedConfiguration, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

            return true;
        }

        //public void SetAsFinished(FlowInstance flowInstance)
        //{            
        //    flowInstance.IsAlive = false;
        //    flowInstances[flowInstances.IndexOf(flowInstance)] = flowInstance;
        //}
    }
}
