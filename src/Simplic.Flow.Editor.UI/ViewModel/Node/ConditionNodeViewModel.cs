using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;
using System;
using System.Linq;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// ConditionNodeViewModel
    /// </summary>
    public class ConditionNodeViewModel : NodeViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodeDefinition">NodeDefinition</param>
        /// <param name="nodeConfiguration">NodeConfiguration</param>
        public ConditionNodeViewModel(NodeDefinition nodeDefinition, NodeConfiguration nodeConfiguration) 
            : base(nodeDefinition, nodeConfiguration)
        {
            // add flow in pin manually if it is not an event            
            CreateFlowInPin();            
        }

        public void UpdateDataTypes(Type typeToSet)
        {
            foreach (var item in DataPins.Where(x => x.IsGeneric))
            {
                item.DataConnectorType = typeToSet;                
            }
        }
    }
}
