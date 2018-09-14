using Simplic.Flow.Configuration;
using Simplic.Flow.Editor.Definition;

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

        }
    }
}
