using System;

namespace Simplic.Flow.Log
{
    public interface IFlowLogService
    {
        void Info(string message, Guid? flowInstanceId = null, Guid? eventId = null);
        void Warning(string message, Guid? flowInstanceId = null, Guid? eventId = null);
        void Error(string message, Exception exception, Guid? flowInstanceId = null, Guid? eventId = null);
    }
}
