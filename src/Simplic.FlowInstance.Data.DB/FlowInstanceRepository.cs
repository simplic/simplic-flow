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

        #region Private Methods

        #region [ConvertToJson]
        /// <summary>
        /// Converts a byte array to <see cref="FlowInstance"/> object
        /// </summary>
        /// <param name="data">Serialized data</param>
        /// <returns><see cref="FlowInstance"/> object</returns>
        private FlowInstance ConvertToJson(byte[] data)
        {
            return JsonConvert.DeserializeObject<FlowInstance>(Encoding.UTF8.GetString(data), new JsonSerializerSettings
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
        private byte[] ConvertFromJson(FlowInstance flowInstance)
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
        public IEnumerable<FlowInstance> GetAll()
        {
            var flow_Instances = sqlService.OpenConnection((conn) =>
            {
                return conn.Query<FlowInstanceModel>($"SELECT * from {Flow_InstanceTableName}");
            });

            foreach (var item in flow_Instances)
            {
                var flowInstance = ConvertToJson(item.Data);
                yield return flowInstance;
            }
        }
        #endregion

        #region [GetAllAlive]
        /// <summary>
        /// Gets a list of <see cref="FlowInstance"/> which are alive from the database 
        /// </summary>
        /// <returns>A list of <see cref="FlowInstance"/> which are alive from the database</returns>
        public IEnumerable<FlowInstance> GetAllAlive()
        {
            var flow_Instances = sqlService.OpenConnection((conn) =>
            {
                return conn.Query<FlowInstanceModel>($"SELECT * from {Flow_InstanceTableName} WHERE IsAlive = :IsAlive ORDER BY CreateTime",
                    new { IsAlive = true });
            });

            foreach (var item in flow_Instances)
            {
                var flowInstance = ConvertToJson(item.Data);
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
        public FlowInstance GetById(Guid flowInstanceId)
        {
            var flow_Instance = sqlService.OpenConnection((conn) =>
            {
                return conn.Query<FlowInstanceModel>($"SELECT * from {Flow_InstanceTableName} WHERE Id = :flowInstanceId",
                    new { flowInstanceId }).FirstOrDefault();
            });

            if (flow_Instance != null)
                return ConvertToJson(flow_Instance.Data);
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
        public bool Save(FlowInstance flowInstance)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"Insert Into {Flow_InstanceTableName} " +
                   $" (Id, Data, IsAlive) On Existing Update Values " +
                   $" (:Id, :Data, :IsAlive);", new
                   {
                       Id = flowInstance.Id,
                       Data = ConvertFromJson(flowInstance),
                       IsAlive = flowInstance.IsAlive                       
                   });

                return affectedRows > 0;
            });
        }
        #endregion 

        #endregion
    }
}
