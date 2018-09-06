using System.Collections.Generic;

namespace Simplic.Flow.Editor
{
    public class ActionNodeShape : BaseNodeShape
    {
        public ActionNodeShape(string headerText, IList<FlowConnector> flowConnectors, IList<DataConnector> dataConnectors) 
            : base(headerText, flowConnectors, dataConnectors)
        {

        }
    }
}
