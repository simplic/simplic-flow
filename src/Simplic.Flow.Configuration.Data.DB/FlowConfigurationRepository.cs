using Dapper;
using Newtonsoft.Json;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simplic.Flow.Configuration.Data.DB
{
    public class FlowConfigurationRepository : IFlowConfigurationRepository
    {
        private readonly ISqlService sqlService;
        private const string FlowConfigurationTableName = "Flow_Configuration";

        public FlowConfigurationRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        #region Private Methods

        #region [ConvertToJson]
        private FlowConfiguration ConvertToJson(byte[] data)
        {
            return JsonConvert.DeserializeObject<FlowConfiguration>(Encoding.UTF8.GetString(data));
        }
        #endregion

        #region [ConvertFromJson]
        private byte[] ConvertFromJson(FlowConfiguration flowInstance)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(flowInstance));
        }
        #endregion

        #endregion

        #region Public Methods

        #region [GetAll]
        public IEnumerable<FlowConfiguration> GetAll(bool getOnlyActive = true)
        {
            var flowConfigurationModels = sqlService.OpenConnection((conn) =>
            {
                var condition = getOnlyActive ? " WHERE IsActive = 1 " : "";
                var sql = $"SELECT * from {FlowConfigurationTableName} {condition}";
                return conn.Query<FlowConfigurationModel>(sql);
            });

            foreach (var item in flowConfigurationModels)
            {
                var flowConfiguration = ConvertToJson(item.Configuration);
                yield return flowConfiguration;
            }
        }
        #endregion

        #region [GetById]
        public FlowConfiguration Get(Guid id)
        {
            var flowConfigurationModel = sqlService.OpenConnection((conn) =>
            {
                return conn.Query<FlowConfigurationModel>($"SELECT * from {FlowConfigurationTableName} WHERE Id = :id",
                    new { id }).FirstOrDefault();
            });

            if (flowConfigurationModel != null)
            {
                var config = ConvertToJson(flowConfigurationModel.Configuration);
                config.Id = flowConfigurationModel.Id;
                config.Name = flowConfigurationModel.Name;

                return config;
            }

            else
                return null;
        }
        #endregion

        #region [Save]
        public bool Save(FlowConfiguration flowConfiguration)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"Insert Into {FlowConfigurationTableName} " +
                   $" (Id, Configuration, Name) On Existing Update Values " +
                   $" (:id, :configuration, :name);", new
                   {
                       id = flowConfiguration.Id,
                       name = flowConfiguration.Name,
                       configuration = ConvertFromJson(flowConfiguration)
                   });

                return affectedRows > 0;
            });
        }
        #endregion

        #region [SetStatus]
        public bool SetStatus(Guid id, bool isActive)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"UPDATE {FlowConfigurationTableName} Set IsActive = :isActive WHERE Id = :id", new
                {
                    id,
                    isActive
                });

                return affectedRows > 0;
            });
        } 
        #endregion

        #endregion
    }
}
