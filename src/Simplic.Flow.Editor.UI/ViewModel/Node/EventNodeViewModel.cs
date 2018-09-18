using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;
using System.Linq;

namespace Simplic.Flow.Editor.UI
{
    /// <summary>
    /// EventNodeViewModel
    /// </summary>
    public class EventNodeViewModel : NodeViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodeDefinition">NodeDefinition</param>
        /// <param name="nodeConfiguration">NodeConfiguration</param>
        public EventNodeViewModel(NodeDefinition nodeDefinition, NodeConfiguration nodeConfiguration)
            : base(nodeDefinition, nodeConfiguration)
        {
            if (!nodeConfiguration.IsStartEvent)
            {
                CreateFlowInPin();
            }            
        }

        public override NodeConfiguration CreateConfiguration()
        {
            var parentViewModel = this.Parent as WorkflowEditorViewModel;
            
            var configuration = base.CreateConfiguration();
            configuration.IsStartEvent = false;

            if (parentViewModel == null)
                return configuration;

            // try to find out if this in pin is empty
            var flowInPins = FlowPins.Where(x => x.PinDirection == PinDirectionDefinition.In);
            foreach (var pin in flowInPins)
            {
                if (!parentViewModel.Connections.Any(x => x.TargetConnectorViewModel == pin))
                {
                    // no connections to this pin, so it must be a start event
                    configuration.IsStartEvent = true;
                }
            }
            
            return configuration;
        }
    }
}
