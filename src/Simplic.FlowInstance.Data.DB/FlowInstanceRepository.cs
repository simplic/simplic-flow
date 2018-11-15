using Dapper;
using Newtonsoft.Json;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simplic.Flow;

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

        #region Private Methods

        #region [ConvertToJson]
        /// <summary>
        /// Converts a byte array to <see cref="FlowInstance"/> object
        /// </summary>
        /// <param name="data">Serialized data</param>
        /// <returns><see cref="FlowInstance"/> object</returns>
        private Flow.FlowInstance ConvertToJson(byte[] data)
        {
            return JsonConvert.DeserializeObject<Flow.FlowInstance>(Encoding.UTF8.GetString(data), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }
        #endregion

        #region [ConvertFromJson]
        /// <summary>
        /// Converts a <see cref="FlowInstance"/> object to byte array
        /// </summary>
        /// <param name="flowInstance">object to convert</param>
        /// <returns>A byte array containing json object of the given <see cref="FlowInstance"/></returns>
        private byte[] ConvertFromJson(Flow.FlowInstance flowInstance)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(flowInstance, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            }));
        }
        #endregion

        #endregion

        #region Public Methods

        #region [GetAll]
        /// <summary>
        /// Gets a list of <see cref="FlowInstance"/> from the database
        /// </summary>
        /// <returns>A list of <see cref="FlowInstance"/> from the database</returns>
        public IEnumerable<Flow.FlowInstance> GetAll()
        {
            var flow_Instances = sqlService.OpenConnection((conn) =>
            {
                return conn.Query<FlowInstanceModel>($"SELECT * from {Flow_InstanceTableName}");
            });

            foreach (var item in flow_Instances)
            {
                var flowInstance = ConvertToJson(item.Data);
                flowInstance.FlowId = item.FlowConfigurationId;
                yield return flowInstance;
            }
        }
        #endregion

        #region [GetAllAlive]
        /// <summary>
        /// Gets a list of <see cref="FlowInstance"/> which are alive from the database 
        /// </summary>
        /// <returns>A list of <see cref="FlowInstance"/> which are alive from the database</returns>
        public IEnumerable<Flow.FlowInstance> GetAllAlive()
        {
            var flow_Instances = sqlService.OpenConnection((conn) =>
            {
                return conn.Query<FlowInstanceModel>($"SELECT * from {Flow_InstanceTableName} WHERE IsAlive = :IsAlive ORDER BY CreateTime",
                    new { IsAlive = true });
            });

            foreach (var item in flow_Instances)
            {
                var flowInstance = ConvertToJson(item.Data);
                flowInstance.FlowId = item.FlowConfigurationId;

                yield return flowInstance;
            }
        }
        #endregion

        #region [GetById]
        /// <summary>
        /// Gets a <see cref="FlowInstance"/>
        /// </summary>
        /// <param name="flowInstanceId">Id to get</param>
        /// <returns><see cref="FlowInstance"/></returns>
        public Flow.FlowInstance GetById(Guid flowInstanceId)
        {
            var flow_Instance = sqlService.OpenConnection((conn) =>
            {
                return conn.Query<FlowInstanceModel>($"SELECT * from {Flow_InstanceTableName} WHERE Id = :flowInstanceId",
                    new { flowInstanceId }).FirstOrDefault();
            });

            if (flow_Instance != null)
            {
                var flowInstance = ConvertToJson(flow_Instance.Data);
                flowInstance.FlowId = flow_Instance.FlowConfigurationId;

                return flowInstance;
            }
            else
                return null;
        }
        #endregion

        #region [Save]
        /// <summary>
        /// Saves a <see cref="FlowInstance"/> into the database
        /// </summary>
        /// <param name="flowInstance">Object to save</param>
        /// <returns>True if successfull</returns>
        public bool Save(Flow.FlowInstance flowInstance)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"Insert Into {Flow_InstanceTableName} " +
                   $" (Id, Data, IsAlive, FlowConfigurationId) On Existing Update Values " +
                   $" (:Id, :Data, :IsAlive, :FlowConfigurationId);", new
                   {
                       Id = flowInstance.Id,
                       Data = ConvertFromJson(flowInstance),
                       IsAlive = flowInstance.IsAlive,
                       FlowConfigurationId = flowInstance.Flow.Id
                   });

                return affectedRows > 0;
            });
        }
        #endregion

        #region [Delete]
        /// <summary>
        /// Deletes a <see cref="FlowInstance"/> from the database
        /// </summary>
        /// <param name="flowInstance">Object to remove</param>
        /// <returns>True if successfull</returns>
        public bool Delete(Flow.FlowInstance flowInstance)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"DELETE FROM {Flow_InstanceTableName} WHERE Id = :Id", new
                   {
                       Id = flowInstance.Id
                   });

                return affectedRows > 0;
            });
        }
        #endregion

        #endregion
    }
}
