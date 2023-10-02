using Dapper;
using Newtonsoft.Json;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simplic.Flow.Configuration.Data.DB
{
    /// <summary>
    /// Repository for FlowConfiguration.
    /// </summary>
    public class FlowConfigurationRepository : IFlowConfigurationRepository
    {
        private readonly ISqlService sqlService;
        private const string FlowConfigurationTableName = "Flow_Configuration";

        /// <summary>
        /// Constructor for FlowConfigurationRepository.
        /// </summary>
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
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<FlowConfiguration> GetAll(bool getOnlyActive = true)
        {
            var flowConfigurationModels = sqlService.OpenConnection((conn) =>
            {
                var condition = getOnlyActive ? " WHERE IsActive = 1 " : "";
                var sql = $"SELECT * from {FlowConfigurationTableName} {condition} AND IsDeleted = 0";
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
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
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
                config.MachineName = flowConfigurationModel.MachineName;
                config.ServiceName = flowConfigurationModel.ServiceName;

                return config;
            }

            else
                return null;
        }
        #endregion

        #region [GetByExportId]
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public FlowConfiguration GetByExportId(Guid exportId)
        {
            var flowConfigurationModel = sqlService.OpenConnection((conn) =>
            {
                return conn.Query<FlowConfigurationModel>
                    ($"SELECT * from {FlowConfigurationTableName} WHERE ExportId = :exportId",
                    new { exportId }).FirstOrDefault();
            });

            if (flowConfigurationModel != null)
            {
                var config = ConvertToJson(flowConfigurationModel.Configuration);
                config.Id = flowConfigurationModel.Id;
                config.Name = flowConfigurationModel.Name;
                config.MachineName = flowConfigurationModel.MachineName;
                config.ServiceName = flowConfigurationModel.ServiceName;

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
                   $" (Id, Configuration, Name, IsActive, MachineName, ServiceName, Description) On Existing Update Values " +
                   $" (:id, :configuration, :name, :isActive, :machineName, :serviceName, :description);", new
                   {
                       id = flowConfiguration.Id,
                       name = flowConfiguration.Name,
                       configuration = ConvertFromJson(flowConfiguration),
                       isActive = flowConfiguration.IsActive,
                       machineName = flowConfiguration.MachineName,
                       serviceName = flowConfiguration.ServiceName,
                       description = flowConfiguration.Description
                   });

                return affectedRows > 0;
            });
        }
        #endregion

        #region [SetStatus]
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
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

        #region [SetDeleted]
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool SetDeleted(Guid id)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"UPDATE {FlowConfigurationTableName} Set IsDeleted = 1, IsActive = 0 WHERE Id = :id", new
                {
                    id
                });

                return affectedRows > 0;
            });
        }
        #endregion

        #endregion
    }
}
