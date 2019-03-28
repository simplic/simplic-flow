using System;
using System.Runtime.Serialization;

namespace Simplic.Flow.Configuration
{
    public class FlowConfigurationServiceException : Exception
    {
        public FlowConfigurationServiceException()
        {
        }

        public FlowConfigurationServiceException(string message) : base(message)
        {
        }

        public FlowConfigurationServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlowConfigurationServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
