using Dapper;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplic.Flow.EventQueue.Data.DB
{
    public class FlowEventQueueRepository : IFlowEventQueueRepository
    {
        private const string FlowEventQueueTableName = "Flow_Event_Queue";
        private readonly ISqlService sqlService;
        private static IList<EventServiceTarget> targets = null;

        public FlowEventQueueRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public IList<EventServiceTarget> GetEventTargets()
        {
            if (targets == null)
            {
                targets = sqlService.OpenConnection((conn) =>
                {
                    return conn.Query<EventServiceTarget>("SELECT DISTINCT ServiceName, MachineName FROM \"admin\".\"Flow_Configuration\" WHERE IsActive = 1").ToList();
                });
            }

            return targets;
        }

        public EventQueueModel Get(string id)
        {
            return sqlService.OpenConnection((conn) =>
            {
                return conn.Query<EventQueueModel>($"SELECT * FROM {FlowEventQueueTableName} WHERE Id = :id",
                    new { id }).FirstOrDefault();
            });
        }

        public void Remove(string id)
        {
            sqlService.OpenConnection((conn) =>
            {
                conn.Query<EventQueueModel>($"DELETE FROM {FlowEventQueueTableName} WHERE Id = :id",
                    new { id }).FirstOrDefault();
            });
        }

        public IEnumerable<EventQueueModel> GetAll()
        {
            return sqlService.OpenConnection((conn) =>
            {
                return conn.Query<EventQueueModel>($"SELECT * FROM {FlowEventQueueTableName}");
            });
        }

        public IEnumerable<EventQueueModel> GetAllUnhandled(string machineName, string serviceName)
        {
            return sqlService.OpenConnection((conn) =>
            {
                machineName = machineName ?? "";
                serviceName = serviceName ?? "";

                return conn.Query<EventQueueModel>($"SELECT top 150 * FROM {FlowEventQueueTableName} " +
                    $" WHERE Handled = :Handled AND MachineName = :machineName AND ServiceName = :serviceName ORDER BY CreateDateTime", new
                    {
                        Handled = false,
                        machineName,
                        serviceName
                    });
            });
        }

        public bool Save(EventQueueModel model)
        {
            return sqlService.OpenConnection((conn) =>
            {
                if (string.IsNullOrWhiteSpace(model.Id))
                    model.Id = Guid.NewGuid().ToString();

                int affectedRows = 0;
                foreach (var target in GetEventTargets())
                {
                    var dbModel = new TargetEventQueueModel
                    {
                        Args = model.Args,
                        CreateDateTime = model.CreateDateTime,
                        CreateUserId = model.CreateUserId,
                        EventName = model.EventName,
                        Handled = model.Handled,
                        Id = $"{target.MachineName}_{target.ServiceName}_{model.Id}",
                        MachineName = target.MachineName,
                        ServiceName = target.ServiceName
                    };

                    affectedRows += conn.Execute($"INSERT INTO {FlowEventQueueTableName} " +
                       $" (Id, EventName, Args, CreateDateTime, CreateUserId, Handled, MachineName, ServiceName) " +
                       $" ON EXISTING UPDATE VALUES (:Id, :EventName, :Args, :CreateDateTime, " +
                       $" :CreateUserId, :Handled, :MachineName, :ServiceName)", dbModel);
                }

                return affectedRows > 0;
            });
        }

        public bool SetHandled(string id, bool isHandled)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"UPDATE {FlowEventQueueTableName} " +
                    $" SET Handled = :{nameof(isHandled)} WHERE Id = :{nameof(id)}", new { id, isHandled });

                return affectedRows > 0;
            });
        }

        public bool SetFailed(string id)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"UPDATE {FlowEventQueueTableName} " +
                    $" SET Handled = 2 WHERE Id = :{nameof(id)}", new { id });

                return affectedRows > 0;
            });
        }

        public void ClearEventQueue()
        {
            sqlService.OpenConnection((conn) =>
            {
                var sql = $"DELETE {FlowEventQueueTableName} WHERE CreateDateTime < :createDateTime";
                var affectedRows = conn.Execute(sql, new { createDateTime = DateTime.Now.AddMinutes(-10) });

                return affectedRows > 0;
            });
        }
    }
}
