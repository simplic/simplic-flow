using Simplic.Flow.Event;
using System;

namespace Simplic.Flow.Node
{
    public class OnInstanceDataChangedEventArgs : FlowEventArgs
    {
        /// <summary>
        /// Gets or sets the source guid.
        /// </summary>
        public Guid SourceGuid { get; set; }

        /// <summary>
        /// Gets or sets the stack guid of the source.
        /// </summary>
        public Guid SourceStackGuid { get; set; }

        /// <summary>
        /// Gets or sets the destination guid.
        /// </summary>
        public Guid DestinationGuid { get; set; }

        /// <summary>
        /// Gets or sets the stack guid of the destination.
        /// </summary>
        public Guid DestinationStackGuid { get; set; }
    }
}
