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

        public FlowEventQueueRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public EventQueueModel Get(Guid id)
        {
            return sqlService.OpenConnection((conn) =>
            {
                return conn.Query<EventQueueModel>($"SELECT * FROM {FlowEventQueueTableName} WHERE Id = :id",
                    new { id }).FirstOrDefault();
            });
        }

        public void Remove(Guid id)
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

        public IEnumerable<EventQueueModel> GetAllUnhandled()
        {
            return sqlService.OpenConnection((conn) =>
            {
                return conn.Query<EventQueueModel>($"SELECT * FROM {FlowEventQueueTableName} " +
                    $" WHERE Handled = :Handled", new { Handled = false });
            });
        }

        public bool Save(EventQueueModel model)
        {
            return sqlService.OpenConnection((conn) =>
            {
                if (model.Id == Guid.Empty)
                    model.Id = Guid.NewGuid();

                var affectedRows = conn.Execute($"INSERT INTO {FlowEventQueueTableName} " +
                   $" (Id, EventName, Args, CreateDateTime, CreateUserId, Handled) " +
                   $" ON EXISTING UPDATE VALUES (:Id, :EventName, :Args, :CreateDateTime, " +
                   $" :CreateUserId, :Handled)", model);

                return affectedRows > 0;
            });
        }

        public bool SetHandled(Guid id, bool isHandled)
        {
            return sqlService.OpenConnection((conn) =>
            {
                var affectedRows = conn.Execute($"UPDATE {FlowEventQueueTableName} " +
                    $" SET Handled = :{nameof(isHandled)} WHERE Id = :{nameof(id)}", new { id, isHandled });

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
