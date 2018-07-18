using Simplic.Sql;
using System;
using System.Collections.Generic;

namespace Simplic.ActiveFlow.Data.DB
{
    public class ActiveFlowRepository : IActiveFlowRepository
    {
        private readonly ISqlService sqlService;
        private const string ActiveFlowTableName = "Flow_Active";

        public ActiveFlow Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActiveFlow> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActiveFlow> GetAllAlive()
        {
            throw new NotImplementedException();
        }

        public bool Save(ActiveFlow activeFlow)
        {
            throw new NotImplementedException();
        }
    }
}
