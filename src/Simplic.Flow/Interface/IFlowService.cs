using Simplic.Flow.Event;
using System;

namespace Simplic.Flow
{
    public interface IFlowService
    {
        /// <summary>
        /// Initialize service
        /// </summary>
        /// <param name="machineName">Machine name</param>
        /// <param name="serviceName">Service name</param>
        void Initialize(string machineName, string serviceName);

        /// <summary>
        /// Run processing cycle
        /// </summary>
        void Run();

        /// <summary>
        /// Enqueue new event
        /// </summary>
        /// <param name="args">Event arguments</param>
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