using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;
using System;
using System.Linq;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// ActionNodeViewModel
    /// </summary>
    public class ActionNodeViewModel : NodeViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodeDefinition">NodeDefinition</param>
        /// <param name="nodeConfiguration">NodeConfiguration</param>
        public ActionNodeViewModel(NodeDefinition nodeDefinition, NodeConfiguration nodeConfiguration) 
            : base(nodeDefinition, nodeConfiguration)
        {
            // add flow in pin manually if it is not an event            
            CreateFlowInPin();
        }
    }
}
