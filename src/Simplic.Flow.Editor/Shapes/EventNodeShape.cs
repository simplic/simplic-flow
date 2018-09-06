using System.Collections.Generic;

namespace Simplic.Flow.Editor
{
    public class EventNodeShape : BaseNodeShape
    {
        public EventNodeShape(string headerText, IList<FlowConnector> flowConnectors, IList<DataConnector> dataConnectors)
            : base(headerText, flowConnectors, dataConnectors)
        {
            
        }
    }
}