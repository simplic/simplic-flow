using System.Collections.Generic;

namespace Simplic.Flow.Editor
{
    public class ActionNode : BaseNode
    {
        public ActionNode(string headerText, IList<FlowConnector> flowConnectors, IList<DataConnector> dataConnectors) 
            : base(headerText, flowConnectors, dataConnectors)
        {

        }
    }
}
