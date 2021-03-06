﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Simplic.Flow;

namespace Simplic.FlowInstance.Data.Memory
{
    public class FlowInstanceMemoryRepository : IFlowInstanceRepository
    {
        private readonly IList<Flow.FlowInstance> flowInstances;
        public FlowInstanceMemoryRepository()
        {
            flowInstances = new List<Flow.FlowInstance>();
        }

        public bool Delete(Flow.FlowInstance flowInstance)
        {
            var index = flowInstances.IndexOf(flowInstance);
            if (index > -1)
                flowInstances.Remove(flowInstance);

            return true;
        }

        public IEnumerable<Flow.FlowInstance> GetAll()
        {
            return flowInstances;
        }

        public IEnumerable<Flow.FlowInstance> GetAllAlive()
        {
            throw new NotImplementedException();
        }

        public Flow.FlowInstance GetById(Guid instanceId)
        {
            return flowInstances.Where(x => x.Id == instanceId).FirstOrDefault();
        }

        public bool Save(Flow.FlowInstance flowInstance)
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

            var obj = JsonConvert.DeserializeObject<Flow.FlowInstance>(serializedConfiguration, new JsonSerializerSettings
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
