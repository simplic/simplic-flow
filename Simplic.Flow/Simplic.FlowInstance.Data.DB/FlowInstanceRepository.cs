using Dapper;
using Newtonsoft.Json;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simplic.FlowInstance.Data.DB
{
    public class FlowInstanceRepository : IFlowInstanceRepository
    {
        private readonly ISqlService sqlService;
        private const string Flow_InstanceTableName = "Flow_Instance";

        public FlowInstanceRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        private FlowInstance ConvertToJson(byte[] data)
        {
            return JsonConvert.DeserializeObject<FlowInstance>(Encoding.UTF8.GetString(data), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }

        private byte[] ConvertFromJson(FlowInstance flowInstance)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(flowInstance, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            }));
        }

        public IEnumerable<FlowInstance> GetAll()
        {            
            var flow_Instances = sqlService.OpenConnection( (conn) => {
                return conn.Query<FlowInstanceModel>($"SELECT * from {Flow_InstanceTableName} WHERE IsAlive = :IsAlive", 
                    new { IsAlive = true });
            });

            foreach (var item in flow_Instances)
            {
                var flowInstance = ConvertToJson(item.Data);
                yield return flowInstance;
            }
        }
        
        public FlowInstance GetById(Guid flowInstanceId)
        {
            var flow_Instance = sqlService.OpenConnection((conn) => {
                return conn.Query<FlowInstanceModel>($"SELECT * from {Flow_InstanceTableName} WHERE Id = :flowInstanceId",
                    new { flowInstanceId }).FirstOrDefault();
            });

            if (flow_Instance != null)            
                return ConvertToJson(flow_Instance.Data);            
            else
                return null;
        }

        public bool Save(FlowInstance flowInstance)
        {            
            return sqlService.OpenConnection((conn) => {

                var affectedRows = conn.Execute($"Insert Into {Flow_InstanceTableName} " +
                   $" (Id, Data, IsAlive, CreateTime) On Existing Update Values " +
                   $" (:Id, :Data, :IsAlive, :CreateTime);", new {
                       Id = flowInstance.Id,
                       Data = ConvertFromJson(flowInstance),
                       IsAlive = flowInstance.IsAlive                       
                   });

                return affectedRows > 0;
            });
        }
    }
}
