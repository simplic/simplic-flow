using System.Collections.Generic;

namespace Simplic.Flow.Editor
{
    public class EventNode : BaseNode
    {
        public EventNode(string headerText, IList<FlowConnector> flowConnectors, IList<DataConnector> dataConnectors)
            : base(headerText, flowConnectors, dataConnectors)
        {

        }
    }
}