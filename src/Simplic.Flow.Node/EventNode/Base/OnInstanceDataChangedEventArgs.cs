using Simplic.Flow.Event;
using System;

namespace Simplic.Flow.Node
{
    public class OnInstanceDataChangedEventArgs : FlowEventArgs
    {
        public Guid SourceGuid { get; set; }
        public Guid SourceStackGuid { get; set; }
        public Guid DestinationGuid { get; set; }
        public Guid DestinationStackGuid { get; set; }
    }
}
