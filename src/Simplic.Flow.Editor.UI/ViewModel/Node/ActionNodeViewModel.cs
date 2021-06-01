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
        private bool isIntermediateStart;

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

            if (NodeConfiguration.IsIntermediateStart)
                SetPinVisibility();
        }

        public void UpdateDataTypes(Type typeToSet)
        {
            foreach (var item in DataPins.Where(x => x.IsGeneric))
            {
                item.DataConnectorType = typeToSet;
            }
        }

        /// <summary>
        /// Gets or sets the flow in-pin visible
        /// </summary>
        internal void SetPinVisibility()
        {
            var inPin = FlowPins.FirstOrDefault(x => x.PinDirection == PinDirectionDefinition.In);

            if (inPin != null)
                inPin.Visibility = NodeConfiguration.IsIntermediateStart ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Gets or sets whether the node is a intermediate start node
        /// </summary>
        public bool IsIntermediateStart
        {
            get => NodeConfiguration.IsIntermediateStart;
            set
            {
                var inPin = FlowPins.FirstOrDefault(x => x.PinDirection == PinDirectionDefinition.In);

                if (!value)
                {
                    NodeConfiguration.IsIntermediateStart = false;
                }
                else if(value && inPin != null && !inPin.IsConnected)
                {
                    NodeConfiguration.IsIntermediateStart = true;
                }

                SetPinVisibility();
            }
        }
    }
}
