using Simplic.Flow.Event;
using System;

namespace Simplic.Flow
{
    public interface IFlowService
    {
        void Run();
        void EnqueueEvent(FlowEventArgs args);

        /// <summary>
        /// Raised when the service has just started processing.
        /// </summary>
        event EventHandler OnStarted;

        /// <summary>
        /// Raised when the service is done processing all events and workflows
        /// </summary>
        event EventHandler OnCompleted;
    }
}